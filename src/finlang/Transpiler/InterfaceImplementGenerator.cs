using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;

namespace finlang.Transpiler;

public class InterfaceImplementGenerator
{
    private readonly CFileGenerator visitor;
    private readonly C99ClsEnumInterface cls;

    public InterfaceImplementGenerator(C99ClsEnumInterface cls)
    {
        if (!cls.IsClass)
            throw new InvalidOperationException("Object has to be an class for this code.");

        visitor = new CFileGenerator(cls);
        this.cls = cls;
    }

    public void GenerateVtables()
    {
        visitor.UseCFile();
        var sb = cls.cFile.mainCode;

        var directInterfaces = GetDirectInterfaces();
        foreach (var directInterface in directInterfaces)
        {
            cls.SetHasVTable();
            cls.cFile.AddFqnDependency(directInterface);

            sb.AppendLine($"\n// virtual table implementation for {directInterface.Name}. Note that this is extern'd.");
            var vtableStructName = InterfaceGenerator.GetInterfaceVtableStructName(Namer.GetCName(directInterface));
            var vtableInstanceName = GetVtableInstanceName(vtableStructName);

            var decl = $"const {vtableStructName} {vtableInstanceName}";

            cls.hFile.mainCode.AppendLine("\n// vtable is extern to allow const initializations");
            cls.hFile.mainCode.AppendLine($"extern {decl};");
            sb.AppendLine($"{decl} = {{");
            foreach (var interfaceMethod in InterfaceGenerator.GetAllInterfaceMethods(directInterface))
            {
                IMethodSymbol impMethod = cls.GetMethods().Single(m => m.Name == interfaceMethod.Name);
                var mDecl = (MethodDeclarationSyntax)interfaceMethod.DeclaringSyntaxReferences.Single().GetSyntax();

                sb.Append($"    .{interfaceMethod.Name} = ");

                // need to cast to use 'void*' self parameter. Ex: (bool (*)(void *))
                sb.Append($"({Namer.GetCName(interfaceMethod.ReturnType)} (*)");
                visitor.VisitParameterListCustom(mDecl.ParameterList, symbol: interfaceMethod, selfTypeName: "void");
                sb.Append(')');

                sb.AppendLine($"{Namer.GetCName(impMethod)},");
            }
            sb.AppendLine("};\n");
        }
    }

    private static string GetVtableInstanceName(string vtableStructName)
    {
        return vtableStructName + "_imp";
    }

    private IEnumerable<INamedTypeSymbol> GetDirectInterfaces()
    {
        return cls.symbol.Interfaces.Where(i => i.Name != nameof(IFinObj));
    }

    internal void GenerateInterfaceConversions()
    {
        var sb = cls.hFile.mainCode;
        string myTypeName = cls.GetCName();

        var directInterfaces = GetDirectInterfaces();

        if (!directInterfaces.Any())
            return;

        //cls.cFile.includes.Add($"<stddef.h>");
        //cls.cFile.includes.Add($"<assert.h>");

        foreach (var directInterface in directInterfaces)
        {
            cls.hFile.AddFqnDependency(directInterface);

            var vtableStructName = InterfaceGenerator.GetInterfaceVtableStructName(Namer.GetCName(directInterface));

            GenConversionMethod(sb, myTypeName, directInterface, vtableStructName);
            foreach (var superInterface in InterfaceGenerator.GetSuperInterfaceTypes(directInterface))
            {
                GenConversionMethod(sb, myTypeName, superInterface, vtableStructName);
            }
        }
    }

    private void GenConversionMethod(StringBuilder sb, string myTypeName, INamedTypeSymbol directInterface, string vtableStructName)
    {
        var superTypeName = Namer.GetCName(directInterface);
        var superVtableTypeName = InterfaceGenerator.GetInterfaceVtableStructName(superTypeName);
        var vtableInstanceName = GetVtableInstanceName(vtableStructName);

        var superMethods = InterfaceGenerator.GetAllInterfaceMethods(directInterface);
        string firstMethodName = superMethods.First().Name;
        string conversionFunctionName = InterfaceGenerator.GetConversionFunctionName(myTypeName, superTypeName);
        sb.AppendLine($"\n// Up conversion from {myTypeName} to {superTypeName} interface");
        sb.AppendLine($"#define {conversionFunctionName}(self_arg)    ({superTypeName}){{ .self = self_arg, .vtable = (const {superVtableTypeName}*)(&{vtableInstanceName}.{firstMethodName}) }}");
    }
}
