namespace finlang.Transpiler;

public class StyleSettings
{
    /// <summary>
    /// Only partially used right now.
    /// </summary>
    public string indent = "    ";

    /// <summary>
    /// NOTE: doesn't override line endings found in the input file.
    /// This sets the line ending for new lines added by the transpiler.
    /// This is set to Environment.NewLine by default.
    /// You can set it to "\n" if you want to use Unix-style line endings.
    /// </summary>
    public string newLine = Environment.NewLine;
}
