// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang v0.3.0-alpha generated this file for C# `hal.IGpio` type.
// Source file: `LedBlinker\hal\IGpio.cs` (relative to C# solution).
// MD5 hash of source file: 17927aabc217259daaec75de5da46185.


#include "hal_IGpio.h"


/// <summary>
/// Reads the state of the digital input.
/// </summary>
/// <returns></returns>
bool hal_IGpio_read_input(hal_IGpio * self)
{
    return self->obj_vtable->read_input(self->obj);
}

/// <summary>
/// Sets the state of the digital output.
/// </summary>
/// <param name="state"></param>
void hal_IGpio_set_output_state(hal_IGpio * self, bool state)
{
    self->obj_vtable->set_output_state(self->obj, state);
}


bool hal_IGpio_get_output_state(hal_IGpio * self)
{
    return self->obj_vtable->get_output_state(self->obj);
}


void hal_IGpio_toggle(hal_IGpio * self)
{
    self->obj_vtable->toggle(self->obj);
}

bool hal_IGpio_enable_pullup(hal_IGpio * self)
{
    return self->obj_vtable->enable_pullup(self->obj);
}

bool hal_IGpio_disable_pullup(hal_IGpio * self)
{
    return self->obj_vtable->disable_pullup(self->obj);
}

void hal_IGpio_set_direction(hal_IGpio * self, hal_GpioDirection direction)
{
    self->obj_vtable->set_direction(self->obj, direction);
}

