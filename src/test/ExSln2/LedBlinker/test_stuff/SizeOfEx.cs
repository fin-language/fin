using finlang;

namespace hal;

/// <summary>
/// https://github.com/fin-language/fin/issues/50
/// </summary>
public class SizeOfEx : FinObj
{
    public static u8 calc_param_sizes(u8 a, i32 b)
    {
        math.unsafe_mode();
        return mem.size_of(a) + mem.size_of(b);
    }

    public static u8 calc_param_refs_sizes(ref u8 a, ref i32 b)
    {
        math.unsafe_mode();
        return mem.size_of(a) + mem.size_of(b); // note generated doesn't return size of pointer
    }

    public static u8 calc_sizeof_u8_i32()
    {
        math.unsafe_mode();
        return u8.SIZE + i32.SIZE;
    }
}