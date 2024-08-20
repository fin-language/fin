using finlang;

namespace hal;

/// <summary>
/// https://github.com/fin-language/fin/issues/80
/// </summary>
public class NumericsMinMaxConstants : FinObj
{
    public static u8 get_max()
    {
        return u8.MAX;
    }
}
