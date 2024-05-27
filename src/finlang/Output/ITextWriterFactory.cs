
namespace finlang.Output;

public interface ITextWriterFactory
{
    ITextWriter Create(string path);
}
