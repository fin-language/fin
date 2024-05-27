namespace finlang.Output;

public class SimpleTextWriter : ITextWriter
{
    private readonly StreamWriter writer;

    public SimpleTextWriter(string path)
    {
        writer = new StreamWriter(path);
    }

    public void Dispose()
    {
        writer.Dispose();
    }

    public void Write(string value)
    {
        writer.Write(value);
    }
}