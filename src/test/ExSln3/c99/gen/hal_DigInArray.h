// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang v0.2.6-alpha generated this file for C# `hal.DigInArray` type.
// Source file: `LedBlinker\hal\DigInArray.cs` (relative to C# solution).
// MD5 hash of source file: 616bacbbc1d95e847dab32a8ef14d855.

#pragma once

#include <stdint.h>
#include "hal_IDigIn.h"



typedef struct hal_DigInArray hal_DigInArray;
struct hal_DigInArray
{
    hal_IDigIn * * _dig_ins;
    uint8_t _count;
};


void hal_DigInArray_ctor(hal_DigInArray * self, hal_IDigIn * * dig_ins, uint8_t count);

hal_IDigIn * hal_DigInArray_unsafe_get(hal_DigInArray * self, uint8_t index);

uint8_t hal_DigInArray_count(hal_DigInArray * self);