#pragma warning disable IDE1006 // Naming Styles


namespace finlang;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter)]
public class override_typeAttribute : Attribute {
    public string Type { get; init; }
    //public string Value { get; init; }

    public override_typeAttribute(string type)
    {
        this.Type = type;
    }
}