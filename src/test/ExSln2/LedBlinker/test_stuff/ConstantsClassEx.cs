using finlang;

namespace hal;

public class ConstantsClassEx : FinObj
{
    public static readonly u32 CPU_FREQ = 16000000;
    public const uint CPU_FREQ2 = 240000000;

    public u32 my_div = 2;

    public u32 calc_as_instance(u32 cycles)
    {
        math.unsafe_mode();
        return CPU_FREQ / cycles / my_div;
    }

    public u32 calc_as_instance2(u32 cycles)
    {
        math.unsafe_mode();
        return CPU_FREQ2 / cycles / my_div;
    }
}