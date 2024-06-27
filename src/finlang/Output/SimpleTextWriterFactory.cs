namespace finlang.Output;

public class SimpleTextWriterFactory : ITextWriterFactory
{
    public ITextWriter Create(string path)
    {
        return new SimpleTextWriter(path);
    }
}
