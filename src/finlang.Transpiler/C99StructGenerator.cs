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

    public void GenerateStruct(C99Class c99Class)
    {
        var structName = namer.GetCName(c99Class.syntaxNode);

        var sb = c99Class._hFile.sb;
        sb.AppendLine($"typedef struct {structName} {structName};  // forward declaration");

        // iterate over all fields
        //foreach (var field in c99Class.symbol.GetMembers().Where(m => m.Kind == SymbolKind.Field))
        //{
        //    sb.AppendLine($"typedef {SymbolHelper.GetC99Type(field)} {field.Name};");
        //}
    }
}
