using Microsoft.CodeAnalysis;

namespace finlang.Transpiler;

public class SymbolHelper
{
    
    public static bool IsDerivedFrom(INamedTypeSymbol symbol, string baseTypeName)
    {
        INamedTypeSymbol? currentSymbol = symbol;

        while (currentSymbol != null)
        {
            if (currentSymbol.Name == baseTypeName)
                return true;

            currentSymbol = currentSymbol.BaseType;
        }

        return false;
    }
}
