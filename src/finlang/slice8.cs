namespace finlang;

/// <summary>
/// Still just a concept. Not implemented yet.
/// </summary>
/// <typeparam name="T"></typeparam>
public class slice8<T>
{
    public u8 size => _size;

    /// <summary>
    /// Only accessible for test/simulation C# code. Not accessible to fin application code. Doesn't exist in generated C code.
    /// </summary>
    [simonly]
    public u8 _size;

    //public c_array<T> _elements;
}
