﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace finlang.Transpiler;

public class C99ClsEnum
{
    readonly public INamedTypeSymbol symbol;
    readonly public SyntaxNode syntaxNode;
    readonly public SemanticModel model;

    readonly public OutputFile hFile = new();
    readonly public OutputFile cFile = new();

    public bool IsFFI { get; init; }
    public bool IsStaticClass { get; init; }

    public C99ClsEnum(SemanticModel model, SyntaxNode syntaxNode, INamedTypeSymbol symbol)
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

    public IEnumerable<IMethodSymbol> GetMethods()
    {
        return symbol.GetMembers().OfType<IMethodSymbol>();
    }

    public IEnumerable<IMethodSymbol> GetNonConstructorMethods()
    {
        return GetMethods().Where(m => m.MethodKind != MethodKind.Constructor);
    }

    public IEnumerable<IFieldSymbol> GetInstanceFields()
    {
        return symbol.GetMembers().OfType<IFieldSymbol>().Where(f => !f.IsConst && !f.IsStatic);
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
