using finlang;

namespace hal;

public class Helper : FinObj
{
    public static u8 calc_stuff(u8 a, u8 b)
    {
        return (a + b).wrap_lshift(b + 1);
    }
}
