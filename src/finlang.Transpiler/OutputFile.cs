using System.Text;

namespace finlang.Transpiler;

public class OutputFile
{
    public string? relativeFilePath;
    public StringBuilder preIncludes = new();
    public StringBuilder includes = new();
    public StringBuilder mainCode = new();

    public OutputFile()
    {
    }

    public void WriteToFile(string destinationDirPath)
    {
        relativeFilePath.ThrowIfNull();
        
        using StreamWriter sw = new(Path.Combine(destinationDirPath, relativeFilePath));
        sw.Write(preIncludes.ToString());
        sw.Write("\n");
        sw.Write(includes.ToString());
        sw.Write("\n\n");
        sw.Write(mainCode.ToString());
    }
}
