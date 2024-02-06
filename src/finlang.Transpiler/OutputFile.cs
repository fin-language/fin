using Microsoft.CodeAnalysis;
using System.Text;

namespace finlang.Transpiler;

public class OutputFile
{
    public string? relativeFilePath;
    readonly public HashSet<string> fqnDependencies = new();
    public StringBuilder preIncludes = new();
    public HashSet<string> includes = new();
    public StringBuilder includesSb = new();
    public StringBuilder mainCode = new();

    public OutputFile()
    {
    }

    public void WriteToFile(string destinationDirPath)
    {
        relativeFilePath.ThrowIfNull();

        foreach (var include in includes)
        {
            includesSb.AppendLine("#include " + include + "");
        }
        
        using StreamWriter sw = new(Path.Combine(destinationDirPath, relativeFilePath));
        sw.Write(preIncludes.ToString());
        sw.Write("\n");
        sw.Write(includesSb.ToString());
        sw.Write("\n\n");
        sw.Write(mainCode.ToString());
    }

    internal void AddFqnDependency(ITypeSymbol type)
    {
        var fqn = Namer.GetFqn(type);
        if (fqn != "System.Void")
            fqnDependencies.Add(fqn);
    }
}
