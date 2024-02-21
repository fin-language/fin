using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;

namespace finlang.Transpiler;

public class InterfaceGenerator
{
    C99ClsEnumInterface cls;

    public InterfaceGenerator(C99ClsEnumInterface cls)
    {
        this.cls = cls;
    }

    public void GenerateInterfaceStructs()
    {
        var interfaceName = cls.GetCName();
        CFileGenerator visitor = new(cls);
        visitor.UseHFile();
        visitor.renderingPrototypes = true;
        var sb = cls.hFile.mainCode;

        if (!cls.IsInterface)
        {
            throw new InvalidOperationException("Object has to be an interface for this code.");
        }

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

        // generate vtable struct

        sb.AppendLine($"struct {interfaceName}_vtable");
        sb.AppendLine("{");

        // get all methods from interface including inherited ones
        var thisInterfaceSymbol = cls.symbol;
        var superInterfaceTypes = cls.symbol.AllInterfaces.Where(i => i.Name != nameof(IFinObj));

        foreach (var superInterfaceType in superInterfaceTypes)
        {
            RenderVTableEntry(visitor, sb, thisInterfaceSymbol, superInterfaceType);
        }

        RenderVTableEntry(visitor, sb, thisInterfaceSymbol, cls.symbol);
        sb.AppendLine("};");
    }

    private void RenderVTableEntry(CFileGenerator visitor, StringBuilder sb, INamedTypeSymbol implementingInterface, INamedTypeSymbol interfaceWithMethods)
    {
        interfaceWithMethods.GetMembers().OfType<IMethodSymbol>().ToList().ForEach(methodSymbol =>
        {
            sb.Append($"    {methodSymbol.ReturnType} (*{methodSymbol.Name})");
            var mDecl = (MethodDeclarationSyntax)methodSymbol.DeclaringSyntaxReferences.Single().GetSyntax();
            visitor.VisitParameterListCustom(mDecl.ParameterList, symbol: methodSymbol, selfType: implementingInterface);
            sb.AppendLine(";");

            // track dependencies
            HeaderGenerator.TrackMethodDependencies(cls, methodSymbol);
        });
    }
}
