using System.Text;

namespace finlang.Transpiler;

public class OutputFile
{
    public string? filePath;
    public StringBuilder sb = new();

    public OutputFile()
    {
    }
}
