using finlang.Transpiler;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace finlang.test.TranspilerTest;

/// <summary>
/// Useful for creating a project or compilation from a code string. Great for testing.
/// </summary>
public class AdhocCodeHelper
{
    public static Project CreateProjectForCode(params string[] sourceFilesContents)
    {
        AdhocWorkspace workspace = new();
        List<DocumentInfo> documents = [];

        int i = 0;
        foreach (var code in sourceFilesContents)
        {
            string fileName = $"CodeFile{i}.cs";
            documents.Add(DocumentInfo.Create(
                id: DocumentId.CreateNewId(ProjectId.CreateNewId()),
                name: fileName,
                loader: TextLoader.From(TextAndVersion.Create(SourceText.From(code), VersionStamp.Create())),
                filePath: fileName));
        }

        ProjectInfo projectInfo = ProjectInfo.Create(
            id: ProjectId.CreateNewId(),
            version: VersionStamp.Create(),
            name: "NewProject",
            assemblyName: "NewProject",
            language: LanguageNames.CSharp,
            documents: documents
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
