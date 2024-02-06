using finlang;
using finlang.Transpiler;

string thisDir = PathHelpers.GetThisDir();

string slnDir = PathHelpers.GetThisDir() + "/../";
string slnName = "ExSln2.sln";
string outDir = slnDir + "/c99/gen";
string projectName = "LedBlinker";

Console.WriteLine("Transpiling " + projectName + " fin/C# project...");

Transpiler transpiler = new(destinationDirPath: outDir, solutionPath: slnDir + slnName, projectName: projectName);

transpiler.GenerateAndWrite();
var fileNames = transpiler.GetListOfAllGeneratedFiles();

Console.WriteLine("Generated files:");
foreach (var fileName in fileNames)
{
    Console.WriteLine("    " + fileName);
}
Console.WriteLine("Done!");
