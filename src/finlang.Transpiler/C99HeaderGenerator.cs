using Microsoft.CodeAnalysis;

namespace finlang.Transpiler;

public class C99HeaderGenerator
{
    public C99HeaderGenerator(SemanticModel model, Namer namer)
    {
    }

    public void GenerateStruct(C99ClsEnum cls)
    {
        var symbol = cls.symbol;
        var structName = cls.GetCName();
        var sb = cls.hFile.mainCode;

        // don't generate a struct for FFI classes
        if (cls.IsFFI)
        {
            sb.AppendLine($"// Class is a Foreign Function Interface. No struct generated.");
            return;
        }

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
            var fieldType = Namer.GetCName(field.Type);
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

        var methods = cls.GetMethods();
        foreach (var method in methods)
        {
            var args = (method.IsStatic || cls.IsStaticClass) ? "" : $"{structName} * self";
            cls.AddHeaderFqnDependency(method.ReturnType);
            var returnType = Namer.GetCName(method.ReturnType);
            var methodName = Namer.GetCName(method);

            foreach (var param in method.Parameters)
            {
                cls.AddHeaderFqnDependency(param.Type);
                var paramName = param.Name;
                var paramType = Namer.GetCName(param.Type);
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
