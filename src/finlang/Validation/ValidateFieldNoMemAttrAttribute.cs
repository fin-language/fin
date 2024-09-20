namespace finlang.Validation;

/// <summary>
/// Classes and structs with this attribute cannot be used as a field in a class or struct with the `mem` attribute.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class ValidateFieldNoMemAttrAttribute: Attribute
{
    public ValidateFieldNoMemAttrAttribute(string message)
    {
    }
}

