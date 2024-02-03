using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;
using System.Xml.Linq;

namespace finlang.Transpiler;

public class C99Transpiler
{
    private string destinationDirPath;
    private string solutionPath;
    public readonly StringBuilder hFileSb = new();
    public readonly StringBuilder cFileSb = new();

    public List<C99Class> c99ClassNodes = new();

    public List<string> projectsToIgnore = new();

    public C99Transpiler(string destinationDirPath, string solutionPath)
    {
        this.destinationDirPath = destinationDirPath;
        this.solutionPath = solutionPath;
    }

    public void GatherDeclarationsForProject(Project project)
    {
        Compilation compilation = project.GetCompilationAsync().Result.ThrowIfNull();
        ThrowAnyDiagnosticError(compilation.GetDiagnostics(), "");

        foreach (var syntaxTree in compilation.SyntaxTrees)
        {
            var fileName = Path.GetFileName(syntaxTree.FilePath);
            var model = compilation.GetSemanticModel(syntaxTree);
            var root = syntaxTree.GetRoot();

            // Find all class declarations in the syntax tree
            var allClasses = model.SyntaxTree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>();

            foreach (var classDeclNode in allClasses)
            {
                INamedTypeSymbol symbol = model.GetDeclaredSymbol(classDeclNode).ThrowIfNull();

                if (SymbolHelper.IsDerivedFrom(symbol, "FinObj"))
                {
                    var c99Decl = new C99Class(model, classDeclNode, symbol);
                    c99ClassNodes.Add(c99Decl);
                }
            }
        }
    }

    internal static void ThrowAnyDiagnosticError(IEnumerable<Diagnostic> enumerable, string programText)
    {
        var errors = enumerable.Where(d => d.Severity == DiagnosticSeverity.Error);

        var message = "";

        foreach (var error in errors)
        {
            message += error.ToString() + "\n";
        }

        if (message.Length > 0)
        {
            throw new TranspilerException(message, programText);
        }
    }

    public void GatherSolutionDeclarations()
    {
        Solution sln = SolutionLoader.Load(solutionPath);

        var targetProjects = sln.Projects.Where(p => !projectsToIgnore.Contains(p.Name));

        foreach (var project in targetProjects)
        {
            GatherDeclarationsForProject(project);
        }
    }

    public void Generate()
    {
        foreach (var cls in c99ClassNodes)
        {
            C99Namer namer = new(cls.model);
            C99StructGenerator gen = new(cls.model, namer);

            gen.GenerateStruct(cls);
        }
    }
}