// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `hal.IDigIn` type.
// Source file: `LedBlinker/hal/IDigIn.cs` (relative to C# solution).


#include "hal_IDigIn.h"


/// <summary>
/// Reads the state of the digital input.
/// </summary>
/// <returns></returns>
bool hal_IDigIn_read_state(hal_IDigIn * self)
{
    return self->obj_vtable->read_state(self->obj);
}

