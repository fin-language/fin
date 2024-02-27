// finlang generated file for c# hal.IGpio type
#pragma once

#include <stdbool.h>
#include "hal_IDigInOut.h"
#include "hal_IDigIn.h"
#include "hal_IDigOut.h"
#include <stddef.h>
#include <assert.h>


typedef struct hal_IGpio hal_IGpio;
typedef struct hal_IGpio_vtable hal_IGpio_vtable;

struct hal_IGpio
{
    /** Pointer to implementing object's vtable for this interface */
    hal_IGpio_vtable const * const obj_vtable;

    /** The actual object that implements this interface */
    void * const obj;
};

struct hal_IGpio_vtable
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
    bool (*enable_pullup)(void * self);
    bool (*enable_pulldown)(void * self);
};

/// <summary>
/// Reads the state of the digital input.
/// </summary>
/// <returns></returns>
bool hal_IGpio_read_state(hal_IGpio * self);

/// <summary>
/// Sets the state of the digital output.
/// </summary>
/// <param name="state"></param>
void hal_IGpio_set_state(hal_IGpio * self, bool state);

void hal_IGpio_toggle(hal_IGpio * self);

bool hal_IGpio_enable_pullup(hal_IGpio * self);

bool hal_IGpio_enable_pulldown(hal_IGpio * self);


// Up conversion from hal_IGpio interface to hal_IDigInOut interface
// `self_arg` should be of type `hal_IGpio *`
#define M_hal_IGpio__to__hal_IDigInOut(self_arg)    (hal_IDigInOut){ .obj = self_arg->obj, .obj_vtable = (const hal_IDigInOut_vtable*)(&self_arg->obj_vtable->read_state) }
// assert that vtable layouts are compatible
static_assert(offsetof(hal_IDigInOut_vtable, read_state) == 0, "Unexpected vtable function start");
static_assert(offsetof(hal_IDigInOut_vtable, read_state) == offsetof(hal_IGpio_vtable, read_state) - offsetof(hal_IGpio_vtable, read_state), "Incompatible vtable layout");
static_assert(offsetof(hal_IDigInOut_vtable, set_state) == offsetof(hal_IGpio_vtable, set_state) - offsetof(hal_IGpio_vtable, read_state), "Incompatible vtable layout");
static_assert(offsetof(hal_IDigInOut_vtable, toggle) == offsetof(hal_IGpio_vtable, toggle) - offsetof(hal_IGpio_vtable, read_state), "Incompatible vtable layout");

// Up conversion from hal_IGpio interface to hal_IDigIn interface
// `self_arg` should be of type `hal_IGpio *`
#define M_hal_IGpio__to__hal_IDigIn(self_arg)    (hal_IDigIn){ .obj = self_arg->obj, .obj_vtable = (const hal_IDigIn_vtable*)(&self_arg->obj_vtable->read_state) }
// assert that vtable layouts are compatible
static_assert(offsetof(hal_IDigIn_vtable, read_state) == 0, "Unexpected vtable function start");
static_assert(offsetof(hal_IDigIn_vtable, read_state) == offsetof(hal_IGpio_vtable, read_state) - offsetof(hal_IGpio_vtable, read_state), "Incompatible vtable layout");

// Up conversion from hal_IGpio interface to hal_IDigOut interface
// `self_arg` should be of type `hal_IGpio *`
#define M_hal_IGpio__to__hal_IDigOut(self_arg)    (hal_IDigOut){ .obj = self_arg->obj, .obj_vtable = (const hal_IDigOut_vtable*)(&self_arg->obj_vtable->set_state) }
// assert that vtable layouts are compatible
static_assert(offsetof(hal_IDigOut_vtable, set_state) == 0, "Unexpected vtable function start");
static_assert(offsetof(hal_IDigOut_vtable, set_state) == offsetof(hal_IGpio_vtable, set_state) - offsetof(hal_IGpio_vtable, set_state), "Incompatible vtable layout");
static_assert(offsetof(hal_IDigOut_vtable, toggle) == offsetof(hal_IGpio_vtable, toggle) - offsetof(hal_IGpio_vtable, set_state), "Incompatible vtable layout");
