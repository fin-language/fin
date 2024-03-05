// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang v0.3.0-alpha generated this file for C# `hal.IDigInOut` type.
// Source file: `LedBlinker/hal/IDigInOut.cs` (relative to C# solution).
// MD5 hash of source file: 130e643f5dc5bd1bb361000566b2fdd9.


#include "hal_IDigInOut.h"


/// <summary>
/// Reads the state of the digital input.
/// </summary>
/// <returns></returns>
bool hal_IDigInOut_read_input(hal_IDigInOut * self)
{
    return self->obj_vtable->read_input(self->obj);
}

/// <summary>
/// Sets the state of the digital output.
/// </summary>
/// <param name="state"></param>
void hal_IDigInOut_set_output_state(hal_IDigInOut * self, bool state)
{
    self->obj_vtable->set_output_state(self->obj, state);
}


bool hal_IDigInOut_get_output_state(hal_IDigInOut * self)
{
    return self->obj_vtable->get_output_state(self->obj);
}


void hal_IDigInOut_toggle(hal_IDigInOut * self)
{
    self->obj_vtable->toggle(self->obj);
}

