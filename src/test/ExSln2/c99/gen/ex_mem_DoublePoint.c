// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `ex_mem.DoublePoint` type.
// Source file: `LedBlinker\mem_test\DoublePoint.cs` (relative to C# solution).


#include "ex_mem_DoublePoint.h"
#include <string.h>



void ex_mem_DoublePoint_ctor(ex_mem_DoublePoint * self)
{
    memset(self, 0, sizeof(*self));
    self->start = finlang_mem_init(new ex_mem_XyPointU8 *());
    self->end = finlang_mem_init(new ex_mem_XyPointU8 *());
}

uint8_t ex_mem_DoublePoint_get_start_x(ex_mem_DoublePoint * self)
{
    return self->start.x;
}
