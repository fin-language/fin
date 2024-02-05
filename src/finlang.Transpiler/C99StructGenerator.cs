using Microsoft.CodeAnalysis;

namespace finlang.Transpiler;

public class C99StructGenerator
{
    SemanticModel model;
    C99Namer namer;

    public C99StructGenerator(SemanticModel model, C99Namer namer)
    {
        this.model = model;
        this.namer = namer;
    }

    public void GenerateStruct(C99ClsEnum c99Class)
    {
        var symbol = c99Class.symbol;
        var structName = c99Class.GetCName();

        // don't generate a struct for FFI classes
        if (c99Class.IsFFI)
        {
            return;
        }

        var structFields = symbol.GetMembers().OfType<IFieldSymbol>().Where(f => !f.IsConst && !f.IsStatic);
        if (structFields.Count() == 0)
            return;

        var sb = c99Class._hFile.mainCode;
        sb.AppendLine($"typedef struct {structName} {structName};  // forward declaration");
        sb.AppendLine($"struct {structName}");
        sb.AppendLine("{");

        foreach (var field in structFields)
        {
            var fieldName = field.Name;
            var fieldType = C99Namer.GetCName(field.Type);
            var starOrSpace = field.Type.IsReferenceType ? " * " : " ";
            sb.AppendLine($"    {fieldType}{starOrSpace}{fieldName};");
        }

        sb.AppendLine("};");
        sb.AppendLine();
    }


}
