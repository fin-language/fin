// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `hal.CArrayDependencyTest` type.
// Source file: `LedBlinker/hal/CArrayDependencyTest.cs` (relative to C# solution).

#pragma once

#include <stdint.h>



typedef struct hal_CArrayDependencyTest hal_CArrayDependencyTest;
struct hal_CArrayDependencyTest
{
    // should have dependency on uint8_t
    uint8_t * _data;
};


hal_CArrayDependencyTest * hal_CArrayDependencyTest_ctor(hal_CArrayDependencyTest * self, uint8_t * data);
