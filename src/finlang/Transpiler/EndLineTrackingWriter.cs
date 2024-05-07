namespace finlang.Transpiler;

public class EndLineTrackingWriter : IDisposable
{
    protected bool endedWithNewLine = false;
    private StreamWriter writer;
    private string lineEnding;

    public EndLineTrackingWriter(string path, string lineEnding)
    {
        writer = new StreamWriter(path);
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
