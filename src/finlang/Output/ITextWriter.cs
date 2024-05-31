
namespace finlang.Output;

public interface ITextWriter : IDisposable
{
    void Write(string value);
}
