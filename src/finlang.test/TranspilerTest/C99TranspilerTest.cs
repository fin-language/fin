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

        {
            var cls = transpiler.c99ClassesEnums.Single(c => c.GetFqn() == "hal.CArrayDependencyTest");
            string structCode = cls.hFile.mainCode.ToString();
            structCode.Should().Contain("  uint8_t * _data;");
            cls.hFile.fqnDependencies.Should().BeEquivalentTo("finlang.u8", "finlang.c_array");
        }

        {
            var ledCls = transpiler.c99ClassesEnums.Single(c => c.GetFqn() == "hal.Led");
            string ledStructCode = ledCls.hFile.mainCode.ToString();
            ledStructCode.Should().Contain("typedef struct hal_Led hal_Led;");
            ledStructCode.Should().Contain("  hal_IDigInOut * _dig_io;");
            ledCls.hFile.fqnDependencies.Should().Contain("hal.IDigInOut", "finlang.u8");
        }

        {
            var mainAppCls = transpiler.c99ClassesEnums.Single(c => c.GetFqn() == "app.Main");
            string mainAppStructCode = mainAppCls.hFile.mainCode.ToString();
            mainAppStructCode.Should().Contain("typedef struct app_Main app_Main;");
            mainAppStructCode.Should().Contain("  uint16_t period_ms;");
            mainAppStructCode.Should().Contain("  uint32_t _toggle_at_ms;");
            mainAppStructCode.Should().Contain("  hal_Led * _redLed;");
            mainAppCls.hFile.fqnDependencies.Should().Contain("hal.Led", "finlang.u32", "finlang.u16");
        }

        {
            var cls = transpiler.c99ClassesEnums.Single(c => c.GetFqn() == "hal.IDigIn");
            string structCode = cls.hFile.mainCode.ToString();
            //structCode.Should().Contain("  uint8_t * _data;");
            //cls.hFile.fqnDependencies.Should().BeEquivalentTo("finlang.u8");
        }
    }

    [Fact]
    public void GenerateFiles()
    {
        transpiler.GenerateAndWrite();

        transpiler.GetListOfAllGeneratedFiles().Should().Contain(
            "app_Main.h",
            "app_Main.c",
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
    App_Main.c/h
    Hal_Led.c/h

It should NOT generate a Hal_Gpio.c/h because that's an FFI class.
*/
