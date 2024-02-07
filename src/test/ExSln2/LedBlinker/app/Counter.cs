using finlang;

namespace app;

/// <summary>
/// A simple counter class.
/// </summary>
public class Counter : FinObj
{
    /// <summary>
    /// The counts array.
    /// </summary>
    c_array<u8> _counts;

    /// <summary>
    /// Another couple of fields.
    /// </summary>
    u8 _count_length, _index;

    public Counter(c_array<u8> counts, u8 count_length)
    {
        _counts = counts;
        _count_length = count_length;
    }

    public u8 get(u8 index)
    {
        u8 result = 0;
        if (index < _count_length)
        {
            result = _counts.unsafe_get(index);
        }
        return result;
    }

    public void set(u8 index, u8 value)
    {
        if (index < _count_length)
            _counts.unsafe_set(index, value);
    }
}
