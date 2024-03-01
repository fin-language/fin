using finlang;

namespace ex_mem;

/// <summary>
/// Tests mem
/// </summary>
public class QuadPoint : FinObj
{
    [mem]public DoublePoint d1 = mem.init(new DoublePoint());
    [mem]public DoublePoint d2 = mem.init(new DoublePoint());
    
    public QuadPoint()
    {
        
    }

    public u8 get_start_x()
    {
        return d1.get_start_x();
    }

    public u8 get_start_y()
    {
        return d1.start.y;
    }
}