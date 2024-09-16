using finlang.Utils;
using finlang.Utils.StringUtilsExtensions;

namespace finlang.test.Utils;

public class StringUtilsTest
{
    [Fact]
    public void Indent()
    {
        StringUtils.Indent("input", indent: "  ").Should().Be("  input");
        StringUtils.Indent("line1\nline2", indent: "\t").Should().Be("\tline1\n\tline2");
        StringUtils.Indent("line1\r\nline2", indent: "\t").Should().Be("\tline1\r\n\tline2");
        "input".Indent("  ").Should().Be("  input");
        "line1\nline2".Indent("\t").Should().Be("\tline1\n\tline2");
        "line1\r\nline2".Indent("\t").Should().Be("\tline1\r\n\tline2");
    }

    [Fact]
    public void Indent_Count()
    {
        StringUtils.Indent("input", indent: "  ", count: 0).Should().Be("input");
        StringUtils.Indent("input", indent: "  ", count: 1).Should().Be("  input");
        StringUtils.Indent("input", indent: "  ", count: 2).Should().Be("    input");
        StringUtils.Indent("line1\nline2", indent: "\t", count: 2).Should().Be("\t\tline1\n\t\tline2");
        StringUtils.Indent("line1\r\nline2", indent: "\t", count: 2).Should().Be("\t\tline1\r\n\t\tline2");
        "input".Indent("  ", count: 0).Should().Be("input");
        "input".Indent("  ", count: 1).Should().Be("  input");
        "input".Indent("  ", count: 2).Should().Be("    input");
        "line1\nline2".Indent("\t", count: 2).Should().Be("\t\tline1\n\t\tline2");
        "line1\r\nline2".Indent("\t", count: 2).Should().Be("\t\tline1\r\n\t\tline2");
    }

    [Fact]
    public void IndentNewLines()
    {
        StringUtils.IndentNewLines("input", indent: "  ").Should().Be("input");
        StringUtils.IndentNewLines("line1\nline2", indent: "    ").Should().Be("line1\n    line2");
        StringUtils.IndentNewLines("line1\nline2", indent: "\t").Should().Be("line1\n\tline2");
        StringUtils.IndentNewLines("line1\r\nline2", indent: "\t").Should().Be("line1\r\n\tline2");
        "input".IndentNewLines("  ").Should().Be("input");
        "line1\nline2".IndentNewLines("    ").Should().Be("line1\n    line2");
        "line1\nline2".IndentNewLines("\t").Should().Be("line1\n\tline2");
        "line1\r\nline2".IndentNewLines("\t").Should().Be("line1\r\n\tline2");
    }

    [Fact]
    public void IndentNewLines_Count()
    {
        StringUtils.IndentNewLines("input", indent: "  ", count: 0).Should().Be("input");
        StringUtils.IndentNewLines("input", indent: "  ", count: 1).Should().Be("input");
        StringUtils.IndentNewLines("input", indent: "  ", count: 2).Should().Be("input");
        StringUtils.IndentNewLines("line1\nline2", indent: "\t", count: 2).Should().Be("line1\n\t\tline2");
        StringUtils.IndentNewLines("line1\r\nline2", indent: "\t", count: 2).Should().Be("line1\r\n\t\tline2");
        "input".IndentNewLines("  ", count: 0).Should().Be("input");
        "input".IndentNewLines("  ", count: 1).Should().Be("input");
        "input".IndentNewLines("  ", count: 2).Should().Be("input");
        "line1\nline2".IndentNewLines("\t", count: 2).Should().Be("line1\n\t\tline2");
        "line1\r\nline2".IndentNewLines("\t", count: 2).Should().Be("line1\r\n\t\tline2");
    }

    [Fact]
    public void ParseCTypeInfo()
    {
        string varTypeName;
        string varTypeQualifiers;

        StringUtils.ParseCTypeInfo("int", out varTypeName, out varTypeQualifiers);
        varTypeName.Should().Be("int");
        varTypeQualifiers.Should().BeEmpty();

        StringUtils.ParseCTypeInfo("int *", out varTypeName, out varTypeQualifiers);
        varTypeName.Should().Be("int ");
        varTypeQualifiers.Should().Be("*");

        StringUtils.ParseCTypeInfo("int * * ", out varTypeName, out varTypeQualifiers);
        varTypeName.Should().Be("int ");
        varTypeQualifiers.Should().Be("* * ");
    }
}
