// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `hal.ConstantsClassEx` type.
// Source file: `LedBlinker/test_stuff/ConstantsClassEx.cs` (relative to C# solution).


#include "hal_ConstantsClassEx.h"



uint32_t hal_ConstantsClassEx_calc_as_instance(hal_ConstantsClassEx * self, uint32_t cycles)
{
    /* fin: math.unsafe_mode() */
    return hal_ConstantsClassEx_CPU_FREQ / cycles / self->my_div;
}

uint32_t hal_ConstantsClassEx_calc_as_instance2(hal_ConstantsClassEx * self, uint32_t cycles)
{
    /* fin: math.unsafe_mode() */
    return hal_ConstantsClassEx_CPU_FREQ2 / cycles / self->my_div;
}
