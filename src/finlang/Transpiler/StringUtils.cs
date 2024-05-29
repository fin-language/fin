using System.Text;
using System.Text.RegularExpressions;

namespace finlang.Transpiler;

public partial class StringUtils
{
    public static string DeIndent(string str)
    {
        var match = DeIndentRegex().Match(str);

        if (!match.Success)
        {
            return str;
        }

        var indent = match.Groups[1];
        var r = new Regex("^" + indent, RegexOptions.Multiline);
        var output = r.Replace(str, "");

        return output;
    }

    public static string RemoveAllHorizontalSpaceChars(string str)
    {
        var r = HorizontalWhiteSpace();
        var output = r.Replace(str, "");
        return output;
    }

    public static string RemoveAnyIndent(string str)
    {
        var r = RemoveAnyIndent();
        var output = r.Replace(str, "");
        return output;
    }

    public static void RemoveSpecificIndentSb(StringBuilder sb, string input, string indent)
    {
        bool atStartOfLine = true;
        for (int i = 0; i < input.Length; i++)
        {
            bool append = true;

            char c = input[i];
            if (c == '\n' || c == '\r')
            {
                atStartOfLine = true;
            }
            else
            {
                if (atStartOfLine)
                {
                    if (MatchesAtOffset(input, toFind: indent, i))
                    {
                        i += indent.Length - 1;  // -1 because `i++`
                        append = false;
                    }
                }
                atStartOfLine = false;
            }

            if (append)
                sb.Append(c);
        }
    }

    /// <summary>
    /// Will fail if match not found.
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="toFindAndKeep"></param>
    public static void RemoveEndCharsUntilX(StringBuilder sb, char toFindAndKeep)
    {
        while (sb[sb.Length - 1] != toFindAndKeep)
        {
            sb.Length--;
        }
    }

    internal static bool MatchesAtOffset(string a, string toFind, int aOffset)
    {
        if (aOffset < 0 || aOffset >= a.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(aOffset), "Invalid starting index");
        }

        if (a.Length - aOffset < toFind.Length)
        {
            return false; // toFind cannot fit within the remaining length of `a` starting from aOffset
        }

        for (int i = 0; i < toFind.Length; i++)
        {
            if (a[aOffset + i] != toFind[i])
            {
                return false; // mismatch found
            }
        }

        return true; // no mismatch found, strings match
    }


    public static string[] SplitIntoLinesOrEmpty(string str)
    {
        var lines = LineEndingRegex().Split(str);

        if (lines.Length == 1 && lines[0] == string.Empty)
            return Array.Empty<string>();

        return lines;
    }

    public static string ReplaceNewLineChars(string str, string replacment)
    {
        return LineEndingRegex().Replace(str, replacment);
    }

    public static string ConvertToSlashNLines(string str)
    {
        return ReplaceNewLineChars(str, "\n");
    }

    public static string DeIndentTrim(string str)
    {
        var output = DeIndent(str);
        output = TrimLeadingHsAndFirstNewline(output);
        output = TrimTrailingNewLineAndHs(output);

        return output;
    }

    public static string RemoveAnyIndentAndTrim(string str)
    {
        var output = RemoveAnyIndent(str);
        output = TrimLeadingHsAndFirstNewline(output);
        output = TrimTrailingNewLineAndHs(output);

        return output;
    }

    /// <summary>
    /// Trims trailing new line and horizontal white space
    /// </summary>
    /// <param name="output"></param>
    /// <returns></returns>
    private static string TrimTrailingNewLineAndHs(string output)
    {
        output = TrimTrailingNewLineAndHsRegex().Replace(output, "");
        return output;
    }

    /// <summary>
    /// Trims leading horizontal space and first new line
    /// </summary>
    /// <param name="output"></param>
    /// <returns></returns>
    private static string TrimLeadingHsAndFirstNewline(string output)
    {
        output = TrimLeadingHsAndFirstNewlineRegex().Replace(output, "");
        return output;
    }

    public static string EscapeCharsForString(string str)
    {
        str = ReplaceNewLineChars(str, "\\n");
        str = EscapeCharsForStringRegex().Replace(str, "\\$1");
        return str;
    }

    internal static string RemoveEverythingBefore(string str, string match, bool requireMatch = false)
    {
        var index = str.LastIndexOf(match);
        if (index < 0)
        {
            if (requireMatch)
                throw new ArgumentException($"match `{match}` not found");
            else
                return str;
        }

        return str.Substring(index + match.Length);
    }

    internal static string MaybeTruncateWithEllipsis(string str, int maxLength)
    {
        if (str.Length > maxLength)
        {
            return str.Substring(0, maxLength) + "…";
        }

        return str;
    }

    internal static string RemoveCCodeComments(string code, bool keepLineEnding = false)
    {
        var regex = RemoveCCodeCommentsRegex();

        var result = regex.Replace(code, (m) => {
            if (keepLineEnding)
                return m.Groups["lineEnding"].Value;
            return "";
        });

        return result;
    }
    
    internal static string AppendWithNewlineIfNeeded(string str, string toAppend)
    {
        AppendInPlaceWithNewlineIfNeeded(ref str, toAppend);
        return str;
    }

    internal static void AppendInPlaceWithNewlineIfNeeded(ref string str, string toAppend)
    {
        if (str.Length > 0 && toAppend.Length > 0)
        {
            str += "\n";
        }

        str += toAppend;
    }

    public static string SnakeCaseToCamelCase(string snakeCaseName)
    {
        var regex = SnakeCaseToCamelCaseRegex();
        var newName = regex.Replace(snakeCaseName, (m) => m.Groups["letterAfterUnderscore"].Value.ToUpper());

        return newName;
    }

    public static string SnakeCaseToPascalCase(string snakeCaseName)
    {
        var regex = SnakeCaseToPascalCaseRegex();
        var newName = regex.Replace(snakeCaseName, (m) => m.Groups["letterToUpperCase"].Value.ToUpper());

        return newName;
    }

    public static void EraseTrailingWhitespace(StringBuilder sb)
    {
        while (char.IsWhiteSpace(sb[^1]))
            sb.Length--;
    }

    public static bool EndsWithNewLineOptSpace(StringBuilder sb)
    {
        for (int i = sb.Length - 1; i >= 0; i--)
        {
            char c = sb[i];
            if (c == '\n' || c == '\r')
            {
                return true;
            }
            else if (c != ' ' && c != '\t')
            {
                return false;
            }
        }

        return false;
    }

    [GeneratedRegex("^\\s*?([ \\t]+)\\S")]
    private static partial Regex DeIndentRegex();
    [GeneratedRegex("^[ \\t]+", RegexOptions.Multiline)]
    private static partial Regex RemoveAnyIndent();
    [GeneratedRegex("\\r\\n|\\r|\\n")]
    private static partial Regex LineEndingRegex();
    [GeneratedRegex("(\\r\\n|\\r|\\n)[ \\t]*$")]
    private static partial Regex TrimTrailingNewLineAndHsRegex();
    [GeneratedRegex("^[ \\t]*(\\r\\n|\\r|\\n)")]
    private static partial Regex TrimLeadingHsAndFirstNewlineRegex();
    [GeneratedRegex("(\\\\|\")")]
    private static partial Regex EscapeCharsForStringRegex();
    [GeneratedRegex("(?x)\n                // .* (?<lineEnding> \\r\\n | \\r | \\n | $ )\n                |\n                /[*]\n                [\\s\\S]*? # anything, lazy\n                [*]/\n            ")]
    private static partial Regex RemoveCCodeCommentsRegex();
    [GeneratedRegex("(?x) _ (?<letterAfterUnderscore> [a-z] ) ")]
    private static partial Regex SnakeCaseToCamelCaseRegex();
    [GeneratedRegex("(?x)\n        ( ^ \\s* | _ ) # either start of input or underscore\n        (?<letterToUpperCase> [a-zA-Z] ) ")]
    private static partial Regex SnakeCaseToPascalCaseRegex();
    [GeneratedRegex("[ \\t]+")]
    private static partial Regex HorizontalWhiteSpace();
}
