using finlang.Transpiler;
using FluentAssertions;

namespace finlang.TranspilerTest;

[Collection(nameof(ExSln2Fixture))]
public class C99TranspilerTest
{
    string slnPath = ExSln2Fixture.GetSlnPath();
    string destDirPath = ExSln2Fixture.GetSlnDir() + "/c99/gen/";
    Transpiler.Transpiler transpiler;

    public C99TranspilerTest()
    {
        transpiler = new(solutionPath: slnPath, destinationDirPath: destDirPath, projectName: "LedBlinker");
    }

    [Fact]
    public void GenStruct()
    {
        transpiler.GatherSolutionDeclarations();

        transpiler.Generate();
        var ledCls = transpiler.c99ClassesEnums.Single(c => c.GetFqn() == "hal.Led");
        string ledStructCode = ledCls.hFile.mainCode.ToString();
        ledStructCode.Should().Contain("typedef struct hal_Led hal_Led;");
        ledStructCode.Should().Contain("  hal_Gpio * _gpio;");
        ledCls.hFile.fqnDependencies.Should().BeEquivalentTo("hal.Gpio", "finlang.u8");

        var mainAppCls = transpiler.c99ClassesEnums.Single(c => c.GetFqn() == "app.MainApp");
        string mainAppStructCode = mainAppCls.hFile.mainCode.ToString();
        mainAppStructCode.Should().Contain("typedef struct app_MainApp app_MainApp;");
        mainAppStructCode.Should().Contain("  uint16_t period_ms;");
        mainAppStructCode.Should().Contain("  uint32_t _toggle_at_ms;");
        mainAppStructCode.Should().Contain("  hal_Led * _redLed;");
        mainAppCls.hFile.fqnDependencies.Should().BeEquivalentTo("hal.Led", "finlang.u32", "finlang.u16");
    }

    [Fact]
    public void GenerateFiles()
    {
        transpiler.GenerateAndWrite();

        transpiler.GetListOfAllGeneratedFiles().Should().Contain(
            "app_MainApp.h",
            "app_MainApp.c",
            "hal_Led.h",
            "hal_Led.c",
            "hal_Gpio.h",
            "app_Counter.h",
            "app_Counter.c",
            "hal_Helper.h",
            "hal_Helper.c",
            "hal_GpioPinState.h"
        );
    }
}

/*

Want to see the following files:
c99/
    App_MainApp.c/h
    Hal_Led.c/h

It should NOT generate a Hal_Gpio.c/h because that's an FFI class.
*/
