using finlang;
using finlang.Transpiler;
using System.Text.RegularExpressions;

string thisDir = PathHelpers.GetThisDir();

string slnDir = PathHelpers.GetThisDir() + "/../";
string slnName = "ExSln2.sln";
string outDir = slnDir + "/c99/gen";
string projectName = "LedBlinker";

Console.WriteLine("Transpiling " + projectName + " fin/C# project...");

Transpiler transpiler = new(destinationDirPath: outDir, solutionPath: slnDir + slnName, projectName: projectName);

transpiler.SetFileNamer((string originalPath) =>
{
    // Replace "mcu_XXX_" with "mcu/XXX/"
    originalPath = Regex.Replace(originalPath, "^mcu_([^_]+)_", "mcu/$1/");
    return originalPath;
});

transpiler.GenerateAndWrite();
var fileNames = transpiler.GetListOfAllGeneratedFiles();

Console.WriteLine("Generated files:");
foreach (var fileName in fileNames)
{
    Console.WriteLine("    " + fileName);
}
Console.WriteLine("Done!");
