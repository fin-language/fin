// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `hal.LedArray` type.
// Source file: `LedBlinker/hal/LedArray.cs` (relative to C# solution).

#pragma once

#include "hal_Led.h"
#include <stdint.h>
#include "hal_LedArray_LedInfo.h"



typedef struct hal_LedArray hal_LedArray;
struct hal_LedArray
{

    hal_Led * * _leds;
    uint8_t _leds_length;
    uint8_t _index;
    hal_LedArray_LedInfo * _led_info;
};


hal_LedArray * hal_LedArray_ctor(hal_LedArray * self, hal_Led * * leds, uint8_t leds_length, hal_LedArray_LedInfo * led_info);

hal_Led * hal_LedArray_maybe_get_led(hal_LedArray * self, uint8_t index);

void hal_LedArray_increment_index(hal_LedArray * self);

// just an excuse for a static method :)    
uint8_t hal_LedArray_inc_wrap(uint8_t value, uint8_t max);
