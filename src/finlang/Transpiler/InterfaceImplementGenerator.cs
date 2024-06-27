using finlang.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Text;

namespace finlang.Transpiler;

public class InterfaceImplementGenerator
{
    private readonly CFileGenerator visitor;
    private readonly C99ClsEnumInterface cls;
    StyleSettings styleSettings;
    protected string NL => styleSettings.newLine;
    protected string Indent => styleSettings.indent;

    public InterfaceImplementGenerator(C99ClsEnumInterface cls, StyleSettings styleSettings)
    {
        if (!cls.IsClass)
            throw new InvalidOperationException("Object has to be an class for this code.");

        visitor = new CFileGenerator(cls, styleSettings);
        this.cls = cls;
        this.styleSettings = styleSettings;
    }

    public void GenerateVtables()
    {
        visitor.UseCFile();
        var sb = cls.cFile.mainCodeSb;

        var directInterfaces = GetDirectInterfaces();
        foreach (var directInterface in directInterfaces)
        {
            cls.SetHasVTable();
            cls.cFile.AddFqnDependency(directInterface);

            sb.Append($"{NL}// virtual table implementation for {directInterface.Name}. Note that this is extern'd.{NL}");
            var vtableStructName = InterfaceGenerator.GetInterfaceVtableStructName(Namer.GetCName(directInterface));
            var vtableInstanceName = GetVtableInstanceName(vtableStructName);

            var decl = $"const {vtableStructName} {vtableInstanceName}";

            cls.hFile.mainCodeSb.Append($"{NL}// vtable is extern to allow const initializations{NL}");
            cls.hFile.mainCodeSb.Append($"extern {decl};{NL}");
            sb.Append($"{decl} = {{{NL}");
            foreach (var interfaceMethod in InterfaceGenerator.GetAllInterfaceMethods(directInterface))
            {
                IMethodSymbol impMethod = cls.GetMethods().Single(m => m.Name == interfaceMethod.Name);
                var mDecl = (MethodDeclarationSyntax)impMethod.DeclaringSyntaxReferences.Single().GetSyntax();

                sb.Append($"{Indent}.{impMethod.Name} = ");

                // need to cast to use 'void*' self parameter. Ex: (bool (*)(void *))
                sb.Append($"({visitor.GetCTypeDeclaration(impMethod.ReturnType)} (*)");
                visitor.VisitParameterListCustom(mDecl.ParameterList, symbol: impMethod, selfTypeName: "void");
                StringUtils.EraseTrailingWhitespace(sb);

                sb.Append(')');

                sb.Append($"{Namer.GetCName(impMethod)},{NL}");
            }
            sb.Append($"}};{NL}{NL}");
        }
    }

    private string GetVtableInstanceName(string vtableStructName)
    {
        // https://github.com/fin-language/fin/issues/57
        return $"{cls.GetCName()}__{vtableStructName}_imp";
    }

    private IEnumerable<INamedTypeSymbol> GetDirectInterfaces()
    {
        // Get all interfaces that are not IFinObj
        var result = cls.symbol.Interfaces.Where(i => i.Name != nameof(IFinObj));

        // Filter out interfaces that are not part of IFinObj
        // https://github.com/fin-language/fin/issues/63
        result = result.Where(i => i.AllInterfaces.Any(ii => ii.Name == nameof(IFinObj)));
        return result;
    }

    internal void GenerateInterfaceConversions()
    {
        var sb = cls.hFile.mainCodeSb;
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
        sb.Append($"{NL}// Up conversion from {myTypeName} to {superTypeName} interface{NL}");
        string conversionBody = $"{{ .obj = self_arg, .obj_vtable = (const {superVtableTypeName}*)(&{vtableInstanceName}.{firstMethodName}) }}";

        sb.Append($"// MAA stands for Macro Aggregate Assignment. See https://github.com/fin-language/fin/issues/60 {NL}");
        sb.Append($"#define {InterfaceGenerator.GetMaaConversionFunctionName(myTypeName, superTypeName)}(self_arg)    {conversionBody}{NL}");
        sb.Append($"// MCL stands for Macro Compound Literal. See https://github.com/fin-language/fin/issues/60 {NL}");
        sb.Append($"#define {InterfaceGenerator.GetMclConversionFunctionName(myTypeName, superTypeName)}(self_arg)    ({superTypeName}){conversionBody}{NL}");
    }
}
