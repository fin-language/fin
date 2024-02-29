using finlang;

namespace hal;

public class VarKeywordEx : FinObj
{
    public static i16 add(u8 a, i8 b)
    {
        //fin: var result = a + b;
        var result = a + b; // should end up as i16
        return result;
    }
}
