// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `ex_mem.DoublePoint` type.
// Source file: `LedBlinker/mem_test/DoublePoint.cs` (relative to C# solution).

#pragma once

#include "ex_mem_XyPointU8.h"
#include <stdint.h>



/// <summary>
/// Tests mem
/// </summary>
typedef struct ex_mem_DoublePoint ex_mem_DoublePoint;
struct ex_mem_DoublePoint
{
    ex_mem_XyPointU8 start ;
    ex_mem_XyPointU8 end;
};


ex_mem_DoublePoint * ex_mem_DoublePoint_ctor(ex_mem_DoublePoint * self);

uint8_t ex_mem_DoublePoint_get_start_x(ex_mem_DoublePoint * self);
