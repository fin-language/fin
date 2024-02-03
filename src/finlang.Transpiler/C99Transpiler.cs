using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;
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

    static List<MetadataReference> GetAssemblies()
    {
        //// Add necessary NuGet package references
        var assemblies2 = new List<MetadataReference>
        {
            //MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            //MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
            //MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
            //MetadataReference.CreateFromFile(typeof(FinObj).Assembly.Location), // finlang
            //MetadataReference.CreateFromFile(typeof(System.Runtime.AssemblyTargetedPatchBandAttribute).Assembly.Location),
            // FIXME we still get this error: ExSln2\hal\Led.cs(5,20): error CS0012: The type 'Object' is defined in an assembly that is not referenced. You must add a reference to assembly 'System.Runtime, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'.
        };

        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in assemblies)
        {
            if (!assembly.IsDynamic && !string.IsNullOrWhiteSpace(assembly.Location))
            {
                assemblies2.Add(MetadataReference.CreateFromFile(assembly.Location));
            }
        }

        return assemblies2;
    }


    public void GatherDeclarationsForProject(Project project)
    {
        project = project.AddMetadataReferences(GetAssemblies());
        Compilation compilation = project.GetCompilationAsync().Result.ThrowIfNull();

        // FIXME! Put back in
        ThrowAnyDiagnosticError(compilation.GetDiagnostics(), "");

        foreach (var syntaxTree in compilation.SyntaxTrees)
        {
            var fileName = Path.GetFileName(syntaxTree.FilePath);
            var model = compilation.GetSemanticModel(syntaxTree);
            var root = syntaxTree.GetRoot();
            //ThrowAnyDiagnosticError(model.GetDiagnostics(), "");

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
            message += "\n" + error.ToString();
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