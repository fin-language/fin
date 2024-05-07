using Microsoft.CodeAnalysis;

namespace finlang.Transpiler;

public class TranspilerException : System.Exception
{
    public SyntaxNode? SyntaxNode { get; init; }

    public TranspilerException(string message, SyntaxNode? syntaxNode = null) : base(message)
    {
        SyntaxNode = syntaxNode;
    }

    public override string Message => base.Message + SyntaxNode?.GetLocationAndCodeErrorString();
}
