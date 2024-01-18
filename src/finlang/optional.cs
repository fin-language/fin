namespace finlang;

/// <summary>
/// Not ready for use. See https://github.com/fin-language/fin/issues/17
/// </summary>
/// <typeparam name="T"></typeparam>
public class optional<T>
{
    public bool has_value;
    public T value;

    public optional(T value)
    {
        this.value = value;
    }

    public static implicit operator T(optional<T> value)
    {
        return value;
    }
}
