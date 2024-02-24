using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace finlang.Transpiler;

public class Namer
{
    SemanticModel model;

    public Namer(SemanticModel model)
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
        List<string> parts = GetFqnParts(symbol);
        var fqn = string.Join(".", parts);
        return fqn;
    }

    public static List<string> GetFqnParts(ISymbol symbol)
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

        return parts;
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
        var fqn = GetFqn(symbol);

        switch (fqn)
        {
            case "finlang.u8": return "uint8_t";
            case "finlang.u16": return "uint16_t";
            case "finlang.u32": return "uint32_t";
            case "finlang.u64": return "uint64_t";
            case "finlang.i8": return "int8_t";
            case "finlang.i16": return "int16_t";
            case "finlang.i32": return "int32_t";
            case "finlang.i64": return "int64_t";
            case "finlang.f32": return "float";
            case "finlang.f64": return "double";
            case "finlang.bool": return "bool";
            case "System.Boolean": return "bool";
            case "System.Void": return "void";
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

        if (symbol is IMethodSymbol methodSymbol)
        {
            string prefix = GetMethodNamePrefixForPrivate(methodSymbol);

            return prefix + MangleTypeSymbolName(fqn);
        }

        var name = MangleTypeSymbolName(fqn);
        return name;
    }

    public static string GetMethodNamePrefixForPrivate(IMethodSymbol methodSymbol)
    {
        string prefix = "";
        if (methodSymbol.Name.StartsWith("_") || methodSymbol.DeclaredAccessibility != Accessibility.Public)
        {
            prefix = "PRIVATE_";
        }

        return prefix;
    }

    public string GetCName(SymbolInfo symbolInfo)
    {
        return GetCName(symbolInfo.Symbol.ThrowIfNull());
    }
}
