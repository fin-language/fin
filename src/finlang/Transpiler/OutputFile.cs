using Microsoft.CodeAnalysis;
using System.Text;

namespace finlang.Transpiler;

public class OutputFile
{
    public string? relativeFilePath;
    readonly public HashSet<string> fqnDependencies = new();
    public StringBuilder preIncludesSb = new();
    public HashSet<string> includesSet = new();
    public StringBuilder includesSb = new();
    public StringBuilder mainCodeSb = new();

    public OutputFile()
    {
    }

    public void WriteToFile(string destinationDirPath, string newLine)
    {
        relativeFilePath.ThrowIfNull();

        foreach (var include in includesSet)
        {
            var quoteChar = include.StartsWith("<") ? "" : "\"";
            includesSb.Append($"#include {quoteChar}{include}{quoteChar}{newLine}");
        }

        string path = Path.Combine(destinationDirPath, relativeFilePath);
        EnsureDirectoryExists(path);

        using EndLineTrackingWriter writer = new(path, newLine);
        writer.Write(preIncludesSb.ToString());
        writer.Write(newLine);
        writer.Write(includesSb.ToString());
        writer.Write($"{newLine}{newLine}");
        writer.Write(mainCodeSb.ToString());
    }

    private static void EnsureDirectoryExists(string path)
    {
        FileInfo fileInfo = new(path);
        fileInfo.Directory.ThrowIfNull().Create(); // Does nothing if the directory already exists.
    }

    internal void AddFqnDependency(ITypeSymbol type)
    {
        var fqn = Namer.GetFqn(type);
        if (fqn != "System.Void")
            fqnDependencies.Add(fqn);
    }
}
