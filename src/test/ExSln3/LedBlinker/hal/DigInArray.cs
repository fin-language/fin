using finlang;

namespace hal;

public class DigInArray : FinObj
{
    public c_array<IDigIn> _dig_ins;
    public u8 _count;

    public DigInArray(c_array<IDigIn> dig_ins, u8 count)
    {
        _dig_ins = dig_ins;
        _count = count;
    }

    public IDigIn unsafe_get(u8 index)
    {
        return _dig_ins.unsafe_get(index);
    }

    public u8 count()
    {
        return _count;
    }
}
