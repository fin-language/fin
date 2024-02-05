using Microsoft.CodeAnalysis;

namespace finlang.Transpiler;

public class C99StructGenerator
{
    public C99StructGenerator(SemanticModel model, C99Namer namer)
    {
    }

    public void GenerateStruct(C99ClsEnum cls)
    {
        var symbol = cls.symbol;
        var structName = cls.GetCName();

        // don't generate a struct for FFI classes
        if (cls.IsFFI)
        {
            return;
        }

        var sb = cls.hFile.mainCode;

        if (cls.IsStaticClass)
        {
            sb.AppendLine($"// Class has no fields. No struct generated.");
            return;
        }

        sb.AppendLine($"typedef struct {structName} {structName};  // forward declaration");
        sb.AppendLine($"struct {structName}");
        sb.AppendLine("{");

        foreach (var field in cls.GetInstanceFields())
        {
            cls.AddHeaderFqnDependency(field.Type);
            var fieldName = field.Name;
            var fieldType = C99Namer.GetCName(field.Type);
            var starOrSpace = field.Type.IsReferenceType ? " * " : " ";
            sb.AppendLine($"    {fieldType}{starOrSpace}{fieldName};");
        }

        sb.AppendLine("};");
        sb.AppendLine();
    }

    public void GenerateFunctionPrototypes(C99ClsEnum cls)
    {
        var symbol = cls.symbol;
        var structName = cls.GetCName();

        var sb = cls.hFile.mainCode;

        var methods = symbol.GetMembers().OfType<IMethodSymbol>();
        foreach (var method in methods)
        {
            var args = (method.IsStatic || cls.IsStaticClass) ? "" : $"{structName} * self";
            cls.AddHeaderFqnDependency(method.ReturnType);
            var returnType = C99Namer.GetCName(method.ReturnType);
            var methodName = C99Namer.GetCName(method);

            foreach (var param in method.Parameters)
            {
                cls.AddHeaderFqnDependency(param.Type);
                var paramName = param.Name;
                var paramType = C99Namer.GetCName(param.Type);
                var starOrSpace = param.Type.IsReferenceType ? " * " : " ";
                if (args.Length > 0)
                {
                    args += ", ";
                }
                args += $"{paramType}{starOrSpace}{paramName}";
            }

            sb.AppendLine($"{returnType} {methodName}({args});");
        }
    }

}
