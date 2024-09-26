namespace finlang;

/// <summary>
/// A naked C style array of memory structs.<br/>
/// Ex: `<![CDATA[c_array_mem<Bike> bikes]]>` transpiles to `Bike * bikes` in C.<br/>
/// Because this is an array of memory structs, you can only get pointers to the structs.
/// </summary>
/// <typeparam name="T"></typeparam>
public class c_array_mem<T> : FinObj where T : FinObj, new()
{
    /// <summary>
    /// Only accessible for test/simulation C# code. Not accessible to fin application code. Doesn't exist in generated C code.
    /// </summary>
    [simonly]
    public readonly T[] _simCsRealMemoryArray;

    public c_array_mem(int size)
    {
        if (size == 0)
            throw new ArgumentException("Array size must be greater than 0.");

        _simCsRealMemoryArray = new T[size];

        for (int i = 0; i < size; i++)
            _simCsRealMemoryArray[i] = new T();
    }

    /// <summary>
    /// Only checked during simulation.<br/>
    /// Not checked in generated C code.<br/>
    /// </summary>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public T unsafe_get(i64 index)
    {
        if (index < 0 || index >= _simCsRealMemoryArray.Length)
        {
            throw new IndexOutOfRangeException($"Attempted reading invalid index `{index}` of C style naked array of length {_simCsRealMemoryArray.Length}. https://github.com/fin-language/fin/issues/14");
        }
        return _simCsRealMemoryArray[index];
    }
}
