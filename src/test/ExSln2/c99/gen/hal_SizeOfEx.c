// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `hal.SizeOfEx` type.
// Source file: `LedBlinker/test_stuff/SizeOfEx.cs` (relative to C# solution).


#include "hal_SizeOfEx.h"


uint8_t hal_SizeOfEx_calc_param_sizes(uint8_t a, int32_t b)
{
    /* fin: math.unsafe_mode() */
    return sizeof(uint8_t /*a*/) + sizeof(int32_t /*b*/);
}

uint8_t hal_SizeOfEx_calc_sizeof_u8_i32()
{
    /* fin: math.unsafe_mode() */
    return sizeof(uint8_t) + sizeof(int32_t);
}