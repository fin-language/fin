// finlang generated file for c# hal.IDigInOut type
#pragma once

#include <stdbool.h>
#include "hal_IDigIn.h"
#include "hal_IDigOut.h"


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
hal_IDigIn hal_IDigInOut__to__hal_IDigIn(hal_IDigInOut * self);

// Up conversion from hal_IDigInOut interface to hal_IDigOut interface
hal_IDigOut hal_IDigInOut__to__hal_IDigOut(hal_IDigInOut * self);

