// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang v0.3.0-alpha generated this file for C# `hal.IDigOut` type.
// Source file: `LedBlinker\hal\IDigOut.cs` (relative to C# solution).
// MD5 hash of source file: 6ec4d90a66830d9aa2801799e611776c.


#include "hal_IDigOut.h"


/// <summary>
/// Sets the state of the digital output.
/// </summary>
/// <param name="state"></param>
void hal_IDigOut_set_output_state(hal_IDigOut * self, bool state)
{
    self->obj_vtable->set_output_state(self->obj, state);
}


bool hal_IDigOut_get_output_state(hal_IDigOut * self)
{
    return self->obj_vtable->get_output_state(self->obj);
}


void hal_IDigOut_toggle(hal_IDigOut * self)
{
    self->obj_vtable->toggle(self->obj);
}

