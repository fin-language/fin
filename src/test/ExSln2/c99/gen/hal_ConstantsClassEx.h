// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `hal.ConstantsClassEx` type.
// Source file: `LedBlinker/test_stuff/ConstantsClassEx.cs` (relative to C# solution).

#pragma once

#include <stdint.h>


// Defines
#define hal_ConstantsClassEx_CPU_FREQ    16000000
#define hal_ConstantsClassEx_CPU_FREQ2    240000000


typedef struct hal_ConstantsClassEx hal_ConstantsClassEx;
struct hal_ConstantsClassEx
{

    uint32_t my_div ;
};


uint32_t hal_ConstantsClassEx_calc_as_instance(hal_ConstantsClassEx * self, uint32_t cycles);

uint32_t hal_ConstantsClassEx_calc_as_instance2(hal_ConstantsClassEx * self, uint32_t cycles);
