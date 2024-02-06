﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;
using System.Text;

namespace finlang.Transpiler;

public class Transpiler
{
    private string destinationDirPath;
    private string solutionPath;
    private string projectName;

    public List<C99ClsEnum> c99ClassEnum = new();
    public Dictionary<string, C99ClsEnum> fqnToC99Class = new();

    public Transpiler(string destinationDirPath, string solutionPath, string projectName)
    {
        this.destinationDirPath = destinationDirPath;
        this.solutionPath = solutionPath;
        this.projectName = projectName;
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
        project = project.AddMetadataReferences(GetAssemblies());
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
                    var c99Decl = new C99ClsEnum(model, classDeclNode, symbol);
                    c99ClassEnum.Add(c99Decl);
                    fqnToC99Class.Add(c99Decl.GetFqn(), c99Decl);
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
        Solution sln = WorkspaceLoader.LoadSolution(solutionPath);
        var project = sln.Projects.Where(p => p.Name == projectName).Single();
        GatherDeclarationsForProject(project);
    }

    public void Generate()
    {
        foreach (var cls in c99ClassEnum)
        {
            Namer namer = new(cls.model);
            HeaderGenerator gen = new(cls.model, namer);

            gen.GenerateStruct(cls);
            gen.GenerateFunctionPrototypes(cls);

            CFileGenerator cFileGenerator = new(cls);
            cFileGenerator.Generate();
        }
    }

    public void SetFilePaths()
    {         
        foreach (var cls in c99ClassEnum)
        {
            var fileNameBase = cls.GetCName();
            cls.hFile.relativeFilePath = fileNameBase + ".h";
            cls.cFile.relativeFilePath = fileNameBase + ".c";
        }
    }

    public void OptimizeDependencies()
    {
        // don't include in .c file the same includes as in .h file
        foreach (var cls in c99ClassEnum)
        {
            foreach (var dep in cls.hFile.fqnDependencies)
            {
                cls.cFile.fqnDependencies.Remove(dep);
            }
        }

        // we could go further in the future and remove all dependencies that are already included in other files
    }

    public void ResolveDependencies()
    {
        OptimizeDependencies();
        DependencyResolver resolver = new(fqnToC99Class);

        foreach (var cls in c99ClassEnum)
        {
            if (cls.IsFFI)
            {
                cls.hFile.includesSb.AppendLine($"#include \"{cls.GetCName()}_port_implementation.h\" // You need to provide this");
            }

            cls.cFile.includesSb.AppendLine("#include \"" + cls.hFile.relativeFilePath + "\"");

            ResolveFileDependencies(resolver, cls.hFile);
            ResolveFileDependencies(resolver, cls.cFile);
        }
    }

    private static void ResolveFileDependencies(DependencyResolver resolver, OutputFile cOrHFile)
    {
        foreach (var fqnDependency in cOrHFile.fqnDependencies)
        {
            string? includePath = resolver.ResolveDependency(fqnDependency);
            if (includePath != null)
            {
                cOrHFile.includesSb.AppendLine("#include " + includePath + "");
            }
        }
    }

    public void SetupFileHeaders()
    {
        foreach (var cls in c99ClassEnum)
        {
            string msg = $"// finlang generated file for c# {cls.GetFqn()} class";
            cls.hFile.preIncludes.AppendLine(msg);
            cls.hFile.preIncludes.AppendLine("#pragma once");
            cls.cFile.preIncludes.AppendLine(msg);
        }
    }

    public void WriteFiles()
    {
        // delete all files in the destination directory
        if (Directory.Exists(destinationDirPath))
            Directory.Delete(destinationDirPath, recursive: true);
        Directory.CreateDirectory(destinationDirPath);

        foreach (var cls in c99ClassEnum)
        {
            cls.hFile.WriteToFile(destinationDirPath);

            if (cls.IsFFI)
            {
                //don't write the c file for FFI classes
            }
            else
            {
                cls.cFile.WriteToFile(destinationDirPath);
            }
        }
    }

    public List<string> GetListOfAllGeneratedFiles()
    {
        List<string> result = new();
        foreach (var cls in c99ClassEnum)
        {
            result.Add(cls.hFile.relativeFilePath.ThrowIfNull());
            if (!cls.IsFFI)
            {
                result.Add(cls.cFile.relativeFilePath.ThrowIfNull());
            }
        }
        return result;
    }

    public void GenerateAndWrite()
    {
        GatherSolutionDeclarations();
        Generate();
        SetupFileHeaders();
        SetFilePaths();
        ResolveDependencies();
        WriteFiles();
    }
}