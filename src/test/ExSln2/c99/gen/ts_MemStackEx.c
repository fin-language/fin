// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `ts.MemStackEx` type.
// Source file: `LedBlinker/test_stuff/MemStackEx.cs` (relative to C# solution).


#include "ts_MemStackEx.h"
#include "ts_MemStackEx_Bike1.h"
#include "ts_MemStackEx_Bike2.h"



int32_t ts_MemStackEx_calc_stuff(int32_t b1_speed, int32_t b2_speed)
{
    // leading comment
    ts_MemStackEx_Bike1 * b1 = &(ts_MemStackEx_Bike1){0}; ts_MemStackEx_Bike1_ctor(b1); // trailing comment
    b1->speed = b1_speed;

    // below uses: `using static finlang.mem;`
    ts_MemStackEx_Bike2 * b2 = &(ts_MemStackEx_Bike2){0}; ts_MemStackEx_Bike2_ctor(b2, b2_speed); // implicit mem
    return b1->speed + b2->speed;
}
