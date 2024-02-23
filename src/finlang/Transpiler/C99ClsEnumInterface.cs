using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;

namespace finlang.Transpiler;

public class C99ClsEnumInterface
{
    readonly public INamedTypeSymbol symbol;
    readonly public SyntaxNode syntaxNode;
    readonly public SemanticModel model;

    readonly public OutputFile hFile = new();
    readonly public OutputFile cFile = new();

    public bool IsFFI { get; init; }
    public bool IsStaticClass { get; init; }

    public C99ClsEnumInterface(SemanticModel model, SyntaxNode syntaxNode, INamedTypeSymbol symbol)
    {
        this.IsFFI = symbol.GetAttributes().Any(a => a.AttributeClass?.Name == "ffiAttribute");
        this.syntaxNode = syntaxNode;
        this.symbol = symbol;
        this.model = model;

        this.IsStaticClass = GetInstanceFields().Any() == false;
    }

    public bool HasCFile()
    {
        return !IsFFI && !IsEnum && !IsEnum;
    }

    public bool IsEnum
    {
        get
        {
            return syntaxNode is EnumDeclarationSyntax;
        }
    }

    public bool IsInterface
    {
        get
        {
            return syntaxNode is InterfaceDeclarationSyntax;
        }
    }

    public bool IsClass
    {
        get
        {
            return syntaxNode is ClassDeclarationSyntax;
        }
    }

    public IEnumerable<IMethodSymbol> GetMethods()
    {
        return GetMembers().OfType<IMethodSymbol>();
    }

    public IEnumerable<ISymbol> GetMembers()
    {
        IEnumerable<ISymbol> members = symbol.GetMembers();

        // select members that don't have the [simonly] attribute
        members = members.Where(m => !m.IsSimOnly());

        return members;
    }

    public IEnumerable<IMethodSymbol> GetNonConstructorMethods()
    {
        return GetMethods().Where(m => m.MethodKind != MethodKind.Constructor);
    }

    public IEnumerable<IFieldSymbol> GetInstanceFields()
    {
        return GetMembers().OfType<IFieldSymbol>().Where(f => !f.IsConst && !f.IsStatic);
    }

    public string GetCName()
    {
        return Namer.GetCName(symbol);
    }

    public string GetFqn()
    {
        return Namer.GetFqn(symbol);
    }

    internal void AddHeaderFqnDependency(ITypeSymbol type)
    {
        // if the type is a c_array, we need to add the dependency of the type it contains
        if (type is INamedTypeSymbol namedType && namedType.Name == "c_array")
        {
            type = namedType.TypeArguments.Single();
        }

        hFile.AddFqnDependency(type);
    }
}
