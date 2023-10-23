using System;

namespace fin.lang;

public class c_array<T>
{
    /// <summary>
    /// Only accessible for test/simulation C# code. Not accessible to fin application code. Doesn't exist in generated C code.
    /// </summary>
    [simonly]
    public T[] _cSharpArray;

    public c_array(int size)
    {
        _cSharpArray = new T[size];
    }

    public c_array(T[] values)
    {
        _cSharpArray = values;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= _cSharpArray.Length)
            {
                throw new IndexOutOfRangeException($"Attempted reading invalid index `{index}` of C style naked array of length {_cSharpArray.Length}. https://github.com/fin-language/fin/issues/14");
            }
            return _cSharpArray[index];
        }

        set
        {
            if (index < 0 || index >= _cSharpArray.Length)
            {
                throw new IndexOutOfRangeException($"Attempted writing value `{value}` to invalid index `{index}` of C style naked array of length {_cSharpArray.Length}. https://github.com/fin-language/fin/issues/14");
            }
            _cSharpArray[index] = value;
        }
    }

}
