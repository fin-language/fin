using finlang;
using static finlang.FinC; // for ignore_unused()

namespace hal;

public class RefParamsExample : FinObj
{
    public static void test()
    {
        u8 a = 1;
        inc(ref a);
        ignore_unused(a);
    }

    public static void inc(ref u8 a)
    {
        a++;
    }

    public static u8 echo(in u8 a)
    {
        //a++; // can't assign to in parameter
        return a;
    }
}
