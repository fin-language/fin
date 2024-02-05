namespace finlang.Transpiler;

public class DependencyResolver
{
    private Dictionary<string, C99ClsEnum> fqnToC99Class;

    public DependencyResolver(Dictionary<string, C99ClsEnum> fqnToC99Class)
    {
        this.fqnToC99Class = fqnToC99Class;
    }

    public string? ResolveDependency(string fqnDependency)
    {
        string? result = null;
        switch (fqnDependency)
        {
            case "System.Void": result = null; break;
            case "System.Bool": result = "<stdbool.h>"; break;
            case "finlang.u8":  
            case "finlang.u16": 
            case "finlang.u32": 
            case "finlang.u64": 
            case "finlang.i8":  
            case "finlang.i16": 
            case "finlang.i32": 
            case "finlang.i64": result = "<stdint.h>"; break;
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