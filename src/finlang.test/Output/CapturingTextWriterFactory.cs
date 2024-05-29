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

    public string GetSingleWriterTextByFileName(string fileName)
    {
        var key = writers.GetKeys().Single(x => Path.GetFileName(x) == fileName);
        return writers.GetValues(key).Single().CapturedText.ToString();
    }

    /// <summary>
    /// Only call if you expect there to be a single writer.
    /// </summary>
    /// <returns></returns>
    public string GetSingleWriterText()
    {
        return writers.GetValues().Single().Single().CapturedText.ToString();
    }
}
