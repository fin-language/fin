// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `issue63.INumberProvider` type.
// Source file: `LedBlinker/test_stuff/InterfaceSelectionForTranspile.cs` (relative to C# solution).

#pragma once

#include <stdint.h>
#include <stddef.h>
#include <assert.h>


typedef struct issue63_INumberProvider issue63_INumberProvider;
typedef struct issue63_INumberProvider_vtable issue63_INumberProvider_vtable;

struct issue63_INumberProvider
{
    /** Pointer to implementing object's vtable for this interface */
    issue63_INumberProvider_vtable const * const obj_vtable;

    /** The actual object that implements this interface */
    void * const obj;
};

struct issue63_INumberProvider_vtable
{
    uint8_t (*GetNumber)(void * self);
};

uint8_t issue63_INumberProvider_GetNumber(issue63_INumberProvider * self);
