using System.Runtime.CompilerServices;

namespace finlang.TranspilerTest;

public class PathHelpers
{
    public static string GetThisDir([CallerFilePath] string path = "")
    {
        return Path.GetDirectoryName(GetThisFilePath(path))! + "/";
    }

    public static string GetThisFilePath([CallerFilePath] string path = "")
    {
        return path;
    }
}
