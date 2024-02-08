// finlang generated file for c# hal.LedArray type
#pragma once

#include "hal_Led.h"
#include <stdint.h>



typedef struct hal_LedArray hal_LedArray;
struct hal_LedArray
{
    hal_Led * * leds;
};


void hal_LedArray_ctor(hal_LedArray * self, hal_Led * * leds);

hal_Led * hal_LedArray_getLed(hal_LedArray * self, uint8_t index);
