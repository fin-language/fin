using System.Text;

namespace finlang.Output;

public class CapturingTextWriter : ITextWriter
{
    public StringBuilder CapturedText = new();
    public string path;

    public CapturingTextWriter(string path)
    {
        this.path = path;
    }

    public void Write(string value)
    {
        CapturedText.Append(value);
    }

    public void Dispose()
    {
    }
}
