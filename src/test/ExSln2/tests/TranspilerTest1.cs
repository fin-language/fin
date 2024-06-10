using finlang.Output;
using finlang.Transpiler;
using FluentAssertions;
using hal;

namespace Tests;

public class TranspilerTest1
{
    [Fact]
    public void GetCTypeNameFromFinType_Test()
    {
        string thisDir = PathHelpers.GetThisDir();
        string slnDir = thisDir + "/../";
        string slnName = "ExSln2.sln";
        string outDir = slnDir + "/c99/gen";
        string projectName = "LedBlinker";

        CTranspiler transpiler = new(destinationDirPath: outDir);
        CapturingTextWriterFactory capturingTextWriterFactory = new();
        transpiler.textWriterFactory = capturingTextWriterFactory;
        transpiler.Options.DeleteOutputDirBeforeTranspile = false;  // we don't want to wipe out real generated files for this test
        transpiler.GenerateAndWrite(solutionPath: slnDir + slnName, projectName: projectName);

        transpiler.GetCTypeNameFromFinType<Led>().Should().Be("hal_Led");
    }
}
