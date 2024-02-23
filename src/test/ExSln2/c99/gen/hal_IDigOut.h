// finlang generated file for c# hal.IDigOut type
#pragma once

#include <stdbool.h>


typedef struct hal_IDigOut hal_IDigOut;
typedef struct hal_IDigOut_vtable hal_IDigOut_vtable;

struct hal_IDigOut
{
    hal_IDigOut_vtable const * /*const*/ vtable;
    void * /*const*/ self;
};

struct hal_IDigOut_vtable
{
    /// <summary>
    /// Sets the state of the digital output.
    /// </summary>
    /// <param name="state"></param>
    void (*set_state)(void * self, bool state);
    void (*toggle)(void * self);
};

/// <summary>
/// Sets the state of the digital output.
/// </summary>
/// <param name="state"></param>
void hal_IDigOut_set_state(hal_IDigOut * self, bool state);

void hal_IDigOut_toggle(hal_IDigOut * self);


