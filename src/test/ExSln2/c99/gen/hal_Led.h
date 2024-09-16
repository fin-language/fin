// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `hal.Led` type.
// Source file: `LedBlinker/hal/Led.cs` (relative to C# solution).

#pragma once

#include "hal_IDigInOut.h"
#include <stdint.h>



typedef struct hal_Led hal_Led;
struct hal_Led
{
    hal_IDigInOut * _dig_io;
    uint8_t my_public_var;
};


hal_Led * hal_Led_ctor(hal_Led * self, hal_IDigInOut * dig_out);

void hal_Led_toggle_twice(hal_Led * self);

void hal_Led_toggle_twice_static(hal_Led * led);

// Will toggle the state of the LED
void hal_Led_toggle(hal_Led * self);
