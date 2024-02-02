using finlang.Transpiler;
using Microsoft.CodeAnalysis;
using FluentAssertions;

namespace finlang.TranspilerTest;

[Collection(nameof(ExSln2TestLock))]
public class SolutionLoaderTest
{
    [Fact]
    public void Test1()
    {
        Solution solution = SolutionLoader.Load(PathHelpers.GetThisDir() + "../test/ExSln2/ExSln2.sln");
        solution.Projects.Count().Should().Be(3);
    }
}
