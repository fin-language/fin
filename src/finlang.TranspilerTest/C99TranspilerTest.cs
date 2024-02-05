using finlang.Transpiler;
using FluentAssertions;

namespace finlang.TranspilerTest;

[Collection(nameof(ExSln2Fixture))]
public class C99TranspilerTest
{
    string slnPath = ExSln2Fixture.GetSlnPath();
    string destDirPath = ExSln2Fixture.GetSlnDir() + "/c99/gen/";
    C99Transpiler transpiler;

    public C99TranspilerTest()
    {
        transpiler = new(solutionPath: slnPath, destinationDirPath: destDirPath);
    }

    [Fact]
    public void GenStruct()
    {
        transpiler.projectsToIgnore.Add("Tests");
        transpiler.GatherSolutionDeclarations();

        transpiler.Generate();
        var ledCls = transpiler.c99ClassEnum.Single(c => c.GetFqn() == "hal.Led");
        string ledStructCode = ledCls._hFile.mainCode.ToString();
        ledStructCode.Should().Contain("typedef struct hal_Led hal_Led;");
        ledStructCode.Should().Contain("  hal_Gpio * _gpio;");

        var mainAppCls = transpiler.c99ClassEnum.Single(c => c.GetFqn() == "app.MainApp");
        string mainAppStructCode = mainAppCls._hFile.mainCode.ToString();
        mainAppStructCode.Should().Contain("typedef struct app_MainApp app_MainApp;");
        mainAppStructCode.Should().Contain("  uint32_t _toggle_at_ms;");
        mainAppStructCode.Should().Contain("  hal_Led * _redLed;");
    }

    [Fact]
    public void GenerateFiles()
    {
        transpiler.GenerateAndWrite();
    }
}

/*

Want to see the following files:
c99/
    App_MainApp.c/h
    Hal_Led.c/h

It should NOT generate a Hal_Gpio.c/h because that's an FFI class.
*/
