// finlang generated file for c# hal.IDigIn type

#include "hal_IDigIn.h"
#include <stddef.h>
#include <assert.h>


/// <summary>
/// Reads the state of the digital input.
/// </summary>
/// <returns></returns>
bool hal_IDigIn_read_state(hal_IDigIn * self)
{
    return self->vtable->read_state(self);
}

