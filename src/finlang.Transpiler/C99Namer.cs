using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace finlang.Transpiler;

public class C99Namer
{
    SemanticModel model;

    public C99Namer(SemanticModel model)
    {
        this.model = model;
    }

    public static string MangleTypeSymbolName(string fullyQualifiedName)
    {
        string textName = fullyQualifiedName.Replace(oldChar: '.', newChar: '_');
        return textName;
    }

    public static string GetName(ISymbol symbol)
    {
        if (symbol is IMethodSymbol methodSymbol && methodSymbol.MethodKind == MethodKind.Constructor)
        {
            return "ctor";
        }
        return symbol.Name;
    }

    public static string GetFqn(ISymbol symbol)
    {
        var parts = new List<string>();

        parts.Insert(index: 0, GetName(symbol));
        symbol = symbol.ContainingSymbol;

        while (symbol != null)
        {
            if (symbol is INamespaceSymbol namespaceSymbol)
            {
                // need to stop at global namespace to prevent ascending into dll
                if (namespaceSymbol.IsGlobalNamespace)
                {
                    break;
                }
            }

            if (symbol is not IMethodSymbol)
                parts.Insert(index: 0, GetName(symbol));

            symbol = symbol.ContainingSymbol;
        }

        var fqn = string.Join(".", parts);
        return fqn;
    }

    public string GetCName(ClassDeclarationSyntax node)
    {
        INamedTypeSymbol symbol = (INamedTypeSymbol)model.GetDeclaredSymbol(node).ThrowIfNull();
        return GetCName(symbol);
    }

    public static string GetCName(SemanticModel model, ClassDeclarationSyntax node)
    {
        INamedTypeSymbol symbol = (INamedTypeSymbol)model.GetDeclaredSymbol(node).ThrowIfNull();
        return GetCName(symbol);
    }

    public string GetCName(StructDeclarationSyntax node)
    {
        INamedTypeSymbol symbol = (INamedTypeSymbol)model.GetDeclaredSymbol(node).ThrowIfNull();
        return GetCName(symbol);
    }

    public string GetCName(EnumDeclarationSyntax node)
    {
        INamedTypeSymbol symbol = (INamedTypeSymbol)model.GetDeclaredSymbol(node).ThrowIfNull();
        return GetCName(symbol);
    }

    public static string GetCName(ISymbol symbol)
    {
        switch (symbol.Name)
        {
            case "u8":
                return "uint8_t";
            case "u16":
                return "uint16_t";
            case "u32":
                return "uint32_t";
            case "u64":
                return "uint64_t";
            case "i8":
                return "int8_t";
            case "i16":
                return "int16_t";
            case "i32":
                return "int32_t";
            case "i64":
                return "int64_t";
            case "f32":
                return "float";
            case "f64":
                return "double";
            case "bool":
                return "bool";
            case "string":
                return "char*";
        }

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

        var fqn = GetFqn(symbol);
        var name = MangleTypeSymbolName(fqn);
        return name;
    }

    public string GetCName(SymbolInfo symbolInfo)
    {
        return GetCName(symbolInfo.Symbol.ThrowIfNull());
    }
}
