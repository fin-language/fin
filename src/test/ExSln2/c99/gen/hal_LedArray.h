// finlang generated file for c# hal.LedArray type
#pragma once

#include <stdint.h>
#include "hal_Led.h"



typedef struct hal_LedArray hal_LedArray;
struct hal_LedArray
{
    hal_Led * * _leds;
    uint8_t _leds_length;
};


void hal_LedArray_ctor(hal_LedArray * self, hal_Led * * leds, uint8_t leds_length);

/// <summary>
/// This is actually a private method
/// </summary>
hal_Led * PRIVATE_hal_LedArray__get_led(hal_LedArray * self, uint8_t index);

hal_Led * hal_LedArray_maybe_get_led(hal_LedArray * self, uint8_t index);
