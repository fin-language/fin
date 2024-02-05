using Microsoft.CodeAnalysis;

namespace finlang.Transpiler;

public class C99StructGenerator
{
    public C99StructGenerator(SemanticModel model, C99Namer namer)
    {
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

        var sb = c99Class.hFile.mainCode;

        var structFields = symbol.GetMembers().OfType<IFieldSymbol>().Where(f => !f.IsConst && !f.IsStatic);
        if (!structFields.Any())
        {
            sb.AppendLine($"// Class has no fields. No struct generated.");
            return;
        }

        sb.AppendLine($"typedef struct {structName} {structName};  // forward declaration");
        sb.AppendLine($"struct {structName}");
        sb.AppendLine("{");

        foreach (var field in structFields)
        {
            c99Class.AddFqnDependency(field.Type);
            var fieldName = field.Name;
            var fieldType = C99Namer.GetCName(field.Type);
            var starOrSpace = field.Type.IsReferenceType ? " * " : " ";
            sb.AppendLine($"    {fieldType}{starOrSpace}{fieldName};");
        }

        sb.AppendLine("};");
        sb.AppendLine();
    }


}
