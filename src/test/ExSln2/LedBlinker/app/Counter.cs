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
    public c_array<u8> _counts;

    /// <summary>
    /// Another couple of fields.
    /// </summary>
    public u8 _count_length, _set_invocations_counts;

    public Counter(c_array<u8> counts, u8 count_length)
    {
        _counts = counts;
        _count_length = count_length;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public u8 get(u8 index)
    {
        u8 result = 0;
        if (index < _count_length)
        {
            result = _counts.unsafe_get(index);
        }
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="value"></param>
    public void set(u8 index, u8 value)
    {
        math.unsafe_mode();

        if (index >= _count_length)
            return;

        _counts.unsafe_set(index, value);
        _set_invocations_counts++;
    }
}
