using finlang.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;

namespace finlang.Transpiler;

public class InterfaceGenerator
{
    private readonly CFileGenerator visitor;
    private readonly C99ClsEnumInterface cls;
    StyleSettings styleSettings;
    protected string NL => styleSettings.newLine;
    protected string Indent => styleSettings.indent;

    private readonly List<IMethodSymbol> methods = new();

    public InterfaceGenerator(C99ClsEnumInterface cls, StyleSettings styleSettings)
    {
        if (!cls.IsInterface)
            throw new InvalidOperationException("Object has to be an interface for this code.");

        visitor = new CFileGenerator(cls, styleSettings);
        this.cls = cls;

        methods = GetAllInterfaceMethods(cls.symbol).ToList();
        this.styleSettings = styleSettings;
    }

    public static IEnumerable<IMethodSymbol> GetAllInterfaceMethods(INamedTypeSymbol interfaceSymbol)
    {
        return GetAllInheritedInterfaceMethods(interfaceSymbol)
            .Concat(GetInterfaceMethods(interfaceSymbol));   // put this interface's methods last
    }

    public static IEnumerable<IMethodSymbol> GetAllInheritedInterfaceMethods(INamedTypeSymbol symbol)
    {
        var superInterfaceTypes = GetSuperInterfaceTypes(symbol);

        foreach (var superInterfaceType in superInterfaceTypes)
        {
            foreach (var m in GetInterfaceMethods(superInterfaceType))
            {
                yield return m;
            }
        }
    }

    private static IEnumerable<IMethodSymbol> GetInterfaceMethods(INamedTypeSymbol superInterfaceType)
    {
        return superInterfaceType.GetMembers().OfType<IMethodSymbol>()
            .Where(m => m.MethodKind != MethodKind.SharedConstructor); // exclude static constructors
    }

    public static IEnumerable<INamedTypeSymbol> GetSuperInterfaceTypes(INamedTypeSymbol symbol)
    {
        return symbol.AllInterfaces.Where(i => i.Name != nameof(IFinObj));
    }

    public void GenerateInterfaceStructs()
    {
        visitor.UseHFile();
        visitor.renderingPrototypes = true;
        var sb = cls.hFile.mainCodeSb;

        var interfaceName = GetInterfaceStructName();

        // generate forward declarations
        sb.Append($"typedef struct {interfaceName} {interfaceName};{NL}");
        sb.Append($"typedef struct {interfaceName}_vtable {interfaceName}_vtable;{NL}");
        sb.Append($"{NL}");

        sb.Append($"struct {interfaceName}{NL}");
        sb.Append($"{{{NL}");
        sb.Append($"{Indent}/** Pointer to implementing object's vtable for this interface */{NL}");
        sb.Append($"{Indent}{interfaceName}_vtable const * const obj_vtable;{NL}");
        sb.Append($"{NL}");
        sb.Append($"{Indent}/** The actual object that implements this interface */{NL}");
        sb.Append($"{Indent}void * const obj;{NL}");
        sb.Append($"}};{NL}");
        sb.Append(NL);

        sb.Append($"struct {interfaceName}_vtable{NL}");
        sb.Append($"{{{NL}");


        foreach (var methodSymbol in methods)
        {
            var mDecl = (MethodDeclarationSyntax)methodSymbol.DeclaringSyntaxReferences.Single().GetSyntax();
            visitor.Visit(mDecl.ReturnType);

            sb.Append($"(*{methodSymbol.Name})");
            visitor.VisitParameterListCustom(mDecl.ParameterList, symbol: methodSymbol, selfTypeName: "void");
            sb.Append($";{NL}");

            HeaderGenerator.TrackMethodDependencies(cls, methodSymbol);
        }

        sb.Append($"}};{NL}{NL}");
    }

    private string GetInterfaceStructName()
    {
        return cls.GetCName();
    }

    public void GeneratePrototypes()
    {
        visitor.UseHFile();
        var sb = cls.hFile.mainCodeSb;
        visitor.renderingPrototypes = true;

        foreach (var methodSymbol in methods)
        {
            GenerateMethodSignatureDeIndented(sb, methodSymbol);
            sb.Append($";{NL}{NL}");
        }
    }

    public void GenerateFunctions()
    {
        visitor.UseCFile();
        visitor.renderingPrototypes = false;
        var sb = cls.cFile.mainCodeSb;

        foreach (var methodSymbol in methods)
        {
            var returnCode = methodSymbol.ReturnsVoid ? "" : "return ";
            GenerateMethodSignatureDeIndented(sb, methodSymbol);
            sb.Append($"{NL}{{{NL}");
            sb.Append($"{Indent}{returnCode}self->obj_vtable->{methodSymbol.Name}(self->obj");
            foreach (var parameter in methodSymbol.Parameters)
            {
                sb.Append($", {parameter.Name}");
            }
            sb.Append($");{NL}");

            sb.Append($"}}{NL}{NL}");
        }
    }

    // generates stuff like `void hal_IDigInOut_set_state(hal_IDigInOut * self, bool state)` and any leading comments
    private void GenerateMethodSignature(StringBuilder sb, IMethodSymbol methodSymbol)
    {
        var mDecl = (MethodDeclarationSyntax)methodSymbol.DeclaringSyntaxReferences.Single().GetSyntax();
        visitor.SetSb(sb);
        visitor.Visit(mDecl.ReturnType); // includes comment and indent

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

    /// <summary>
    /// MCL means Macro Compound Literal
    /// https://github.com/fin-language/fin/issues/60
    /// </summary>
    public static string GetMclConversionFunctionName(string fromInterfaceName, string toInterfaceName)
    {
        return $"MCL_{fromInterfaceName}__to__{toInterfaceName}";
    }

    /// <summary>
    /// MAA means Macro Aggregate Assignment
    /// https://github.com/fin-language/fin/issues/60
    /// </summary>
    public static string GetMaaConversionFunctionName(string fromInterfaceName, string toInterfaceName)
    {
        return $"MAA_{fromInterfaceName}__to__{toInterfaceName}";
    }

    public void GenerateConversionFunctions()
    {
        visitor.UseHFile();
        visitor.renderingPrototypes = false;
        OutputFile codeFile = cls.hFile;
        var sb = codeFile.mainCodeSb;
        codeFile.includesSet.Add($"<stddef.h>");
        codeFile.includesSet.Add($"<assert.h>");

        var superInterfaceTypes = GetSuperInterfaceTypes(cls.symbol);

        var myTypeName = cls.GetCName();
        var myVtableTypeName = GetInterfaceVtableStructName(myTypeName);

        foreach (var superInterface in superInterfaceTypes)
        {
            codeFile.AddFqnDependency(superInterface);
            var superMethods = GetAllInterfaceMethods(superInterface);
            string firstMethodName = superMethods.First().Name;

            var superTypeName = Namer.GetCName(superInterface);
            var superVtableTypeName = GetInterfaceVtableStructName(superTypeName);

            sb.Append($"{NL}// Up conversion from {myTypeName} interface to {superTypeName} interface{NL}");
            sb.Append($"// `self_arg` should be of type `{myTypeName} *`{NL}");

            string conversionBody = $"{{ .obj = self_arg->obj, .obj_vtable = (const {superVtableTypeName}*)(&self_arg->obj_vtable->{firstMethodName}) }}";

            sb.Append($"// MAA stands for Macro Aggregate Assignment. See https://github.com/fin-language/fin/issues/60 {NL}");
            sb.Append($"#define {GetMaaConversionFunctionName(myTypeName, superTypeName)}(self_arg)    {conversionBody}{NL}");
            sb.Append($"// MCL stands for Macro Compound Literal. See https://github.com/fin-language/fin/issues/60 {NL}");
            sb.Append($"#define {GetMclConversionFunctionName(myTypeName, superTypeName)}(self_arg)    ({superTypeName}){conversionBody}{NL}");

            sb.Append($"// assert that vtable layouts are compatible{NL}");
            sb.Append($"static_assert(offsetof({superVtableTypeName}, {firstMethodName}) == 0, \"Unexpected vtable function start\");{NL}");

            foreach (var methodSymbol in superMethods)
            {
                // static_assert(offsetof(hal_IDigIn_vtable, read_state) == offsetof(hal_IDigInOut_vtable, read_state) - offsetof(hal_IDigInOut_vtable, read_state), "Incompatible vtable layout");
                sb.Append($"static_assert(offsetof({superVtableTypeName}, {methodSymbol.Name}) == offsetof({myVtableTypeName}, {methodSymbol.Name}) - offsetof({myVtableTypeName}, {firstMethodName}), \"Incompatible vtable layout\");{NL}");
            }
        }
    }
}
