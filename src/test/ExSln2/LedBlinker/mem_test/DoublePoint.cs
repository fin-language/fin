using finlang;

namespace ex_mem;

/// <summary>
/// Tests mem
/// </summary>
public class DoublePoint : FinObj
{
    [mem] public XyPointU8 start = mem.init(new XyPointU8());
    [mem] public XyPointU8 end;

    public DoublePoint()
    {
        end = mem.init(new XyPointU8());
    }

    public u8 get_start_x()
    {
        return start.x;
    }
}
