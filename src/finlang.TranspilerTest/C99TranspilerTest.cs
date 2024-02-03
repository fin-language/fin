using finlang.Transpiler;
using FluentAssertions;

namespace finlang.TranspilerTest;

[Collection(nameof(ExSln2Fixture))]
public class C99TranspilerTest
{
    [Fact]
    public void Test1()
    {
        string slnPath = ExSln2Fixture.GetSlnPath();
        string destDirPath = ExSln2Fixture.GetSlnDir() + "/c99/";

        C99Transpiler transpiler = new(solutionPath: slnPath, destinationDirPath: destDirPath);
        transpiler.projectsToIgnore.Add("Tests");
        transpiler.GatherSolutionDeclarations();
        transpiler.c99ClassNodes.Count().Should().Be(3);

        transpiler.Generate();
        var ledCls = transpiler.c99ClassNodes.Single(c => c.GetFqn() == "hal.Led");
        string ledStructCode = ledCls._hFile.sb.ToString();
        ledStructCode.Should().Contain("typedef struct hal_Led hal_Led;");
        ledStructCode.Should().Contain("  hal_Gpio * _gpio;");

        var mainAppCls = transpiler.c99ClassNodes.Single(c => c.GetFqn() == "app.MainApp");
        string mainAppStructCode = mainAppCls._hFile.sb.ToString();
        mainAppStructCode.Should().Contain("typedef struct app_MainApp app_MainApp;");
        mainAppStructCode.Should().Contain("  uint32_t _toggle_at_ms;");
        mainAppStructCode.Should().Contain("  hal_Led * _redLed;");
    }
}

/*

Want to see the following files:
c99/
    App_MainApp.c/h
    Hal_Led.c/h

It should NOT generate a Hal_Gpio.c/h because that's an FFI class.
*/
