using System.Diagnostics.CodeAnalysis;

namespace finlang.Transpiler;

internal static class NullHelper
{
    public static T ThrowIfNull<T>([NotNull] this T? value, string valueExpression = "")
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value), valueExpression);

        return value;
    }
}
