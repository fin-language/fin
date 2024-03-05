using finlang;
using finlang.Transpiler;
using Microsoft.Extensions.FileSystemGlobbing;
using System.Text.RegularExpressions;

string thisDir = PathHelpers.GetThisDir();

string slnDir = PathHelpers.GetThisDir() + "/../";
string slnName = "ExSln3.sln";
string c99Dir = slnDir + "/c99";
string outDir = c99Dir + "/gen";
string projectName = "LedBlinker";

Console.WriteLine("Transpiling " + projectName + " fin/C# project...");

Transpiler transpiler = new(destinationDirPath: outDir, solutionPath: slnDir + slnName, projectName: projectName);

transpiler.Options.OutputTimestamp = false;

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


Console.WriteLine("Deploying code...");
Deploy(slnDir, c99Dir);
Console.WriteLine("Done!");

/////////////////////////////////////////////////////////

static void Deploy(string slnDir, string c99Dir)
{
    // copy selected files to deploy directory
    string deployDir = slnDir + "/deploy";
    string tang1Dir = deployDir + "/tang1";

    // erase old files
    Directory.Delete(deployDir, true);
    Directory.CreateDirectory(tang1Dir);

    // copy new files
    string[] filesToCopy = new string[] {
    "main.c",
    "board_tang_TangRev1_port_implementation.h",
    "board_tang_TangRev1_port_implementation.c",
    "mcu/avr8/mcu_avr8_Avr8Gpio_port_implementation.h",
    "mcu/avr8/mcu_avr8_Avr8Gpio_port_implementation.c",
};

    Matcher matcher = new();
    matcher.AddInclude("gen/**");
    matcher.AddExclude("gen/mcu/stm32/*");

    //PatternMatchingResult result = matcher.Match("file.md");

    foreach (var filePath in filesToCopy)
    {
        string src = c99Dir + "/" + filePath;
        string dst = tang1Dir + "/" + Path.GetFileName(filePath);
        File.Copy(src, dst);
    }

    foreach (var filePath in matcher.GetResultsInFullPath(c99Dir))
    {
        string dst = tang1Dir + "/" + Path.GetFileName(filePath);
        File.Copy(filePath, dst);
    }
}