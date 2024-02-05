using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;

namespace finlang.Transpiler;

public class CFileGenerator : CSharpSyntaxWalker
{
    C99ClsEnum cls;
    private SemanticModel model;
    StringBuilder sb;
    Namer namer;

    public CFileGenerator(C99ClsEnum cls) : base(SyntaxWalkerDepth.StructuredTrivia)
    {
        this.cls = cls;
        this.model = cls.model;
        sb = cls.cFile.mainCode;
        namer = new Namer(model);
    }

    public void Generate()
    {
        // TODO private constants

        foreach (var member in cls.GetMethods())
        {
            if (member.DeclaringSyntaxReferences.Any())
                Visit(member.DeclaringSyntaxReferences[0].GetSyntax());
        }
    }

    public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
    {
        var symbol = model.GetDeclaredSymbol(node).ThrowIfNull();

        VisitLeadingTrivia(node);
        sb.Append($"void {Namer.GetCName(symbol)}");

        VisitParameterList(node.ParameterList);

        var body = node.Body.ThrowIfNull();
        VisitToken(body.OpenBraceToken);
        sb.Append("        memset(self, 0, sizeof(*self));\n");
        cls.cFile.includes.Add("<string.h>"); // for memset
        body.VisitChildrenNodesWithWalker(this);
        VisitToken(body.CloseBraceToken);
    }

    // parameters are declared for methods and constructors
    public override void VisitParameterList(ParameterListSyntax node)
    {
        ISymbol? symbol = null;

        if (node.Parent is MethodDeclarationSyntax mds)
        {
            symbol = model.GetDeclaredSymbol(mds).ThrowIfNull();
        }
        else if (node.Parent is ConstructorDeclarationSyntax cds)
        {
            symbol = model.GetDeclaredSymbol(cds).ThrowIfNull();
        }

        var list = new WalkableChildSyntaxList(this, node.ChildNodesAndTokens());

        if (symbol?.IsStatic == false)
        {
            list.VisitUpTo(node.OpenParenToken, including: true);

            sb.Append(Namer.GetCName(symbol.ContainingType) + "* self");
            if (node.Parameters.Count > 0)
            {
                sb.Append(", ");
            }
        }

        list.VisitRest();
    }

    ///////////////////////////////////////////////////////////////////////////////

    public override void VisitPredefinedType(PredefinedTypeSyntax node)
    {
        string result = node.Keyword.Text switch
        {
            "void" => "void",
            "bool" => "bool",
            "sbyte" => "int8_t",
            "byte" => "uint8_t",
            "short" => "int16_t",
            "ushort" => "uint16_t",
            "int" => "int32_t",
            "uint" => "uint32_t",
            "long" => "int64_t",
            "ulong" => "uint64_t",
            "float" => "float",
            "double" => "double",
            "string" => "char const *",
            _ => throw new NotImplementedException(node + ""),
        };

        VisitLeadingTrivia(node);
        sb.Append(result);
        VisitTrailingTrivia(node);
    }

    public override void VisitIdentifierName(IdentifierNameSyntax node)
    {
        var result = node.Identifier.Text;

        switch (result)
        {
            case "Boolean": result = "bool"; break;
            case "SByte": result = "int8_t"; break;
            case "Byte": result = "uint8_t"; break;
            case "Int16": result = "int16_t"; break;
            case "UInt16": result = "uint16_t"; break;
            case "Int32": result = "int32_t"; break;
            case "UInt32": result = "uint32_t"; break;
            case "Int64": result = "int64_t"; break;
            case "UInt64": result = "uint64_t"; break;
            case "Double": result = "float"; break;
            case "Single": result = "double"; break;

            default:
                {
                    SymbolInfo symbol = model.GetSymbolInfo(node);
                    result = Namer.GetCName(symbol.Symbol.ThrowIfNull());
                    break;
                }
        }

        VisitLeadingTrivia(node);
        sb.Append(result);
        VisitTrailingTrivia(node);
    }

    public override void VisitTrivia(SyntaxTrivia trivia)
    {
        sb.Append(trivia);
    }

    public override void VisitLeadingTrivia(SyntaxToken token)
    {
        if (!token.HasLeadingTrivia)
            return;

        VisitTriviaList(token.LeadingTrivia);
    }

    public override void VisitTrailingTrivia(SyntaxToken token)
    {
        if (!token.HasTrailingTrivia)
            return;

        VisitTriviaList(token.TrailingTrivia);
    }

    public void VisitTriviaList(SyntaxTriviaList syntaxTrivias)
    {
        VisitTriviaList((IReadOnlyList<SyntaxTrivia>)syntaxTrivias);
    }

    public void VisitTriviaList(IReadOnlyList<SyntaxTrivia> syntaxTrivias)
    {
        foreach (var trivia in syntaxTrivias)
        {
            VisitTrivia(trivia);
        }
    }

    private void VisitLeadingTrivia(SyntaxNode node)
    {
        VisitLeadingTrivia(node.GetFirstToken());
    }

    private void VisitTrailingTrivia(SyntaxNode node)
    {
        VisitTrailingTrivia(node.GetFirstToken());
    }

    private void OutputAttachedCommentTrivia(SyntaxNode node)
    {
        List<SyntaxTrivia> toOutput = GilTranspilerHelper.GetAttachedCommentTrivia(node);
        VisitTriviaList(toOutput);
    }

    public override void VisitToken(SyntaxToken token)
    {
        VisitLeadingTrivia(token);

        switch ((SyntaxKind)token.RawKind)
        {
            case SyntaxKind.PublicKeyword:
            case SyntaxKind.EnumKeyword:
            case SyntaxKind.StaticKeyword:
            case SyntaxKind.ReadOnlyKeyword:
            case SyntaxKind.PrivateKeyword:
                return;
        }

        if (token.IsKind(SyntaxKind.ExclamationToken) && token.Parent.IsKind(SyntaxKind.SuppressNullableWarningExpression))
        {
            // ignore exclamations like: `this.current_state_exit_handler!();`
        }
        else if (token.IsKind(SyntaxKind.IdentifierToken) && token.Parent is MethodDeclarationSyntax mds)
        {
            sb.Append(Namer.GetCName(model.GetDeclaredSymbol(mds).ThrowIfNull()));
        }
        else if (token.IsKind(SyntaxKind.IdentifierToken) && token.Parent is EnumMemberDeclarationSyntax emds)
        {
            sb.Append(Namer.GetCName(model.GetDeclaredSymbol(emds).ThrowIfNull()));
        }
        else if (token.IsKind(SyntaxKind.ThisKeyword))
        {
            sb.Append("self");
        }
        else
        {
            sb.Append(token);
        }

        VisitTrailingTrivia(token);
    }
}
