using finlang;

namespace hal;

public class MultipleVarsEx : FinObj
{
    /// <summary>
    /// This are some public variables
    /// </summary>
    public u8 a, b, c;

    public Led? led_a, led_b, led_c;
    public c_array<u8>? arr_a, arr_b, arr_c;
}
