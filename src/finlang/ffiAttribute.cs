#pragma warning disable IDE1006 // Naming Styles

using System;

namespace finlang;

/// <summary>
/// Foreign Function Interface. This attribute is used to mark a method as a foreign function.
/// Classes marked with this attribute will NOT be transpiled to C99.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ffiAttribute : Attribute {

}
