// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `ts.MemStackEx.Bike2` type.
// Source file: `LedBlinker/test_stuff/MemStackEx.cs` (relative to C# solution).

#pragma once

#include <stdint.h>



    typedef struct ts_MemStackEx_Bike2 ts_MemStackEx_Bike2;
struct ts_MemStackEx_Bike2
{
        uint8_t id;
        int32_t speed;
};


ts_MemStackEx_Bike2 * ts_MemStackEx_Bike2_ctor(ts_MemStackEx_Bike2 * self, int32_t speed);

ts_MemStackEx_Bike2 * ts_MemStackEx_Bike2_set_speed(ts_MemStackEx_Bike2 * self, int32_t speed);

ts_MemStackEx_Bike2 * ts_MemStackEx_Bike2_set_id(ts_MemStackEx_Bike2 * self, uint8_t id);