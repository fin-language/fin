#pragma warning disable IDE1006 // Naming Styles


namespace finlang;

/// <summary>
/// If applied to a method, it means that the method will return a copy of the object and not a reference to the object.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
public class memAttribute : Attribute { }
