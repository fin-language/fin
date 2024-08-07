// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `hal.IDigOut` type.
// Source file: `LedBlinker/hal/IDigOut.cs` (relative to C# solution).
// MD5 hash of source file: 6ec4d90a66830d9aa2801799e611776c.

#pragma once

#include <stdbool.h>
#include <stddef.h>
#include <assert.h>


typedef struct hal_IDigOut hal_IDigOut;
typedef struct hal_IDigOut_vtable hal_IDigOut_vtable;

struct hal_IDigOut
{
    /** Pointer to implementing object's vtable for this interface */
    hal_IDigOut_vtable const * const obj_vtable;

    /** The actual object that implements this interface */
    void * const obj;
};

struct hal_IDigOut_vtable
{
    /// <summary>
    /// Sets the state of the digital output.
    /// </summary>
    /// <param name="state"></param>
    void (*set_output_state)(void * self, bool state);

    bool (*get_output_state)(void * self);

    void (*toggle)(void * self);
};

/// <summary>
/// Sets the state of the digital output.
/// </summary>
/// <param name="state"></param>
void hal_IDigOut_set_output_state(hal_IDigOut * self, bool state);


bool hal_IDigOut_get_output_state(hal_IDigOut * self);


void hal_IDigOut_toggle(hal_IDigOut * self);

