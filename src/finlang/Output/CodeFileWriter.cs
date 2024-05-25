// from StateSmith project
namespace finlang.Output;

public class CodeFileWriter : ICodeFileWriter
{
    public void WriteFile(string filePath, string code)
    {
        //consolePrinter.OutputStageMessage($"Writing to file `{pathPrinter.PrintPath(filePath)}`");
        File.WriteAllText(path:filePath, code);
    }
}

