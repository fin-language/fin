using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace finlang.Transpiler;

public class C99ClsEnum
{
    readonly public INamedTypeSymbol symbol;
    readonly public ClassDeclarationSyntax syntaxNode;
    readonly public SemanticModel model;

    readonly public HashSet<string> _fqnDependencies = new();
    readonly public OutputFile hFile = new();
    readonly public OutputFile cFile = new();

    public bool IsFFI { get; init; }

    public C99ClsEnum(SemanticModel model, ClassDeclarationSyntax syntaxNode, INamedTypeSymbol symbol)
    {
        this.IsFFI = symbol.GetAttributes().Any(a => a.AttributeClass?.Name == "ffiAttribute");
        this.syntaxNode = syntaxNode;
        this.symbol = symbol;
        this.model = model;
    }

    public string GetCName()
    {
        return C99Namer.GetCName(symbol);
    }

    public string GetFqn()
    {
        return C99Namer.GetFqn(symbol);
    }

    internal void AddFqnDependency(ITypeSymbol type)
    {
        var fqn = C99Namer.GetFqn(type);
        _fqnDependencies.Add(fqn);
    }
}
