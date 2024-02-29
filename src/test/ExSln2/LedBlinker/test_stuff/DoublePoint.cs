using finlang;

namespace hal;

public class DoublePoint : FinObj
{
    [mem]public XyPoint start = mem.init(new XyPoint() { x = 1, y = 2});
    [mem]public XyPoint end = mem.init(new XyPoint() { x = 3, y = 4 });

    public DoublePoint()
    {
        
    }

    public u8 get_start_x()
    {
        return start.x;
    }
}
