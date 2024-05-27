using finlang.Output;

namespace finlang.Transpiler;

/// <summary>
/// Ensures that the file ends with a newline of the specified type.
/// https://github.com/fin-language/fin/issues/55
/// https://github.com/fin-language/fin/issues/52
/// </summary>
public class EndLineTrackingWriter : IDisposable
{
    protected bool endedWithNewLine = false;
    private ITextWriter writer;
    private string lineEnding;

    public EndLineTrackingWriter(string path, string lineEnding, ITextWriterFactory textWriterFactory)
    {
        writer = textWriterFactory.Create(path);
        this.lineEnding = lineEnding;
    }

    public void Dispose()
    {
        WriteEndLineIfNeeded();
        writer.Dispose();
    }

    public void Write(string value)
    {
        if (value.Length == 0)
            return;

        writer.Write(value);
        endedWithNewLine = value.EndsWith(lineEnding);
    }

    public void WriteEndLineIfNeeded()
    {
        if (!endedWithNewLine)
        {
            writer.Write(lineEnding);
            endedWithNewLine = true;
        }
    }
}
