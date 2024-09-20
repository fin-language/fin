namespace finlang.Validation;

/// <summary>
/// Classes with this attribute cannot be used as a field in a class or struct with the `mem` attribute.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class ValidateFieldNoMemAttrAttribute: Attribute
{
    public ValidateFieldNoMemAttrAttribute(string message)
    {
    }
}

/// <summary>
/// Classes with this attribute cannot be used as a parameter in a function. Introduced for `c_array_sized<T>` which should only be used as a field/local var.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class ValidateNotAParameterAttribute : Attribute
{
    public ValidateNotAParameterAttribute(string message)
    {
    }
}