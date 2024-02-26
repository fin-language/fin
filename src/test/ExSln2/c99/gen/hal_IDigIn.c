// finlang generated file for c# hal.IDigIn type

#include "hal_IDigIn.h"


/// <summary>
/// Reads the state of the digital input.
/// </summary>
/// <returns></returns>
bool hal_IDigIn_read_state(hal_IDigIn * self)
{
    return self->obj_vtable->read_state(self->obj);
}

