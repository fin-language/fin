// From StateSmith project
namespace finlang.Utils.StringUtilExtensions;

public static class StringUtilExtensionsClass
{
    public static string DeIndent(this string str)
    {
        return StringUtils.DeIndent(str);
    }

    public static string RemoveAnyIndent(this string str)
    {
        return StringUtils.RemoveAnyIndent(str);
    }

    public static string Indent(this string str, string indent)
    {
        return StringUtils.Indent(str, indent);
    }

    public static string IndentNewLines(this string str, string indent, int indentCount = 1)
    {
        var effectiveIndent = indent;

        for (int i = 1; i < indentCount; i++)
        {
            effectiveIndent += indent;
        }

        return StringUtils.IndentNewLines(str, effectiveIndent);
    }

    public static string[] SplitIntoLinesOrEmpty(this string str)
    {
        return StringUtils.SplitIntoLinesOrEmpty(str);
    }

    public static string ConvertToSlashNLines(this string str)
    {
        return StringUtils.ConvertToSlashNLines(str);
    }

    public static string DeIndentTrim(this string str)
    {
        return StringUtils.DeIndentTrim(str);
    }

    public static string RemoveAnyIndentAndTrim(this string str)
    {
        return StringUtils.RemoveAnyIndentAndTrim(str);
    }
}
