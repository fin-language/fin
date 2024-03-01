namespace finlang;

/// <summary>
/// This class must only be used as a field for now.
/// It is like `u8 data[10];` in C. If you pass it to a function, it will be passed as a c array (ie `u8 * data).   
/// </summary>
/// <typeparam name="T"></typeparam>
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