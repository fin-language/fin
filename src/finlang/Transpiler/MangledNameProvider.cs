namespace finlang.Transpiler;

public class MangledNameProvider : IMangledNameProvider
{
    private Dictionary<string, C99ClsEnumInterface> fqnToC99Class;

    public MangledNameProvider(Dictionary<string, C99ClsEnumInterface> fqnToC99Class)
    {
        this.fqnToC99Class = fqnToC99Class;
    }

    /// <inheritdoc />
    public string FromFinType<T>() where T : class
    {
        string fqn = typeof(T).FullName.ThrowIfNull();
        return FromFinType(fqn);
    }

    /// <inheritdoc />
    public string FromFinType(string finTypeFqn)
    {
        return fqnToC99Class[finTypeFqn].GetCName();
    }
}
