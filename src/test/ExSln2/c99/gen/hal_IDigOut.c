// finlang generated file for c# hal.IDigOut type

#include "hal_IDigOut.h"


/// <summary>
/// Sets the state of the digital output.
/// </summary>
/// <param name="state"></param>
void hal_IDigOut_set_state(hal_IDigOut * self, bool state)
{
    self->vtable->set_state(self, state);
}

void hal_IDigOut_toggle(hal_IDigOut * self)
{
    self->vtable->toggle(self);
}

