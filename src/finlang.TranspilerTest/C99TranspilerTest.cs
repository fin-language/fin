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

        transpiler.c99DeclarationList.Count().Should().Be(2);
    }
}

/*

Want to see the following files:
c99/
    App_MainApp.c/h
    Hal_Led.c/h

It should NOT generate a Hal_Gpio.c/h because that's an FFI class.
*/
