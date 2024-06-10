using finlang.Transpiler;
using FluentAssertions;
using System.Text.RegularExpressions;

namespace finlang.TranspilerTest;

[Collection(nameof(ExSln2Fixture))]
public class C99TranspilerTest
{
    string slnPath = ExSln2Fixture.GetSlnPath();
    string destDirPath = ExSln2Fixture.GetSlnDir() + "/c99/gen/";
    Transpiler.CTranspiler transpiler;

    public C99TranspilerTest()
    {
        transpiler = new(destinationDirPath: destDirPath);
    }

    [Fact]
    public void GenStruct()
    {
        transpiler.GatherSolutionDeclarations(solutionPath: slnPath, projectName: "LedBlinker");

        transpiler.Generate();

        {
            var cls = transpiler.c99ClassesEnums.Single(c => c.GetFqn() == "hal.CArrayDependencyTest");
            string structCode = cls.hFile.mainCodeSb.ToString();
            structCode.Should().Contain("  uint8_t * _data;");
            cls.hFile.fqnDependencies.Should().BeEquivalentTo("finlang.u8", "finlang.c_array");
        }

        {
            var ledCls = transpiler.c99ClassesEnums.Single(c => c.GetFqn() == "hal.Led");
            string ledStructCode = ledCls.hFile.mainCodeSb.ToString();
            ledStructCode.Should().Contain("typedef struct hal_Led hal_Led;");
            ledStructCode.Should().Contain("  hal_IDigInOut * _dig_io;");
            ledCls.hFile.fqnDependencies.Should().Contain("hal.IDigInOut", "finlang.u8");
        }

        {
            var mainAppCls = transpiler.c99ClassesEnums.Single(c => c.GetFqn() == "app.Main");
            string mainAppStructCode = mainAppCls.hFile.mainCodeSb.ToString();
            mainAppStructCode.Should().Contain("typedef struct app_Main app_Main;");
            mainAppStructCode.Should().Contain("  uint16_t period_ms;");
            mainAppStructCode.Should().Contain("  uint32_t _toggle_at_ms;");
            mainAppStructCode.Should().Contain("  hal_Led * _redLed;");
            mainAppCls.hFile.fqnDependencies.Should().Contain("hal.Led", "finlang.u32", "finlang.u16");
        }

        {
            var cls = transpiler.c99ClassesEnums.Single(c => c.GetFqn() == "hal.IDigIn");
            string structCode = cls.hFile.mainCodeSb.ToString();
            //structCode.Should().Contain("  uint8_t * _data;");
            //cls.hFile.fqnDependencies.Should().BeEquivalentTo("finlang.u8");
        }
    }

    [Fact]
    public void GenerateFiles()
    {
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

        transpiler.GenerateAndWrite(solutionPath: slnPath, projectName: "LedBlinker");

        //transpiler.GetListOfAllGeneratedFiles().Should().Contain(
        //    "app_Main.h",
        //    "app_Main.c",
        //    "hal_Led.h",
        //    "hal_Led.c",
        //    "hal_Gpio.h",
        //    "app_Counter.h",
        //    "app_Counter.c",
        //    "hal_Helper.h",
        //    "hal_Helper.c",
        //    "hal_GpioPinState.h"
        //);
    }
}