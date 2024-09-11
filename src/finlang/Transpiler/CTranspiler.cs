using finlang.Output;
using finlang.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;
using System.Security.Cryptography;

namespace finlang.Transpiler;

public class CTranspiler
{
    /// <summary>
    /// Doesn't need to include FQN right now
    /// </summary>
    public const string ENV_VAR_TRANSPILER_DEBUG_TYPE = "FINLANG_TRANSPILER_DEBUG_TYPE";

    /// <summary>
    /// https://github.com/fin-language/fin/issues/62
    /// </summary>
    public string? selectClassWhenDebugging = null;
    public TranspilerOptions Options = new();
    public List<C99ClsEnumInterface> c99ClassesEnums = new();
    public Dictionary<string, C99ClsEnumInterface> fqnToC99Class = new();

    protected string NL => Options.StyleSettings.newLine;

    public ITextWriterFactory textWriterFactory = new SimpleTextWriterFactory();

    private string destinationDirPath;
    private Func<string, string> fileNamer = (string s) => s;

    public CTranspiler(string destinationDirPath)
    {
        this.destinationDirPath = destinationDirPath;
        selectClassWhenDebugging ??= Environment.GetEnvironmentVariable(ENV_VAR_TRANSPILER_DEBUG_TYPE);
    }

    public void SetFileNamer(Func<string, string> fileNamer)
    {
        this.fileNamer = fileNamer;
    }

    static List<MetadataReference> GetAssemblies()
    {
        // Add necessary NuGet package references
        // We get the finobj reference from the current assembly
        var targetAssemblies = new List<MetadataReference>();

        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in assemblies)
        {
            if (!assembly.IsDynamic && !string.IsNullOrWhiteSpace(assembly.Location))
            {
                targetAssemblies.Add(MetadataReference.CreateFromFile(assembly.Location));
            }
        }

        return targetAssemblies;
    }

    /// <summary>
    /// Classes or enums (not done yet)
    /// </summary>
    /// <param name="project"></param>
    public void GatherDeclarationsForProject(Project project)
    {
        project = AdjustProjectForTranspilation(project);
        Compilation compilation = project.GetCompilationAsync().Result.ThrowIfNull();

        ThrowAnyDiagnosticError(compilation.GetDiagnostics());

        foreach (var syntaxTree in compilation.SyntaxTrees)
        {
            //var fileName = Path.GetFileName(syntaxTree.FilePath);
            var model = compilation.GetSemanticModel(syntaxTree);

            FindAllClasses(model);
            FindAllEnums(model);
            FindAllInterfaces(model);
        }
    }

    public static Project AdjustProjectForTranspilation(Project project)
    {
        // remove finlang.csproj reference for test projects otherwise we get errors while running our tests
        var toRemove = project.ProjectReferences.Where(pr => pr.ProjectId.ToString().Contains("finlang.csproj")).ToList();
        foreach (var pr in toRemove)
        {
            project = project.RemoveProjectReference(pr);
        }

        project = project.AddMetadataReferences(GetAssemblies());
        return project;
    }

    private void FindAllEnums(SemanticModel model)
    {
        var allEnums = model.SyntaxTree.GetRoot().DescendantNodes().OfType<EnumDeclarationSyntax>();

        foreach (var enumDeclNode in allEnums)
        {
            INamedTypeSymbol symbol = model.GetDeclaredSymbol(enumDeclNode).ThrowIfNull();
            
            // could check for [simonly] attribute
            {
                var c99Decl = new C99ClsEnumInterface(model, enumDeclNode, symbol, textWriterFactory);
                c99ClassesEnums.Add(c99Decl);
                fqnToC99Class.Add(c99Decl.GetFqn(), c99Decl);
            }
        }
    }

    // TODO profile here. might need to handle one class at a time to avoid memory issues
    private void FindAllClasses(SemanticModel model)
    {
        var allClasses = model.SyntaxTree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>();

        foreach (var classDeclNode in allClasses)
        {
            INamedTypeSymbol symbol = model.GetDeclaredSymbol(classDeclNode).ThrowIfNull();

            if (IsIgnoredDuringDebugging(symbol))
                continue;

            if (SymbolHelper.IsDerivedFrom(symbol, nameof(FinObj)) && !symbol.IsSimOnly())
            {
                var c99Decl = new C99ClsEnumInterface(model, classDeclNode, symbol, textWriterFactory);
                c99ClassesEnums.Add(c99Decl);
                fqnToC99Class.Add(c99Decl.GetFqn(), c99Decl);
            }
        }
    }

    private bool IsIgnoredDuringDebugging(INamedTypeSymbol symbol)
    {
        if (selectClassWhenDebugging == null || selectClassWhenDebugging == "null")
            return false;

        return System.Diagnostics.Debugger.IsAttached && symbol.Name != selectClassWhenDebugging;
    }

    private void FindAllInterfaces(SemanticModel model)
    {
        var allInterfaces = model.SyntaxTree.GetRoot().DescendantNodes().OfType<InterfaceDeclarationSyntax>();

        foreach (var declNode in allInterfaces)
        {
            INamedTypeSymbol interfaceSymbol = model.GetDeclaredSymbol(declNode).ThrowIfNull();

            if (IsIgnoredDuringDebugging(interfaceSymbol))
                continue;

            // could also check for [simonly] attribute
            if (interfaceSymbol.AllInterfaces.Any(iface => iface.Name == nameof(IFinObj)))
            {
                var c99Decl = new C99ClsEnumInterface(model, declNode, interfaceSymbol, textWriterFactory);
                c99ClassesEnums.Add(c99Decl);
                fqnToC99Class.Add(c99Decl.GetFqn(), c99Decl);
            }
        }
    }

    internal void ThrowAnyDiagnosticError(IEnumerable<Diagnostic> enumerable)
    {
        var errors = enumerable.Where(d => d.Severity == DiagnosticSeverity.Error);

        var message = "";

        foreach (var error in errors)
        {
            // this might show up as an error if `<WarningsAsErrors>Nullable</WarningsAsErrors>` is set in the .csproj file
            if (error.Id == "CS8632") // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context
                continue;

            message += NL + error.ToString();
        }

        if (message.Length > 0)
        {
            throw new TranspilerException(message);
        }
    }

    public void GatherSolutionDeclarations(string solutionPath, string projectName)
    {
        Solution sln = WorkspaceLoader.LoadSolution(solutionPath);
        var project = sln.Projects.Where(p => p.Name == projectName).Single();
        GatherDeclarationsForProject(project);
    }

    public void Generate()
    {
        foreach (var cls in c99ClassesEnums)
        {
            HeaderGenerator headerGen = new(Options.StyleSettings);

            if (cls.IsEnum)
            {
                headerGen.GenerateEnum(cls);
            }
            else if (cls.IsInterface)
            {
                var iGen = new InterfaceGenerator(cls, Options.StyleSettings);
                headerGen.GenerateCDefines(cls, cls.hFile.mainCodeSb);
                iGen.GenerateInterfaceStructs();
                iGen.GeneratePrototypes();
                iGen.GenerateFunctions();
                iGen.GenerateConversionFunctions();
            }
            else
            {
                // this is a class
                headerGen.GenerateCDefines(cls, cls.hFile.mainCodeSb);
                headerGen.GenerateStructures(cls);
                headerGen.GenerateFunctionPrototypes(cls);

                CFileGenerator cFileGenerator = new(cls, Options.StyleSettings);
                cFileGenerator.Generate();

                // de indent main code and prototypes
                StringUtils.DeIndentInPlace(cls.cFile.mainCodeSb);
                StringUtils.DeIndentInPlace(cls.cFile.prototypesSb);

                var iImplGen = new InterfaceImplementGenerator(cls, Options.StyleSettings);
                iImplGen.GenerateVtables();
                iImplGen.GenerateInterfaceConversions();
            }
        }
    }

    public void SetFilePaths()
    {         
        foreach (var cls in c99ClassesEnums)
        {
            var fileNameBase = cls.GetCName();
            cls.hFile.relativeFilePath = fileNamer(fileNameBase + ".h");
            cls.cFile.relativeFilePath = fileNamer(fileNameBase + ".c");
        }
    }

    public void OptimizeDependencies()
    {
        // don't include in .c file the same includes as in .h file
        foreach (var cls in c99ClassesEnums)
        {
            foreach (var dep in cls.hFile.fqnDependencies)
            {
                cls.cFile.fqnDependencies.Remove(dep);
            }
        }

        // don't include .h file in itself
        foreach (var cls in c99ClassesEnums)
        {
            cls.hFile.fqnDependencies.Remove(cls.GetFqn());
        }

        // we could go further in the future and remove all dependencies that are already included in other files
    }

    public void ResolveDependencies()
    {
        OptimizeDependencies();
        DependencyResolver resolver = new(fqnToC99Class);

        foreach (var cls in c99ClassesEnums)
        {
            if (cls.IsFFIClass || cls.HasFFIMethod)
            {
                cls.hFile.includesSb.Append($"#include \"{cls.GetCName()}{Options.FfiHeaderNamePostfix}\" // You need to provide this{NL}");
            }

            if (cls.HasCFile())
            {
                cls.cFile.includesSb.Append($"#include \"{cls.hFile.relativeFilePath}\"{NL}");
            }

            ResolveFileDependencies(resolver, cls.hFile);
            ResolveFileDependencies(resolver, cls.cFile);
        }
    }

    private void ResolveFileDependencies(DependencyResolver resolver, OutputFile cOrHFile)
    {
        HashSet<string> resolvedDependencies = new();

        // resolve dependencies and add to set to remove duplicates
        foreach (var fqnDependency in cOrHFile.fqnDependencies)
        {
            string? includePath = resolver.ResolveDependency(fqnDependency);
            if (includePath != null)
            {
                resolvedDependencies.Add(includePath);
            }
        }

        // add resolved dependencies to the file
        foreach (var includePath in resolvedDependencies)
        {
            cOrHFile.includesSb.Append($"#include {includePath}{NL}");
        }
    }

    public void SetupFileHeaders(string solutionPath)
    {
        foreach (var cls in c99ClassesEnums)
        {
            string msg = "";
            msg += $"// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.{NL}"; 
            msg += $"// finlang{MaybeGetVersionInfo()} generated this file for C# `{cls.GetFqn()}` type.{NL}";

            string sourceFilePath = cls.GetSourceFilePath();
            string relativeSourcePath = Path.GetRelativePath(Path.GetDirectoryName(solutionPath).ThrowIfNull(), sourceFilePath);

            // Use linux style slashes. See https://github.com/fin-language/fin/issues/46
            relativeSourcePath = relativeSourcePath.Replace("\\", "/");

            msg += $"// Source file: `{relativeSourcePath}` (relative to C# solution).{NL}";

            // calculate MD5 hash of the source file
            if (Options.OutputChecksum)
            {
                var md5 = MD5.HashData(File.ReadAllBytes(sourceFilePath));
                var md5String = BitConverter.ToString(md5).Replace("-", "").ToLower();
                msg += $"// MD5 hash of source file: {md5String}.{NL}";
            }

            if (Options.OutputTimestamp)
                msg += $"// Generated {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.{NL}";

            msg += NL;
            cls.hFile.preIncludesSb.Append(msg);
            cls.cFile.preIncludesSb.Append(msg);
         
            cls.hFile.preIncludesSb.Append($"#pragma once{NL}");
        }
    }

    private string MaybeGetVersionInfo()
    {
        var versionInfo = "";
        if (Options.OutputVersionInfo)
        {
            versionInfo = " v" + GetVersionInfoString();
        }

        return versionInfo;
    }

    public static string GetVersionInfoString()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        AssemblyInformationalVersionAttribute? attr = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();

        string versionInfo;

        if (attr != null)
        {
            versionInfo = attr.InformationalVersion;
        }
        else
        {
            Version? version = assembly.GetName().Version;
            versionInfo = version?.ToString() + "-<unable-to-get-suffix>";
        }

        return versionInfo;
    }

    public void WriteFiles()
    {
        if (Options.DeleteOutputDirBeforeTranspile)
        {
            // delete all files in the destination directory
            if (Directory.Exists(destinationDirPath))
                Directory.Delete(destinationDirPath, recursive: true);
        }

        Directory.CreateDirectory(destinationDirPath);

        WriteFilesWithoutDirectoryWipeCreate();
    }

    public void WriteFilesWithoutDirectoryWipeCreate()
    {
        foreach (var cls in c99ClassesEnums)
        {
            cls.hFile.WriteToFile(destinationDirPath, NL);
            cls.cFile.WriteToFile(destinationDirPath, NL, skipIfMainCodeEmpty: true);
        }
    }

    public List<string> GetListOfAllGeneratedFiles()
    {
        List<string> result = new();
        foreach (var cls in c99ClassesEnums)
        {
            result.Add(cls.hFile.relativeFilePath.ThrowIfNull());
            if (cls.HasCFile())
            {
                result.Add(cls.cFile.relativeFilePath.ThrowIfNull());
            }
        }
        return result;
    }

    public void GenerateAndWrite(string solutionPath, string projectName)
    {
        GatherSolutionDeclarations(solutionPath: solutionPath, projectName: projectName);
        Generate();
        SetupFileHeaders(solutionPath: solutionPath);
        SetFilePaths();
        ResolveDependencies();
        WriteFiles();
    }

    public IMangledNameProvider GetMangledNameProvider()
    {
        return new MangledNameProvider(fqnToC99Class);
    }
}
