using finlang.Output;

namespace finlang.test.Output;

public class CapturingTextWriterFactory : ITextWriterFactory
{
    public HashList<string, CapturingTextWriter> writers = new();

    public ITextWriter Create(string path)
    {
        var writer = new CapturingTextWriter(path);
        writers.Add(path, writer);
        return writer;
    }
}
