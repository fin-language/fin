using finlang;

namespace hal;

public class DigOutArray : FinObj
{
    public c_array<IDigOut> _dig_outs;
    public u8 _count;

    public DigOutArray(c_array<IDigOut> dig_outs, u8 count)
    {
        _dig_outs = dig_outs;
        _count = count;
    }

    public IDigOut unsafe_get(u8 index)
    {
        return _dig_outs.unsafe_get(index);
    }

    public u8 count()
    {
        return _count;
    }
}
