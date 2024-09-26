#pragma warning disable IDE1006 // Naming Styles

namespace finlang;

/// <summary>
/// The rendered C type will have the `const` keyword.
/// https://github.com/fin-language/fin/issues/92
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Parameter)]
public class c_constAttribute : Attribute
{

}