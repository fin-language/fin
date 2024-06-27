namespace finlang;

/// <summary>
/// Provides ways of customizing the finlang C transpiler
/// </summary>
public class FinC
{
    /// <summary>
    /// Generates C code that prevents an unused variable warning.
    /// </summary>
    /// <param name="_"></param>
    public static void ignore_unused(object _)
    {
        // do nothing
    }

    public static T EchoToC<T>(T value)
    {
        return value;
    }
}
