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
        CFileGenerator visitor = new(cls);
        visitor.UseHFile();
        visitor.renderingPrototypes = true;
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
        CFileGenerator visitor = new(cls);
        visitor.UseHFile();
        visitor.renderingPrototypes = true;
        var sb = cls.hFile.mainCode;

        foreach (var node in cls.syntaxNode.ChildNodes())
        {
            if (node is not MethodDeclarationSyntax && node is not ConstructorDeclarationSyntax)
            {
                continue;
            }

            visitor.Visit(node);
            
            // remove last characters from string buffer until we find ')'
            // this is needed because the closing parenthesis often has a newline/whitespace after it.
            while (sb[sb.Length - 1] != ')')
            {
                sb.Length--;
            }

            sb.Append(";\n");
        }

        var methods = cls.GetMethods();
        foreach (var method in methods)
        {
            cls.AddHeaderFqnDependency(method.ReturnType);

            foreach (var param in method.Parameters)
            {
                cls.AddHeaderFqnDependency(param.Type);
            }
        }
    }

}
