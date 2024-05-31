using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;
using System.Text;

namespace finlang.Transpiler;

public class HeaderGenerator
{
    StyleSettings styleSettings;
    protected string NL => styleSettings.newLine;

    public HeaderGenerator(StyleSettings styleSettings)
    {
        this.styleSettings = styleSettings;
    }

    public void GenerateEnum(C99ClsEnumInterface cls)
    {
        CFileGenerator visitor = new(cls, styleSettings);
        visitor.UseHFile();
        visitor.renderingPrototypes = true;

        if (!cls.IsEnum)
        {
            throw new InvalidOperationException("Object has to be an enum for this code.");
        }

        visitor.Visit(cls.syntaxNode);
    }

    public void GenerateStructures(C99ClsEnumInterface cls)
    {
        var structName = cls.GetCName();
        CFileGenerator visitor = new(cls, styleSettings);
        visitor.UseHFile();
        visitor.renderingPrototypes = true;
        var sb = cls.hFile.mainCodeSb;

        // don't generate a struct for FFI classes
        if (cls.IsFFIClass)
        {
            sb.Append($"// Class is a Foreign Function Interface. No struct generated.{NL}");
            return;
        }

        if (cls.IsStaticClass)
        {
            sb.Append($"// Class has no fields. No struct generated.{NL}");
            return;
        }

        ClassDeclarationSyntax clsDeclSyntax = (ClassDeclarationSyntax)cls.syntaxNode;
        visitor.VisitLeadingTrivia(clsDeclSyntax);
        sb.Append($"typedef struct {structName} {structName};{NL}");
        sb.Append($"struct {structName}{NL}");
        sb.Append($"{{{NL}");

        foreach (var field in cls.syntaxNode.ChildNodes().OfType<FieldDeclarationSyntax>())
        {
            if (!field.IsStatic() && !field.IsConst() && !field.IsSimOnly())
                visitor.VisitFieldDeclaration(field);
        }

        foreach (var field in cls.GetInstanceFields())
        {
            cls.AddHeaderFqnDependency(field.Type);
        }

        sb.Append($"}};{NL}");
        sb.Append(NL);
    }

    public void GenerateCDefines(C99ClsEnumInterface cls, StringBuilder sb)
    {
        IEnumerable<IFieldSymbol> defineFields = cls.GetCDefineFields();

        if (!defineFields.Any())
            return;
        
        sb.Append($"// Defines{NL}");

        foreach (var f in defineFields)
        {
            var name = Namer.GetCName(f);

            object? value = f.ConstantValue;

            if (value == null)
            {
                // try to get the value from the initializer
                SyntaxNode syntaxNode = f.DeclaringSyntaxReferences.Single().GetSyntax();
                var initializer = syntaxNode.DescendantNodes().OfType<EqualsValueClauseSyntax>().FirstOrDefault();

                if (initializer == null)
                    throw new TranspilerException("Field has no constant value and no initializer. Cannot generate C define.", syntaxNode);

                value = initializer.Value;
            }

            sb.Append($"#define {name}    {value}{NL}");
        }

        sb.Append(NL);
    }

    public void GenerateFunctionPrototypes(C99ClsEnumInterface cls)
    {
        CFileGenerator visitor = new(cls, styleSettings);
        var sb = new StringBuilder();
        visitor.SetSb(sb);
        visitor.renderingPrototypes = true;

        foreach (var node in cls.syntaxNode.ChildNodes())
        {
            if (node is MethodDeclarationSyntax || node is ConstructorDeclarationSyntax)
            {
                var bmds = (BaseMethodDeclarationSyntax)node;

                if (bmds.IsSimOnly())
                    continue;

                if (bmds.IsFinNonPublic())
                    continue;

                visitor.Visit(node);

                StringUtils.EraseTrailingWhitespace(sb); // this is needed because the closing parenthesis often has a newline/whitespace after it.

                sb.Append($";{NL}");

                var symbol = cls.model.GetDeclaredSymbol(bmds).ThrowIfNull();
                TrackMethodDependencies(cls, (IMethodSymbol)symbol);
            }
        }

        var result = StringUtils.DeIndent(sb.ToString());
        cls.hFile.mainCodeSb.Append(result);
    }

    public static void TrackMethodDependencies(C99ClsEnumInterface cls, IMethodSymbol method)
    {
        cls.AddHeaderFqnDependency(method.ReturnType);

        foreach (var param in method.Parameters)
        {
            cls.AddHeaderFqnDependency(param.Type);
        }
    }
}
