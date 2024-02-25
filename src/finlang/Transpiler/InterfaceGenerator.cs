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

        methods = GetAllInterfaceMethods(cls.symbol).ToList();
    }

    public static IEnumerable<IMethodSymbol> GetAllInterfaceMethods(INamedTypeSymbol interfaceSymbol)
    {
        return GetAllInheritedInterfaceMethods(interfaceSymbol)
            .Concat(interfaceSymbol.GetMembers().OfType<IMethodSymbol>());   // put this interface's methods last
    }

    public static IEnumerable<IMethodSymbol> GetAllInheritedInterfaceMethods(INamedTypeSymbol symbol)
    {
        var superInterfaceTypes = GetSuperInterfaceTypes(symbol);

        foreach (var superInterfaceType in superInterfaceTypes)
        {
            foreach (var m in superInterfaceType.GetMembers().OfType<IMethodSymbol>())
            {
                yield return m;
            }
        }
    }

    public static IEnumerable<INamedTypeSymbol> GetSuperInterfaceTypes(INamedTypeSymbol symbol)
    {
        return symbol.AllInterfaces.Where(i => i.Name != nameof(IFinObj));
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
            visitor.VisitParameterListCustom(mDecl.ParameterList, symbol: methodSymbol, selfTypeName: "void");
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
    }

    public void GenerateFunctions()
    {
        visitor.UseCFile();
        visitor.renderingPrototypes = false;
        var sb = cls.cFile.mainCode;

        foreach (var methodSymbol in methods)
        {
            var returnCode = methodSymbol.ReturnsVoid ? "" : "return ";
            GenerateMethodSignatureDeIndented(sb, methodSymbol);
            sb.AppendLine("\n{");
            sb.Append($"    {returnCode}self->vtable->{methodSymbol.Name}(self");
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
        visitor.VisitParameterListCustom(mDecl.ParameterList, symbol: methodSymbol, selfTypeName: Namer.GetCName(cls.symbol));
    }

    private void GenerateMethodSignatureDeIndented(StringBuilder sb, IMethodSymbol methodSymbol)
    {
        var tempSb = new StringBuilder();
        GenerateMethodSignature(tempSb, methodSymbol);
        sb.Append(StringUtils.DeIndent(tempSb.ToString()));
        visitor.SetSb(sb);
    }

    public static string GetInterfaceVtableStructName(string structName)
    {
        return structName + "_vtable";
    }

    public void GenerateConversionFunctions()
    {
        visitor.UseCFile();
        visitor.renderingPrototypes = false;
        var sb = cls.cFile.mainCode;
        cls.cFile.includes.Add($"<stddef.h>");
        cls.cFile.includes.Add($"<assert.h>");

        cls.hFile.mainCode.AppendLine();

        var superInterfaceTypes = GetSuperInterfaceTypes(cls.symbol);

        var myTypeName = cls.GetCName();
        var myVtableTypeName = GetInterfaceVtableStructName(myTypeName);

        foreach (var superInterface in superInterfaceTypes)
        {
            var tab = "    ";
            var resultVarName = "result";
            var superTypeName = Namer.GetCName(superInterface);
            var superVtableTypeName = GetInterfaceVtableStructName(superTypeName);

            string comment = $"// Up conversion from {myTypeName} interface to {superTypeName} interface";
            string conversionMethodSignature = $"{superTypeName} {myTypeName}__to__{superTypeName}({myTypeName} * self)";
            sb.AppendLine(comment);
            sb.AppendLine(conversionMethodSignature);
            sb.AppendLine("{");
            sb.AppendLine($"{tab}{superTypeName} {resultVarName};");
            sb.AppendLine();

            // h file stuff
            {
                cls.hFile.AddFqnDependency(superInterface);
                cls.hFile.mainCode.AppendLine(comment);
                cls.hFile.mainCode.AppendLine($"{conversionMethodSignature};\n");
            }

            var superMethods = GetAllInterfaceMethods(superInterface);
            string firstMethodName = superMethods.First().Name;

            sb.AppendLine($"{tab}// assert that vtable layouts are compatible");
            // static_assert(offsetof(hal_IDigIn_vtable, read_state) == 0, "Unexpected function pointer offset");
            sb.AppendLine($"{tab}static_assert(offsetof({superVtableTypeName}, {firstMethodName}) == 0, \"Unexpected vtable function start\");");

            foreach (var methodSymbol in superMethods)
            {
                // static_assert(offsetof(hal_IDigIn_vtable, read_state) == offsetof(hal_IDigInOut_vtable, read_state) - offsetof(hal_IDigInOut_vtable, read_state), "Incompatible vtable layout");
                sb.AppendLine($"{tab}static_assert(offsetof({superVtableTypeName}, {methodSymbol.Name}) == offsetof({myVtableTypeName}, {methodSymbol.Name}) - offsetof({myVtableTypeName}, {firstMethodName}), \"Incompatible vtable layout\");");
            }

            sb.AppendLine($"\n{tab}// adjust vtable pointer");

            //out.vtable = (hal_IDigIn_vtable*)self->vtable + offsetof(hal_IDigInOut_vtable, read_state);
            sb.AppendLine($"{tab}{resultVarName}.vtable = ({superVtableTypeName}*)self->vtable + offsetof({myVtableTypeName}, {firstMethodName});");

            sb.AppendLine($"{tab}{resultVarName}.self = self->self;");
            sb.AppendLine($"{tab}return {resultVarName};");
            sb.AppendLine("}\n");
        }
    }
}
