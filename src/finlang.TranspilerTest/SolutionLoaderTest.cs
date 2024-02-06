using finlang.Transpiler;
using Microsoft.CodeAnalysis;
using FluentAssertions;

namespace finlang.TranspilerTest;

[Collection(nameof(ExSln2Fixture))]
public class SolutionLoaderTest
{
    [Fact]
    public void LoadSolution()
    {
        Solution solution = WorkspaceLoader.LoadSolution(PathHelpers.GetThisDir() + "../test/ExSln2/ExSln2.sln");
        solution.Projects.Count().Should().Be(3);
    }

    [Fact]
    public void LoadProject()
    {
        Project project = WorkspaceLoader.LoadProject(PathHelpers.GetThisDir() + "../test/ExSln2/LedBlinker/LedBlinker.csproj");
        project.Name.Should().Be("LedBlinker");
    }
}
