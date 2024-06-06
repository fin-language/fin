// From StateSmith project
namespace finlang.Utils.StringUtilsExtensions;

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

    public static string Indent(this string str, string indent, int count = 1)
    {
        return StringUtils.Indent(str, indent, count);
    }

    public static string IndentNewLines(this string str, string indent, int count = 1)
    {
        return StringUtils.IndentNewLines(str, indent: indent, count: count);
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
