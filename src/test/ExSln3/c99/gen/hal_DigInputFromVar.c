// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `hal.DigInputFromVar` type.
// Source file: `LedBlinker/hal/DigInputFromVar.cs` (relative to C# solution).
// MD5 hash of source file: fa883057055fe3eb2deb89555e407125.


#include "hal_DigInputFromVar.h"



bool hal_DigInputFromVar_read_input(hal_DigInputFromVar * self)
{
    return self->last_state;
}

// virtual table implementation for IDigIn. Note that this is extern'd.
const hal_IDigIn_vtable hal_DigInputFromVar__hal_IDigIn_vtable_imp = {
    .read_input = (bool (*)(void * self))hal_DigInputFromVar_read_input,
};

