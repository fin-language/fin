using finlang.Transpiler;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace finlang.test.TranspilerTest;

/// <summary>
/// Useful for creating a project or compilation from a code string. Great for testing.
/// </summary>
public class AdhocCodeHelper
{
    public Project CreateProjectForCode(string code)
    {
        AdhocWorkspace workspace = new();
        ProjectInfo projectInfo = ProjectInfo.Create(
            ProjectId.CreateNewId(),
            VersionStamp.Create(),
            "NewProject",
            "NewProject",
            LanguageNames.CSharp
        );
        Project project = workspace.AddProject(projectInfo);
        var document = workspace.AddDocument(project.Id, "NewFile.cs", SourceText.From(code));
        return project;
    }

    public Compilation GetCompilationForCode(string code)
    {
        Project project = CreateProjectForCode(code);
        return project.Documents.Single().Project.GetCompilationAsync().Result.ThrowIfNull();
    }
}
