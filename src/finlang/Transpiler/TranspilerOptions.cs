namespace finlang.Transpiler;

public class TranspilerOptions
{
    public bool OutputVersionInfo = true;
    public bool OutputTimestamp = true;
    public bool OutputChecksum = true;

    /// <summary>
    /// https://github.com/fin-language/fin/issues/69
    /// </summary>
    public string FfiHeaderNamePostfix = "_ffi.h";

    public StyleSettings StyleSettings = new();
}
