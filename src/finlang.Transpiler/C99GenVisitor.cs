#nullable enable

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System.Text;
using Microsoft.Extensions.Primitives;

// spell-checker: ignore customizer

namespace finlang.Transpiler;

public class C99GenVisitor : CSharpSyntaxWalker
{
    public readonly StringBuilder hFileSb;
    public readonly StringBuilder cFileSb;
    public StringBuilder privateSb = new();
    public StringBuilder publicSb = new();
    public StringBuilder sb;
    protected readonly SemanticModel model;
    protected bool renderingPrototypes = false;
    private readonly IEnumerable<ClassDeclarationSyntax> allClasses;
    private readonly IEnumerable<EnumDeclarationSyntax> allEnums;
    private readonly IEnumerable<DelegateDeclarationSyntax> allDelegates;
    protected readonly GilTranspilerHelper transpilerHelper;

    public C99GenVisitor(SemanticModel model, StringBuilder hFileSb, StringBuilder cFileSb) : base(SyntaxWalkerDepth.StructuredTrivia)
    {
        this.model = model;
        this.hFileSb = hFileSb;
        this.cFileSb = cFileSb;

        transpilerHelper = new(this, model);
        allClasses = model.SyntaxTree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>();
        allEnums = model.SyntaxTree.GetRoot().DescendantNodes().OfType<EnumDeclarationSyntax>();
        allDelegates = model.SyntaxTree.GetRoot().DescendantNodes().OfType<DelegateDeclarationSyntax>();
        sb = hFileSb;
    }

    public void Process()
    {
        sb = hFileSb;
        hFileSb.AppendLine("#pragma once");
        hFileSb.AppendLine("#include <stdint.h>\n");

        sb = cFileSb;
        cFileSb.AppendLine("#include <stdbool.h> // required for `consume_event` flag");
        cFileSb.AppendLine("#include <string.h> // for memset\n");

        sb = hFileSb;

        OutputForwardClassStuff();

        sb.AppendLine();
        OutputCommentSection($"enumerations and constant numbers");
        foreach (var cls in allClasses)
        {
            foreach (var kid in cls.ChildNodes())
            {
                if (kid is EnumDeclarationSyntax || kid is FieldDeclarationSyntax field && field.IsConst())
                    Visit(kid);
            }
        }
        sb.AppendLine();

        OutputCommentSection($"structures");
        foreach (var cls in allClasses.Reverse())   // reversing allows dependencies between structs to work out OK.
        {
            VisitClassDeclaration(cls);
        }
    }

    public override void VisitClassDeclaration(ClassDeclarationSyntax cls)
    {
        sb = hFileSb;

        string name = GetCName(cls);

        OutputStruct(cls, name);

        sb = cFileSb;
        publicSb = cFileSb;
        privateSb = cFileSb;

        foreach (var kid in cls.ChildNodes().OfType<ConstructorDeclarationSyntax>())
        {
            VisitConstructorDeclaration(kid);
        }

        foreach (var kid in cls.ChildNodes().OfType<MethodDeclarationSyntax>())
        {
            VisitMethodDeclaration(kid);
        }
    }

    private void OutputCommentSection(string title)
    {
        sb.AppendLine("//////////////////////////////////////////////////////////////////////////////////////////////////////////////");
        sb.AppendLine($"// {title}");
        sb.AppendLine("//////////////////////////////////////////////////////////////////////////////////////////////////////////////");
        sb.AppendLine();
    }

    private void OutputForwardClassStuff()
    {
        sb.AppendLine();
        OutputCommentSection("typedefs");
        foreach (var cls in allClasses)
        {
            string name = GetCName(cls);
            sb.AppendLine($"typedef struct {name} {name};");
        }
        foreach (var enumDecl in allEnums)
        {
            string name = GetCName(enumDecl);
            sb.AppendLine($"typedef enum {name} {name};");
        }
        foreach (var d in allDelegates)
        {
            VisitDelegateDeclaration(d);
        }
        sb.AppendLine();

        foreach (var cls in allClasses)
        {
            if (cls.DescendantNodes().OfType<MethodDeclarationSyntax>().Any() == false)
                continue;

            OutputCommentSection($"public {cls.Identifier.ValueText} functions");

            publicSb = hFileSb;
            privateSb = cFileSb;
            CaptureFunctionPrototypes(cls);
        }
    }

    public override void VisitInvocationExpression(InvocationExpressionSyntax node)
    {
        bool done = false;

        //done |= transpilerHelper.HandleGilSpecialInvocations(node, sb);
        //done |= transpilerHelper.HandleGilUnusedVarSpecialInvocation(node, argument =>
        //{
        //    var argName = "sm"; // we only ignore `sm` in ROOT_exit right now so we can cheat here. If that changes, we can visit `argument` instead.

        //    VisitLeadingTrivia(node);
        //    sb.Append($"(void){argName}");   // trailing semi-colon is already part of parent ExpressionStatement
        //});

        if (!done)
        {
            base.VisitInvocationExpression(node);
        }
    }

    // to ignore GIL attributes
    public override void VisitAttributeList(AttributeListSyntax node)
    {
        VisitLeadingTrivia(node);
    }

    private void OutputStruct(TypeDeclarationSyntax node, string name, bool outputTypedef = false)
    {
        OutputAttachedCommentTrivia(node);
        if (outputTypedef)
            sb.Append("typedef ");

        sb.Append("struct ");
        sb.AppendTokenAndTrivia(node.Identifier, overrideTokenText: name);
        sb.AppendTokenAndTrivia(node.OpenBraceToken);

        foreach (var kid in node.ChildNodes())
        {
            if (kid is FieldDeclarationSyntax field && !field.IsConst())
                VisitFieldDeclaration(field);
        }

        VisitLeadingTrivia(node.CloseBraceToken);
        sb.Append('}');
        if (outputTypedef)
            sb.Append($" {name}");
        sb.Append(';');
        VisitTrailingTrivia(node.CloseBraceToken);
        sb.AppendLine();
    }

    private void CaptureFunctionPrototypes(ClassDeclarationSyntax node)
    {
        renderingPrototypes = true;

        List<SyntaxNode> kids = GetMethodsAndConstructors(node);

        foreach (var kid in kids)
        {
            Visit(kid);
            sb.Append(";\n\n");
        }
        renderingPrototypes = false;
    }

    private List<SyntaxNode> GetMethodsAndConstructors(ClassDeclarationSyntax node)
    {
        List<SyntaxNode> kids = new();
        kids.AddRange(node.ChildNodes().OfType<ConstructorDeclarationSyntax>());
        //kids.AddRange(node.ChildNodes().OfType<MethodDeclarationSyntax>().Where(mds => !transpilerHelper.IsGilNoEmit(mds)));
        return kids;
    }

    // delegates are assumed to be method pointers
    public override void VisitDelegateDeclaration(DelegateDeclarationSyntax node)
    {
        var symbol = model.GetDeclaredSymbol(node).ThrowIfNull();

        VisitLeadingTrivia(node);
        sb.Append("typedef ");
        Visit(node.ReturnType);
        sb.Append("(*");
        sb.Append(GetCName(symbol));
        sb.Append(')');
        sb.Append("(" + GetCName(symbol.ContainingType) + "* sm)");
        //Visit(node.ParameterList);
        VisitToken(node.SemicolonToken);
    }

    public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
    {
        var symbol = model.GetDeclaredSymbol(node).ThrowIfNull();

        if (!renderingPrototypes)
            VisitLeadingTrivia(node); // this will output any unattached comment sections
        else
            OutputAttachedCommentTrivia(node);

        sb.Append($"void {GetCName(symbol)}");

        VisitParameterList(node.ParameterList);

        if (!renderingPrototypes)
        {
            var body = node.Body.ThrowIfNull();
            VisitToken(body.OpenBraceToken);
            sb.Append("    memset(sm, 0, sizeof(*sm));\n"); // todo_low - sm should be var so we can use `sm`, `this`, `self`...
            body.VisitChildrenNodesWithWalker(this);
            VisitToken(body.CloseBraceToken);
        }
    }

    public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
        sb = node.IsPublic() ? publicSb : privateSb;

        //if (transpilerHelper.IsGilNoEmit(node))
        //    return;

        if (!renderingPrototypes)
            VisitLeadingTrivia(node); // this will output any unattached comment sections
        else
            OutputAttachedCommentTrivia(node);

        if (!node.IsPublic())
            sb.Append("static ");

        Visit(node.ReturnType);
        VisitToken(node.Identifier);
        VisitParameterList(node.ParameterList);

        if (!renderingPrototypes)
            VisitBlock(node.Body);
    }

    public override void VisitBlock(BlockSyntax? node)
    {
        if (node == null) return;

        base.VisitBlock(node);
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

        // If rendering prototypes, we don't want to output closing parenthesis's trailing trivia which is likely an end of line.
        // That would result in weird output like `void some_func()\n;`.
        if (renderingPrototypes)
            list.Replace(node.CloseParenToken, replacement: node.CloseParenToken.WithTrailingTrivia());

        if (symbol?.IsStatic == false)
        {
            list.VisitUpTo(node.OpenParenToken, including: true);

            sb.Append(GetCName(symbol.ContainingType) + "* sm");
            if (node.Parameters.Count > 0)
            {
                sb.Append(", ");
            }
        }

        list.VisitRest();
    }

    // arguments are passed to methods/constructors
    public override void VisitArgumentList(ArgumentListSyntax node)
    {
        var invocation = (InvocationExpressionSyntax)node.Parent.ThrowIfNull();
        var iMethodSymbol = (IMethodSymbol)model.GetSymbolInfo(invocation).ThrowIfNull().Symbol.ThrowIfNull();

        if (!iMethodSymbol.IsStatic)
        {
            var list = new WalkableChildSyntaxList(this, node.ChildNodesAndTokens());
            list.VisitUpTo(node.OpenParenToken, including: true);

            sb.Append("sm");
            if (node.Arguments.Count > 0)
            {
                sb.Append(", ");
            }

            list.VisitRest();
        }
        else
        {
            base.VisitArgumentList(node);
        }
    }

    public override void VisitParameter(ParameterSyntax node)
    {
        var parameterSymbol = model.GetDeclaredSymbol(node);

        if (parameterSymbol != null && parameterSymbol.Type.IsReferenceType && parameterSymbol.Type.BaseType?.Name != nameof(System.MulticastDelegate))
        {
            Visit(node.Type);
            //sb.Append(PostProcessor.trimHorizontalWhiteSpaceMarker); // converts `ROOT_enter(Spec1Sm * sm);` to `ROOT_enter(Spec1Sm* sm);`
            sb.Append("* ");
            VisitToken(node.Identifier);
        }
        else
        {
            base.VisitParameter(node);
        }
    }

    // <Expression> <OperatorToken> <Name>
    // `this.stuff` this == Expression. stuff == Name.
    public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
    {
        bool done = false;

        if (transpilerHelper.ExpressionIsEnumMember(node.Expression))
        {
            // used for enum access: `MyEnumClass.EnumName`
            Visit(node.Name);
            done = true;
        }
        else if (transpilerHelper.HandleThisMethodAccess(node))
        {
            done = true;
        }
        else
        {
            if (node.IsSimpleMemberAccess())
            {
                done = HandleSimpleMemberAccess(node, done);
            }
        }

        if (!done)
            base.VisitMemberAccessExpression(node);
    }

    private bool HandleSimpleMemberAccess(MemberAccessExpressionSyntax node, bool done)
    {
        bool isPtr = false;

        if (node.Expression is ThisExpressionSyntax tes)
        {
            // `this.stuff` to `sm->stuff`
            VisitLeadingTrivia(tes.Token);
            sb.Append("sm");
            VisitTrailingTrivia(tes.Token);
            isPtr = true;
        }
        // `sm.stuff` to `sm->stuff`
        else if (node.Expression is IdentifierNameSyntax identifierNameSyntax)
        {
            ISymbol? symbol = model.GetSymbolInfo(identifierNameSyntax).Symbol;

            if (symbol is IParameterSymbol parameterSymbol && parameterSymbol.Type.IsReferenceType)
            {
                Visit(identifierNameSyntax);
                isPtr = true;
            }
        }
        else
        {
            Visit(node.Expression);
        }

        if (isPtr)
        {
            sb.Append("->");
            VisitTrailingTrivia(node.OperatorToken);
        }
        else
        {
            VisitToken(node.OperatorToken);
        }

        Visit(node.Name);
        done = true;
        

        return done;
    }

    public override void VisitNullableType(NullableTypeSyntax node)
    {
        // converts `Func? behavior_func` to `Func behavior_func`
        Visit(node.ElementType);
        VisitLeadingTrivia(node.QuestionToken);
        VisitTrailingTrivia(node.QuestionToken);
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
            sb.Append(GetCName(model.GetDeclaredSymbol(mds).ThrowIfNull()));
        }
        else if (token.IsKind(SyntaxKind.IdentifierToken) && token.Parent is EnumMemberDeclarationSyntax emds)
        {
            sb.Append(GetCName(model.GetDeclaredSymbol(emds).ThrowIfNull()));
        }
        else if (token.IsKind(SyntaxKind.ThisKeyword))
        {
            sb.Append("sm");
        }
        else
        {
            sb.Append(token);
        }

        VisitTrailingTrivia(token);
    }

    public override void VisitCastExpression(CastExpressionSyntax node)
    {
        if (transpilerHelper.IsEnumMemberConversionToInt(node))
        {
            // just visit expression so we omit int cast
            // `(int32_t)event_id` ---> `event_id`
            Visit(node.Expression);
        }
        else
        {
            base.VisitCastExpression(node);
        }
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
                    result = GetCName(symbol.Symbol.ThrowIfNull());
                    break;
                }
        }

        VisitLeadingTrivia(node);
        sb.Append(result);
        VisitTrailingTrivia(node);
    }

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

    public override void VisitLiteralExpression(LiteralExpressionSyntax node)
    {
        // convert `null` to `NULL`
        if (node.IsKind(SyntaxKind.NullLiteralExpression))
        {
            sb.Append("NULL");
        }
        else
        {
            base.VisitLiteralExpression(node);
        }
    }

    public override void VisitVariableDeclaration(VariableDeclarationSyntax node)
    {
        if (node.Type is ArrayTypeSyntax)
        {
            HandleArrayVarDecl(node);
        }
        else
        {
            base.VisitVariableDeclaration(node);
        }
    }

    public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
    {
        bool done = false;
        bool useDefine = false;
        bool useEnum = true;

        if (node.IsConst())
        {
            if (useDefine)
            {
                done = true;
                VisitLeadingTrivia(node);
                sb.Append("#define ");
                var decl = node.Declaration.Variables.Single();
                sb.Append(GetCName(model.GetDeclaredSymbol(decl).ThrowIfNull()));
                sb.Append(' ');
                Visit(decl.Initializer.ThrowIfNull().Value);
                sb.Append('\n');
            }
            else if (useEnum)
            {
                done = true;
                VisitLeadingTrivia(node);
                sb.Append("enum\n{\n    ");
                VisitVariableDeclarator(node.Declaration.Variables.Single());
                sb.Append("\n};\n");
            }
        }

        if (!done)
        {
            base.VisitFieldDeclaration(node);
        }
    }

    public override void VisitVariableDeclarator(VariableDeclaratorSyntax node)
    {
        if (node.FirstAncestorOrSelf<FieldDeclarationSyntax>().IsConst())
        {
            sb.Append(GetCName(model.GetDeclaredSymbol(node).ThrowIfNull()));
            VisitTrailingTrivia(node.Identifier);
        }
        else
        {
            VisitToken(node.Identifier);
        }

        if (node.Initializer?.Value is ObjectCreationExpressionSyntax)
        {
            //sb.Append(PostProcessor.trimHorizontalWhiteSpaceMarker);
        }
        else
        {
            if (node.Initializer != null)
                VisitEqualsValueClause(node.Initializer);
        }
    }

    private void HandleArrayVarDecl(VariableDeclarationSyntax node)
    {
        var ats = (ArrayTypeSyntax)node.Type;
        Visit(ats.ElementType);
        sb.Append(' ');

        foreach (VariableDeclaratorSyntax v in node.Variables)
        {
            sb.Append(v.Identifier);

            var rank = v.DescendantNodes().OfType<ArrayRankSpecifierSyntax>().SingleOrDefault();
            if (rank != null)
                VisitArrayRankSpecifier(rank);
        }
    }

    public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
    {
        VisitLeadingTrivia(node);
        string name = GetCName(node);

        //sb.AppendTokenAndTrivia(node.Identifier, overrideTokenText: customizer.MakeEnumDeclaration(name));
        sb.AppendTokenAndTrivia(node.OpenBraceToken);

        foreach (var kid in node.ChildNodesAndTokens().SkipWhile(n => n.IsToken))
        {
            if (kid.IsNode)
            {
                Visit(kid.AsNode());
            }
            else
            {
                if (kid == node.CloseBraceToken)
                    break;
                VisitToken(kid.AsToken());
            }
        }

        VisitLeadingTrivia(node.CloseBraceToken);
        sb.Append($"}} {name};");
        VisitTrailingTrivia(node.CloseBraceToken);
    }

    private static string MangleTypeSymbolName(string fullyQualifiedName)
    {
        string textName = fullyQualifiedName.Replace(oldChar: '.', newChar: '_');
        return textName;
    }

    private string GetCName(ClassDeclarationSyntax node)
    {
        INamedTypeSymbol symbol = model.GetDeclaredSymbol(node).ThrowIfNull();
        return GetCName(symbol);
    }

    private string GetCName(StructDeclarationSyntax node)
    {
        INamedTypeSymbol symbol = model.GetDeclaredSymbol(node).ThrowIfNull();
        return GetCName(symbol);
    }

    private string GetCName(EnumDeclarationSyntax node)
    {
        INamedTypeSymbol symbol = model.GetDeclaredSymbol(node).ThrowIfNull();
        return GetCName(symbol);
    }

    private string GetCName(ISymbol symbol)
    {
        if (symbol is IFieldSymbol fieldSymbol)
        {
            if (!fieldSymbol.IsStatic && !fieldSymbol.IsConst)
            {
                return fieldSymbol.Name;
            }
        }

        if (symbol.Kind == SymbolKind.Parameter || symbol.Kind == SymbolKind.Local)
        {
            return symbol.Name;
        }

        if (symbol is IMethodSymbol methodSymbol && methodSymbol.DeclaredAccessibility != Accessibility.Public)
        {
            return methodSymbol.Name;
        }

        var fqn = transpilerHelper.GetFQN(symbol);
        var name = MangleTypeSymbolName(fqn);
        return name;
    }

    private string GetCName(SymbolInfo symbolInfo)
    {
        return GetCName(symbolInfo.Symbol.ThrowIfNull());
    }


    ///////////////////////////////////////////////////////////////////////////////

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


}
