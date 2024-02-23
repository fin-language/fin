// finlang generated file for c# hal.IDigIn type
#pragma once

#include <stdbool.h>


typedef struct hal_IDigIn hal_IDigIn;
typedef struct hal_IDigIn_vtable hal_IDigIn_vtable;

struct hal_IDigIn
{
    hal_IDigIn_vtable const * /*const*/ vtable;
    void * /*const*/ self;
};

struct hal_IDigIn_vtable
{
    /// <summary>
    /// Reads the state of the digital input.
    /// </summary>
    /// <returns></returns>
    bool (*read_state)(void * self);
};

/// <summary>
/// Reads the state of the digital input.
/// </summary>
/// <returns></returns>
bool hal_IDigIn_read_state(hal_IDigIn * self);


