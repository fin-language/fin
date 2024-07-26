using finlang;
using finlang.Transpiler;
using hal;
using System.Text.RegularExpressions;

string thisDir = PathHelpers.GetThisDir();

string slnDir = thisDir + "/../";
string slnName = "ExSln2.sln";
string outDir = slnDir + "/c99/gen";
string projectName = "LedBlinker";

Environment.SetEnvironmentVariable("FINLANG_TRANSPILER_DEBUG_TYPE", "hal.FuncPtrEx");

Console.WriteLine("Transpiling " + projectName + " fin/C# project...");

CTranspiler transpiler = new(destinationDirPath: outDir);

// disable output of info that adds git noise
transpiler.Options.OutputTimestamp = false;
transpiler.Options.OutputVersionInfo = false;
transpiler.Options.OutputChecksum = false;

transpiler.SetFileNamer((string originalPath) =>
{
    // Replace "mcu_XXX_" with "mcu/XXX/"
    originalPath = Regex.Replace(originalPath, "^mcu_([^_]+)_", "mcu/$1/");
    return originalPath;
});

transpiler.GenerateAndWrite(solutionPath: slnDir + slnName, projectName: projectName);
var fileNames = transpiler.GetListOfAllGeneratedFiles();

Console.WriteLine("Generated files:");
foreach (var fileName in fileNames)
{
    Console.WriteLine("    " + fileName);
}
Console.WriteLine("Done!");
