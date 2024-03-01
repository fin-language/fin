
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;

namespace finlang.Transpiler;

public class TranspilerHelper
{
    private readonly CSharpSyntaxWalker transpilerWalker;
    public readonly SemanticModel model;
    public readonly CompilationUnitSyntax root;

    public TranspilerHelper(CSharpSyntaxWalker transpilerWalker, SemanticModel model, CompilationUnitSyntax? root = null)
    {
        this.transpilerWalker = transpilerWalker;
        this.model = model;
        this.root = root ?? model.SyntaxTree.GetCompilationUnitRoot();
    }

    public bool ExpressionIsEnumMember(ExpressionSyntax expressionSyntax)
    {
        ISymbol? symbol = model.GetSymbolInfo(expressionSyntax).Symbol;
        return symbol.IsEnumMember();
    }

    public bool IsEnumMemberConversionToInt(CastExpressionSyntax node)
    {
        if (node.Type is PredefinedTypeSyntax pts && pts.Keyword.IsKind(SyntaxKind.IntKeyword))
        {
            if (ExpressionIsEnumMember(node.Expression))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// returns true if `this.SomeMethod`
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public bool IsThisMethodAccess(MemberAccessExpressionSyntax node)
    {
        var m = model;

        return IsThisMethodAccess(node, m);
    }

    public static bool IsThisMethodAccess(MemberAccessExpressionSyntax node, SemanticModel model)
    {
        if (node.IsThisMemberAccess())
        {
            if (node.Name.IsIMethodSymbol(model))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Returns attached comment trivia and possible trailing end of line trivia.
    /// Unattached comments have a full blank line trailing them:
    /// <code>
    /// // unattached comment
    /// 
    /// // attached comment
    /// void some_function()
    /// </code>
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public static List<SyntaxTrivia> GetAttachedCommentTrivia(SyntaxNode node)
    {
        // Only output attached comments. If we find 2 or more end of line trivia without a comment trivia,
        // clear any stored trivia.
        List<SyntaxTrivia> toOutput = new();

        int endOfLineCount = 0;
        foreach (var t in node.GetLeadingTrivia())
        {
            bool isComment = t.IsKind(SyntaxKind.SingleLineCommentTrivia)
                          || t.IsKind(SyntaxKind.MultiLineCommentTrivia); // can also look at others like SingleLineDocumentationCommentTrivia

            if (t.IsKind(SyntaxKind.EndOfLineTrivia))
            {
                endOfLineCount++;
                if (endOfLineCount > 1)
                    toOutput.Clear();
                else if (toOutput.Any()) // append end of line if we already had a comment stored
                    toOutput.Add(t);
            }
            else if (isComment)
            {
                endOfLineCount = 0;
                toOutput.Add(t);
            }
            else
            {
                toOutput.Add(t);
            }
        }

        return toOutput;
    }
}
