// finlang generated file for c# hal.IDigInOut type

#include "hal_IDigInOut.h"
#include <stddef.h>
#include <assert.h>


/// <summary>
/// Reads the state of the digital input.
/// </summary>
/// <returns></returns>
bool hal_IDigInOut_read_state(hal_IDigInOut * self)
{
    return self->vtable->read_state(self);
}

/// <summary>
/// Sets the state of the digital output.
/// </summary>
/// <param name="state"></param>
void hal_IDigInOut_set_state(hal_IDigInOut * self, bool state)
{
    self->vtable->set_state(self, state);
}

void hal_IDigInOut_toggle(hal_IDigInOut * self)
{
    self->vtable->toggle(self);
}

// Up conversion from hal_IDigInOut interface to hal_IDigIn interface
hal_IDigIn hal_IDigInOut__to__hal_IDigIn(hal_IDigInOut * self)
{
    hal_IDigIn result;

    // assert that vtable layouts are compatible
    static_assert(offsetof(hal_IDigIn_vtable, read_state) == 0, "Unexpected vtable function start");
    static_assert(offsetof(hal_IDigIn_vtable, read_state) == offsetof(hal_IDigInOut_vtable, read_state) - offsetof(hal_IDigInOut_vtable, read_state), "Incompatible vtable layout");

    // adjust vtable pointer
    result.vtable = (hal_IDigIn_vtable*)self->vtable + offsetof(hal_IDigInOut_vtable, read_state);
    result.self = self->self;
    return result;
}

// Up conversion from hal_IDigInOut interface to hal_IDigOut interface
hal_IDigOut hal_IDigInOut__to__hal_IDigOut(hal_IDigInOut * self)
{
    hal_IDigOut result;

    // assert that vtable layouts are compatible
    static_assert(offsetof(hal_IDigOut_vtable, set_state) == 0, "Unexpected vtable function start");
    static_assert(offsetof(hal_IDigOut_vtable, set_state) == offsetof(hal_IDigInOut_vtable, set_state) - offsetof(hal_IDigInOut_vtable, set_state), "Incompatible vtable layout");
    static_assert(offsetof(hal_IDigOut_vtable, toggle) == offsetof(hal_IDigInOut_vtable, toggle) - offsetof(hal_IDigInOut_vtable, set_state), "Incompatible vtable layout");

    // adjust vtable pointer
    result.vtable = (hal_IDigOut_vtable*)self->vtable + offsetof(hal_IDigInOut_vtable, set_state);
    result.self = self->self;
    return result;
}

