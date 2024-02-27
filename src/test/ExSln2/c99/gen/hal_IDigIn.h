// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang v0.2.6-alpha generated this file for C# `hal.IDigIn` type.
// Source file: `LedBlinker\hal\IDigIn.cs` (relative to C# solution).
// MD5 hash of source file: a73eae7474d55f68b67a57193ed25dc4.
// Generated 2024-02-27 12:36:55.

#pragma once

#include <stdbool.h>
#include <stddef.h>
#include <assert.h>


typedef struct hal_IDigIn hal_IDigIn;
typedef struct hal_IDigIn_vtable hal_IDigIn_vtable;

struct hal_IDigIn
{
    /** Pointer to implementing object's vtable for this interface */
    hal_IDigIn_vtable const * const obj_vtable;

    /** The actual object that implements this interface */
    void * const obj;
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

