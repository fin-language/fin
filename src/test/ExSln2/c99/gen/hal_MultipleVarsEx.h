// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `hal.MultipleVarsEx` type.
// Source file: `LedBlinker\hal\MultipleVarsEx.cs` (relative to C# solution).

#pragma once

#include <stdint.h>
#include "hal_Led.h"



typedef struct hal_MultipleVarsEx hal_MultipleVarsEx;
struct hal_MultipleVarsEx
{
    /// <summary>
    /// This are some public variables
    /// </summary>
    uint8_t a, b, c;

    hal_Led * led_a, led_b, led_c;
    uint8_t *  arr_a, arr_b, arr_c;
};

