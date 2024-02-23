using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;

namespace finlang.Transpiler;

public class InterfaceGenerator
{
    private readonly CFileGenerator visitor;
    private readonly C99ClsEnumInterface cls;
    private readonly List<IMethodSymbol> methods = new();

    public InterfaceGenerator(C99ClsEnumInterface cls)
    {
        if (!cls.IsInterface)
            throw new InvalidOperationException("Object has to be an interface for this code.");

        visitor = new CFileGenerator(cls);
        this.cls = cls;

        var superInterfaceTypes = cls.symbol.AllInterfaces.Where(i => i.Name != nameof(IFinObj));

        foreach (var superInterfaceType in superInterfaceTypes)
        {
            methods.AddRange(superInterfaceType.GetMembers().OfType<IMethodSymbol>());
        }

        // put specific interface methods last
        methods.AddRange(cls.symbol.GetMembers().OfType<IMethodSymbol>());
    }

    public void GenerateInterfaceStructs()
    {
        visitor.UseHFile();
        visitor.renderingPrototypes = true;
        var sb = cls.hFile.mainCode;

        var interfaceName = GetInterfaceStructName();

        // generate forward declarations
        sb.AppendLine($"typedef struct {interfaceName} {interfaceName};");
        sb.AppendLine($"typedef struct {interfaceName}_vtable {interfaceName}_vtable;");
        sb.AppendLine();

        sb.AppendLine($"struct {interfaceName}");
        sb.AppendLine("{");
        sb.AppendLine($"    {interfaceName}_vtable const * /*const*/ vtable;");
        sb.AppendLine("    void * /*const*/ self;");
        sb.AppendLine("};");
        sb.AppendLine();

        sb.AppendLine($"struct {interfaceName}_vtable");
        sb.AppendLine("{");


        foreach (var methodSymbol in methods)
        {
            var mDecl = (MethodDeclarationSyntax)methodSymbol.DeclaringSyntaxReferences.Single().GetSyntax();
            visitor.VisitToken(mDecl.ReturnType.GetFirstToken()); // includes comment and indent

            sb.Append($"(*{methodSymbol.Name})");
            visitor.VisitParameterListCustom(mDecl.ParameterList, symbol: methodSymbol, selfType: cls.symbol);
            sb.AppendLine(";");

            HeaderGenerator.TrackMethodDependencies(cls, methodSymbol);
        }

        sb.AppendLine("};\n");
    }

    private string GetInterfaceStructName()
    {
        return cls.GetCName();
    }

    public void GeneratePrototypes()
    {
        visitor.UseHFile();
        var sb = cls.hFile.mainCode;
        visitor.renderingPrototypes = true;

        foreach (var methodSymbol in methods)
        {
            GenerateMethodSignatureDeIndented(sb, methodSymbol);
            sb.AppendLine(";\n");
        }

        var mainCode = cls.hFile.mainCode;
    }

    public void GenerateFunctions()
    {
        visitor.UseCFile();
        visitor.renderingPrototypes = false;
        var sb = cls.cFile.mainCode;

        foreach (var methodSymbol in methods)
        {
            GenerateMethodSignatureDeIndented(sb, methodSymbol);
            sb.AppendLine("\n{");
            sb.Append($"    return self->vtable->{methodSymbol.Name}(self");
            foreach (var parameter in methodSymbol.Parameters)
            {
                sb.Append($", {parameter.Name}");
            }
            sb.Append($");\n");

            sb.AppendLine("}\n");
        }
    }

    // generates stuff like `void hal_IDigInOut_set_state(hal_IDigInOut * self, bool state)` and any leading comments
    private void GenerateMethodSignature(StringBuilder sb, IMethodSymbol methodSymbol)
    {
        var mDecl = (MethodDeclarationSyntax)methodSymbol.DeclaringSyntaxReferences.Single().GetSyntax();
        visitor.SetSb(sb);
        visitor.VisitToken(mDecl.ReturnType.GetFirstToken()); // includes comment and indent

        var prefix = cls.GetCName() + "_";
        sb.Append($"{prefix}{methodSymbol.Name}");
        visitor.VisitParameterListCustom(mDecl.ParameterList, symbol: methodSymbol, selfType: cls.symbol);
    }

    private void GenerateMethodSignatureDeIndented(StringBuilder sb, IMethodSymbol methodSymbol)
    {
        var tempSb = new StringBuilder();
        GenerateMethodSignature(tempSb, methodSymbol);
        sb.Append(StringUtils.DeIndent(tempSb.ToString()));
        visitor.SetSb(sb);
    }
}
