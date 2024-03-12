using finlang;

namespace hal;

public interface ConstantsInterfaceEx : IFinObj
{
    static readonly u32 CPU_FREQ = 16000000;
    const uint CPU_FREQ2 = 240000000;

    u32 calc(u32 cycles);
}
