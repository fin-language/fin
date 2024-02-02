using finlang.Transpiler;

namespace finlang.TranspilerTest;

[Collection(nameof(ExSln2TestLock))]
public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        C99Transpiler transpiler = new();
        string slnPath = PathHelpers.GetThisDir() + "../test/ExSln2/ExSln2.sln";
        transpiler.Transpile(slnPath);
    }
}
