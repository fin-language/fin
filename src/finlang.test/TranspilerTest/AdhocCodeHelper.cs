using finlang.Transpiler;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace finlang.test.TranspilerTest;

/// <summary>
/// Useful for creating a project or compilation from a code string. Great for testing.
/// </summary>
public class AdhocCodeHelper
{
    public static Project CreateProjectForCode(string code)
    {
        AdhocWorkspace workspace = new();
        ProjectInfo projectInfo = ProjectInfo.Create(
            id: ProjectId.CreateNewId(),
            version: VersionStamp.Create(),
            name: "NewProject",
            assemblyName: "NewProject",
            language: LanguageNames.CSharp,
            documents: new[] {
                DocumentInfo.Create(
                id: DocumentId.CreateNewId(ProjectId.CreateNewId()),
                name: "NewFile.cs",
                loader: TextLoader.From(TextAndVersion.Create(SourceText.From(code), VersionStamp.Create())),
                filePath: "NewFile.cs")
            }
        );
        Project project = workspace.AddProject(projectInfo);
        project = CTranspiler.AdjustProjectForTranspilation(project);
        return project;
    }

    public Compilation GetCompilationForCode(string code)
    {
        Project project = CreateProjectForCode(code);
        return project.Documents.Single().Project.GetCompilationAsync().Result.ThrowIfNull();
    }
}
