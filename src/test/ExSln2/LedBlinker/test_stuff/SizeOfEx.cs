using finlang;

namespace hal;

public class SizeOfEx : FinObj
{
    public static u8 calc_param_sizes(u8 a, i32 b)
    {
        math.unsafe_mode();
        return mem.size_of(a) + mem.size_of(b);
    }

    public static u8 calc_sizeof_u8_i32()
    {
        math.unsafe_mode();
        return u8.SIZE + i32.SIZE;
    }
}