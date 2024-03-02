#pragma warning disable IDE1006 // Naming Styles

namespace finlang;

/// <summary>
/// https://github.com/fin-language/fin/issues/44
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class add_includeAttribute : Attribute
{
    public string Include { get; }

    public add_includeAttribute(string include)
    {
        this.Include = include;
    }
}
