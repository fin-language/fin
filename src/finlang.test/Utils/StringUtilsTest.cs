using finlang.Utils;

namespace finlang.test.Utils;

public class StringUtilsTest
{
    [Fact]
    public void Indent()
    {
        StringUtils.Indent("input", indent: "  ").Should().Be("  input");
        StringUtils.Indent("line1\nline2", indent: "\t").Should().Be("\tline1\n\tline2");
    }

    [Fact]
    public void Indent_Count()
    {
        StringUtils.Indent("input", indent: "  ", count: 0).Should().Be("input");
        StringUtils.Indent("input", indent: "  ", count: 1).Should().Be("  input");
        StringUtils.Indent("input", indent: "  ", count: 2).Should().Be("    input");
        StringUtils.Indent("line1\nline2", indent: "\t", count: 2).Should().Be("\t\tline1\n\t\tline2");
    }
}
