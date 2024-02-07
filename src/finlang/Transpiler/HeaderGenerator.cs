using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace finlang.Transpiler;

public class HeaderGenerator
{
    public HeaderGenerator(SemanticModel model, Namer namer)
    {
    }

    public void GenerateStruct(C99ClsEnum cls)
    {
        var symbol = cls.symbol;
        var structName = cls.GetCName();
        var sb = cls.hFile.mainCode;
        CFileGenerator visitor = new(cls);
        visitor.UseHFile();

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

        ClassDeclarationSyntax clsDeclSyntax = cls.syntaxNode;
        visitor.VisitLeadingTrivia(clsDeclSyntax);
        sb.AppendLine($"typedef struct {structName} {structName};");
        sb.AppendLine($"struct {structName}");
        sb.AppendLine("{");

        foreach (var field in cls.syntaxNode.ChildNodes().OfType<FieldDeclarationSyntax>())
        {
            if (!field.IsConst())
                visitor.VisitFieldDeclaration(field);
        }

        foreach (var field in cls.GetInstanceFields())
        {
            cls.AddHeaderFqnDependency(field.Type);
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
