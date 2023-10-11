﻿// From StateSmith project
namespace fin.sim.gen;

public static class StringUtilExtensions
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

    public static string IndentNewLines(this string str, string indent)
    {
        return StringUtils.IndentNewLines(str, indent);
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