using finlang;

namespace hal;

public class ConstantsStaticClassEx : FinObj
{
    public static readonly u32 CPU_FREQ = 16000000;
    public const uint CPU_FREQ2 = 240000000;

    public static u32 calc_as_static(u32 cycles)
    {
        math.unsafe_mode();
        return CPU_FREQ / cycles;
    }

    public static u32 calc_as_static2(u32 cycles)
    {
        math.unsafe_mode();
        return CPU_FREQ2 / cycles;
    }
}
