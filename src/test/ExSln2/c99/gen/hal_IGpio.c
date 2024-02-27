// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang v0.2.6-alpha generated this file for C# `hal.IGpio` type.
// Source file: `LedBlinker\hal\IGpio.cs` (relative to C# solution).
// MD5 hash of source file: 48fe59467cef629fcd44b7ebbe4e16d2.
// Generated 2024-02-27 12:36:55.


#include "hal_IGpio.h"


/// <summary>
/// Reads the state of the digital input.
/// </summary>
/// <returns></returns>
bool hal_IGpio_read_state(hal_IGpio * self)
{
    return self->obj_vtable->read_state(self->obj);
}

/// <summary>
/// Sets the state of the digital output.
/// </summary>
/// <param name="state"></param>
void hal_IGpio_set_state(hal_IGpio * self, bool state)
{
    self->obj_vtable->set_state(self->obj, state);
}

void hal_IGpio_toggle(hal_IGpio * self)
{
    self->obj_vtable->toggle(self->obj);
}

bool hal_IGpio_enable_pullup(hal_IGpio * self)
{
    return self->obj_vtable->enable_pullup(self->obj);
}

bool hal_IGpio_enable_pulldown(hal_IGpio * self)
{
    return self->obj_vtable->enable_pulldown(self->obj);
}

