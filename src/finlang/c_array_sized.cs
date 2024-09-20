using finlang.Validation;

namespace finlang;

/// <summary>
/// This class must only be used as a field for now.
/// It is like `u8 data[10];` in C. If you pass it to a function, it will be passed as a c array (ie `u8 * data).   
/// </summary>
/// <typeparam name="T"></typeparam>
[ValidateFieldNoMemAttr("Don't declare fields of type `c_array_sized<T>` with the `[mem]` attribute. They don't need it.")]
[ValidateNotAParameter($"{nameof(c_array_sized<T>)}<T> cannot be used as a parameter because of how C works. Use a {nameof(c_array<T>)}<T> instead.")]
public class c_array_sized<T> : c_array<T>
{
    public c_array_sized(int size) : base(size)
    {
    }

    public c_array_sized(T[] values) : base(values)
    {
    }

    public u32 length => (u32)_simCsRealMemoryArray.Length;
}