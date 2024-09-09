using finlang.Output;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;

namespace finlang.Transpiler;

public class C99ClsEnumInterface
{
    readonly public INamedTypeSymbol symbol;
    private readonly ITextWriterFactory textWriterFactory;
    readonly public SyntaxNode syntaxNode;
    readonly public SemanticModel model;

    readonly public OutputFile hFile;
    readonly public OutputFile cFile;

    private bool hasVTable = false;
    public bool HasVTable => hasVTable;

    public bool IsFFIClass { get; init; }
    private bool hasFFIMethod = false;
    public bool HasFFIMethod => hasFFIMethod;

    public bool IsStaticClass { get; init; }

    public C99ClsEnumInterface(SemanticModel model, SyntaxNode syntaxNode, INamedTypeSymbol symbol, ITextWriterFactory textWriterFactory)
    {
        this.IsFFIClass = symbol.IsFFI();
        this.syntaxNode = syntaxNode;
        this.symbol = symbol;
        this.textWriterFactory = textWriterFactory;
        this.model = model;
        hFile = new(textWriterFactory);
        cFile = new(textWriterFactory);

        this.IsStaticClass = GetInstanceFields().Any() == false;

        var inludeAttributes = symbol.GetAttributes().Where(a => a.AttributeClass?.Name == nameof(add_includeAttribute));
        foreach (var attribute in inludeAttributes)
        {
            var include = (string)attribute.ConstructorArguments[0].Value.ThrowIfNull();
            hFile.includesSet.Add(include);
        }
    }

    public void SetHasFFIMethod()
    {
        hasFFIMethod = true;
    }

    public void SetHasVTable()
    {
        hasVTable = true;
    }

    public bool HasCFile()
    {
        if (IsFFIClass && !hasVTable)
            return false;

        return !IsEnum;
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

    // get delegates defined in the class
    public IEnumerable<INamedTypeSymbol> GetDelegateDefinitions()
    {
        return GetMembers().OfType<INamedTypeSymbol>().Where(m => m.TypeKind == TypeKind.Delegate);
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

    public bool HasUserDeclaredConstructor()
    {
        return syntaxNode.ChildNodes().OfType<ConstructorDeclarationSyntax>().Any(); // we don't use descendants because we don't want to include constructors of nested classes
    }

    public bool NeedsDefaultConstructor()
    {
        return !IsFFIClass && !HasUserDeclaredConstructor() && !IsStaticClass;
    }

    public IEnumerable<IFieldSymbol> GetInstanceFields()
    {
        return GetMembers().OfType<IFieldSymbol>().Where(f => !f.IsConst && !f.IsStatic);
    }

    public IEnumerable<IFieldSymbol> GetCDefineFields()
    {
        return GetMembers().OfType<IFieldSymbol>().Where(f => f.IsConst || (f.IsStatic && f.IsReadOnly));
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
        if (type is INamedTypeSymbol namedType && (namedType.IsCArray() || namedType.IsCArrayMem()))
        {
            type = namedType.TypeArguments.Single();
        }

        hFile.AddFqnDependency(type);
    }

    public string GetSourceFilePath()
    {
        return syntaxNode.SyntaxTree.FilePath;
    }
}
