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

    public List<C99Declaration> c99DeclarationList = new();

    public List<string> projectsToIgnore = new();

    public C99Transpiler(string destinationDirPath, string solutionPath)
    {
        this.destinationDirPath = destinationDirPath;
        this.solutionPath = solutionPath;
    }

    public void GatherDeclarationsForProject(Project project)
    {
        Compilation compilation = project.GetCompilationAsync().Result.ThrowIfNull();

        foreach (var syntaxTree in compilation.SyntaxTrees)
        {
            var fileName = Path.GetFileName(syntaxTree.FilePath);
            var model = compilation.GetSemanticModel(syntaxTree);
            var root = syntaxTree.GetRoot();

            // Find all declarations in the syntax tree
            var allClasses = model.SyntaxTree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>();

            foreach (var classDeclNode in allClasses)
            {
                INamedTypeSymbol symbol = model.GetDeclaredSymbol(classDeclNode).ThrowIfNull();

                if (SymbolHelper.IsDerivedFrom(symbol, "FinObj"))
                {
                    var c99Decl = new C99Declaration(classDeclNode);
                    c99DeclarationList.Add(c99Decl);
                }
            }
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
}