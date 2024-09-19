using finlang.Validation;

namespace finlang;

/// <summary>
/// A naked C style array. It transpiles to a C pointer to type T.<br/>
/// Ex: `<![CDATA[c_array<u8> data]]>` transpiles to `u8 * data` in C.<br/>
/// Ex: `<![CDATA[c_array<Bike> bikes]]>` transpiles to `Bike * * bikes` in C.<br/>
/// </summary>
/// <typeparam name="T"></typeparam>
[ValidateFieldNoMemAttr("Don't declare fields of type `c_array<T>` with the `[mem]` attribute. They don't need it.")]
public class c_array<T> : FinObj
{
    /// <summary>
    /// You probably want to use <see cref="SimGetValues"/> instead.<br/>
    /// This returns backing array with no offset.<br/>
    /// Only accessible for test/simulation C# code. Not accessible to fin application code. Doesn't exist in generated C code.
    /// </summary>
    [simonly]
    public readonly T[] _simCsRealMemoryArray;

    /// <summary>
    /// Allows having a c like array that functions more like a pointer.<br/>
    /// TODO: don't allow `<![CDATA[c_array<mem<T>>]]>` to have an offset as that makes no sense.<br/>
    /// Only accessible for test/simulation C# code. Not accessible to fin application code. Doesn't exist in generated C code.
    /// </summary>
    [simonly]
    public readonly i32 _simOffset;

    public c_array(int size)
    {
        if (size == 0)
            throw new ArgumentException("Array size must be greater than 0.");

        _simCsRealMemoryArray = new T[size];
    }

    [simonly]
    public static implicit operator c_array<T>(T[] values)
    {
        return new(values);
    }

    [simonly]
    public T[] SimGetValues()
    {
        math.unsafe_mode(); // for subtraction
        T[] result = new T[_simCsRealMemoryArray.Length - _simOffset];
        Array.Copy(sourceArray: _simCsRealMemoryArray, sourceIndex: _simOffset, destinationArray: result, destinationIndex: 0, length: result.Length);
        return result;
    }

    /// <summary>
    /// Copies values into array.
    /// </summary>
    /// <param name="values"></param>
    public c_array(T[] values) : this(values.Length)
    {
        values.CopyTo(_simCsRealMemoryArray, 0);
    }

    /// <summary>
    /// Allows having a c like array that functions more like a pointer.<br/>
    /// </summary>
    /// <param name="array_to_point_to"></param>
    /// <param name="offset_into_existing_array"></param>
    /// <exception cref="ArgumentException"></exception>
    [simonly]
    internal static c_array<T> AliasCSharpArray(T[] array_to_point_to, i32 offset_into_existing_array)
    {
        return new(array_to_point_to, offset_into_existing_array);
    }

    /// <summary>
    /// Allows having a c like array that functions more like a pointer.<br/>
    /// </summary>
    /// <param name="array_to_point_to"></param>
    /// <param name="offset_into_existing_array"></param>
    /// <exception cref="ArgumentException"></exception>
    private c_array(T[] array_to_point_to, i32 offset_into_existing_array)
    {
        _simCsRealMemoryArray = array_to_point_to;

        // TODOLOW should we really throw here? it might be better to catch on array element access instead.
        // someone might have a silly starting offset, but then account for it correctly later.
        if (offset_into_existing_array < 0 || offset_into_existing_array >= array_to_point_to.Length)
            throw new ArgumentException($"Offset `{offset_into_existing_array}` is out of bounds of array of length `{array_to_point_to.Length}`.");

        _simOffset = offset_into_existing_array;
    }

    public c_array<T> alias_with_offset(i32 offset)
    {
        math.unsafe_mode(); // for addition
        return new(_simCsRealMemoryArray, _simOffset + offset);
    }

    /// <summary>
    /// Only checked during simulation.<br/>
    /// Not checked in generated C code.<br/>
    /// </summary>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public T unsafe_get(i64 index)
    {
        long originalIndex = index;
        long effective_index = checked(originalIndex + _simOffset); // avoid fin integer math for now because we aren't currently tracking math scope inside fin sim lib right now

        if (effective_index < 0 || effective_index >= _simCsRealMemoryArray.Length)
        {
            string indexText = GetIndexText(effective_index, originalIndex);
            throw new IndexOutOfRangeException($"Attempted reading invalid index `{indexText}` of C style naked array of length {_simCsRealMemoryArray.Length}. https://github.com/fin-language/fin/issues/14");
        }
        return _simCsRealMemoryArray[effective_index];
    }

    /// <summary>
    /// Only checked during simulation.<br/>
    /// Not checked in generated C code.<br/>
    /// </summary>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public void unsafe_set(i64 index, T value)
    {
        long originalIndex = index;
        long effective_index = checked(originalIndex + _simOffset); // avoid fin integer math for now because we aren't currently tracking math scope inside fin sim lib right now

        if (effective_index < 0 || effective_index >= _simCsRealMemoryArray.Length)
        {
            string indexText = GetIndexText(effective_index, originalIndex);
            throw new IndexOutOfRangeException($"Attempted writing value `{value}` to invalid index `{indexText}` of C style naked array of length {_simCsRealMemoryArray.Length}. https://github.com/fin-language/fin/issues/14");
        }

        _simCsRealMemoryArray[effective_index] = value;
    }

    /// <summary>
    /// Useful for when a c_array is actually an alias of another array.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="originalIndex"></param>
    /// <returns></returns>
    private string GetIndexText(i64 index, long originalIndex)
    {
        string indexText;

        if (_simOffset == 0)
            indexText = index._csReadValue.ToString();
        else
            indexText = $"{index} ({originalIndex} + {_simOffset} alias offset)";
        return indexText;
    }
}
