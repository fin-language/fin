namespace fin.lang.gen;

public class DocHelper
{
    public static string Code(string code)
    {
        return $@"<code>{Escape(code)}</code>";
    }

    public static string Escape(string str)
    {
        return str.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
    }
}
