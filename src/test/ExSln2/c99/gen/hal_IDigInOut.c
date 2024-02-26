// finlang generated file for c# hal.IDigInOut type

#include "hal_IDigInOut.h"


/// <summary>
/// Reads the state of the digital input.
/// </summary>
/// <returns></returns>
bool hal_IDigInOut_read_state(hal_IDigInOut * self)
{
    return self->obj_vtable->read_state(self->obj);
}

/// <summary>
/// Sets the state of the digital output.
/// </summary>
/// <param name="state"></param>
void hal_IDigInOut_set_state(hal_IDigInOut * self, bool state)
{
    self->obj_vtable->set_state(self->obj, state);
}

void hal_IDigInOut_toggle(hal_IDigInOut * self)
{
    self->obj_vtable->toggle(self->obj);
}

