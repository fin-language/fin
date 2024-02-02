using Microsoft.CodeAnalysis;

namespace finlang.Transpiler;

public class C99Declaration
{
    readonly public SyntaxNode _syntaxNode;

    readonly public HashSet<C99Declaration> _dependencies = new();
    readonly public OutputFile _hFile = new();
    readonly public OutputFile _cFile = new();

    public C99Declaration(SyntaxNode syntaxNode)
    {
        _syntaxNode = syntaxNode;
    }
}
