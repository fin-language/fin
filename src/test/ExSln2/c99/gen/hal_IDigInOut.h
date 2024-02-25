// finlang generated file for c# hal.IDigInOut type
#pragma once

#include <stdbool.h>
#include "hal_IDigIn.h"
#include "hal_IDigOut.h"
#include <stddef.h>
#include <assert.h>


typedef struct hal_IDigInOut hal_IDigInOut;
typedef struct hal_IDigInOut_vtable hal_IDigInOut_vtable;

struct hal_IDigInOut
{
    hal_IDigInOut_vtable const * /*const*/ vtable;
    void * /*const*/ self;
};

struct hal_IDigInOut_vtable
{
    /// <summary>
    /// Reads the state of the digital input.
    /// </summary>
    /// <returns></returns>
    bool (*read_state)(void * self);
    /// <summary>
    /// Sets the state of the digital output.
    /// </summary>
    /// <param name="state"></param>
    void (*set_state)(void * self, bool state);
    void (*toggle)(void * self);
};

/// <summary>
/// Reads the state of the digital input.
/// </summary>
/// <returns></returns>
bool hal_IDigInOut_read_state(hal_IDigInOut * self);

/// <summary>
/// Sets the state of the digital output.
/// </summary>
/// <param name="state"></param>
void hal_IDigInOut_set_state(hal_IDigInOut * self, bool state);

void hal_IDigInOut_toggle(hal_IDigInOut * self);


// Up conversion from hal_IDigInOut interface to hal_IDigIn interface
// `self_arg` should be of type `hal_IDigInOut *`
#define M_hal_IDigInOut__to__hal_IDigIn(self_arg)    (hal_IDigIn){ .self = self_arg->self, .vtable = (const hal_IDigIn_vtable*)(&self_arg->vtable->read_state) }
// assert that vtable layouts are compatible
static_assert(offsetof(hal_IDigIn_vtable, read_state) == 0, "Unexpected vtable function start");
static_assert(offsetof(hal_IDigIn_vtable, read_state) == offsetof(hal_IDigInOut_vtable, read_state) - offsetof(hal_IDigInOut_vtable, read_state), "Incompatible vtable layout");

// Up conversion from hal_IDigInOut interface to hal_IDigOut interface
// `self_arg` should be of type `hal_IDigInOut *`
#define M_hal_IDigInOut__to__hal_IDigOut(self_arg)    (hal_IDigOut){ .self = self_arg->self, .vtable = (const hal_IDigOut_vtable*)(&self_arg->vtable->set_state) }
// assert that vtable layouts are compatible
static_assert(offsetof(hal_IDigOut_vtable, set_state) == 0, "Unexpected vtable function start");
static_assert(offsetof(hal_IDigOut_vtable, set_state) == offsetof(hal_IDigInOut_vtable, set_state) - offsetof(hal_IDigInOut_vtable, set_state), "Incompatible vtable layout");
static_assert(offsetof(hal_IDigOut_vtable, toggle) == offsetof(hal_IDigInOut_vtable, toggle) - offsetof(hal_IDigInOut_vtable, set_state), "Incompatible vtable layout");
