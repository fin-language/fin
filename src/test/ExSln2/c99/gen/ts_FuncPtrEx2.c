// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `ts.FuncPtrEx2` type.
// Source file: `LedBlinker/test_stuff/FuncPtrEx2.cs` (relative to C# solution).


#include "ts_FuncPtrEx2.h"
#include <string.h>


void ts_FuncPtrEx2_ctor(ts_FuncPtrEx2 * self)
{
    memset(self, 0, sizeof(*self));
    self->func = ts_FuncPtrEx2_add;
}

int32_t ts_FuncPtrEx2_add(int32_t a, int32_t b)
{
    return a + b;
}

int32_t ts_FuncPtrEx2_sub(int32_t a, int32_t b)
{
    return a - b;
}

void ts_FuncPtrEx2_use_sub(ts_FuncPtrEx2 * self)
{
    self->func = ts_FuncPtrEx2_sub;
}

void ts_FuncPtrEx2_set(ts_FuncPtrEx2 * self, ts_FuncPtrEx2_FuncPtr f)
{
    self->func = f;
}

int32_t ts_FuncPtrEx2_Run(ts_FuncPtrEx2 * self, int32_t a, int32_t b)
{
    return self->func(a, b);
}
