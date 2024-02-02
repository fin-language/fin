using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace finlang.Transpiler;

public class C99Class
{
    readonly public INamedTypeSymbol symbol;
    readonly public ClassDeclarationSyntax syntaxNode;
    readonly public SemanticModel model;

    //readonly public HashSet<C99Class> _dependencies = new();    // this is wrong, it should use FQN strings or syntax nodes
    readonly public OutputFile _hFile = new();
    readonly public OutputFile _cFile = new();

    public C99Class(SemanticModel model, ClassDeclarationSyntax syntaxNode, INamedTypeSymbol symbol)
    {
        this.syntaxNode = syntaxNode;
        this.symbol = symbol;
        this.model = model;
    }

    public string GetFqn()
    {
        return C99Namer.GetFqn(symbol);
    }
}
