namespace finlang.Transpiler;

public class DependencyResolver
{
    private readonly Dictionary<string, C99ClsEnum> fqnToC99Class;

    public DependencyResolver(Dictionary<string, C99ClsEnum> fqnToC99Class)
    {
        this.fqnToC99Class = fqnToC99Class;
    }

    public string? ResolveDependency(string fqnDependency)
    {
        string? result = null;
        switch (fqnDependency)
        {
            // c# types
            case "System.Void": 
                result = null; break;
            case nameof(System) + "." + nameof(System.Boolean):
                result = "<stdbool.h>"; break;

            // fin types
            case nameof(finlang) + "." + nameof(finlang.u8):
            case nameof(finlang) + "." + nameof(finlang.u16):
            case nameof(finlang) + "." + nameof(finlang.u32):
            case nameof(finlang) + "." + nameof(finlang.u64):
            case nameof(finlang) + "." + nameof(finlang.i8):
            case nameof(finlang) + "." + nameof(finlang.i16):
            case nameof(finlang) + "." + nameof(finlang.i32):
            case nameof(finlang) + "." + nameof(finlang.i64):
                result = "<stdint.h>"; break;
        }

        if (result == null)
        {
            if (fqnToC99Class.TryGetValue(key: fqnDependency, out C99ClsEnum? cls))
            {
                result =  "\"" + cls.hFile.relativeFilePath + "\"";
            }
        }

        return result;
    }
}