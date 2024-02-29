using finlang;

namespace hal;

public class MemExample1 : FinObj
{
    [mem]XyPoint start = mem.init(new XyPoint() { x = 1, y = 2});
    [mem]XyPoint end = mem.init(new XyPoint() { x = 3, y = 4 });
    
    public u8 get_start_x()
    {
        return start.x;
    }
}