// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `hal.FuncPtrEx2` type.
// Source file: `LedBlinker/test_stuff/FuncPtrEx2.cs` (relative to C# solution).


#include "hal_FuncPtrEx2.h"
#include <string.h>


void hal_FuncPtrEx2_ctor(hal_FuncPtrEx2 * self)
{
    memset(self, 0, sizeof(*self));
    self->func = hal_FuncPtrEx2_add;
}

int32_t hal_FuncPtrEx2_add(int32_t a, int32_t b)
{
    return a + b;
}

int32_t hal_FuncPtrEx2_sub(int32_t a, int32_t b)
{
    return a - b;
}

void hal_FuncPtrEx2_use_sub(hal_FuncPtrEx2 * self)
{
    self->func = hal_FuncPtrEx2_sub;
}

void hal_FuncPtrEx2_set(hal_FuncPtrEx2 * self, hal_FuncPtrEx2_FuncPtr f)
{
    self->func = f;
}

int32_t hal_FuncPtrEx2_Run(hal_FuncPtrEx2 * self, int32_t a, int32_t b)
{
    return self->func(a, b);
}
