// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `issue63.InterfaceSelectionForTranspile` type.
// Source file: `LedBlinker/test_stuff/InterfaceSelectionForTranspile.cs` (relative to C# solution).

#pragma once

#include <stdint.h>
#include "issue63_INumberProvider.h"



typedef struct issue63_InterfaceSelectionForTranspile issue63_InterfaceSelectionForTranspile;
struct issue63_InterfaceSelectionForTranspile
{
    uint8_t my_number ;
};


uint8_t issue63_InterfaceSelectionForTranspile_GetNumber(issue63_InterfaceSelectionForTranspile * self);

// vtable is extern to allow const initializations
extern const issue63_INumberProvider_vtable issue63_INumberProvider_vtable_imp;

// Up conversion from issue63_InterfaceSelectionForTranspile to issue63_INumberProvider interface
#define M_issue63_InterfaceSelectionForTranspile__to__issue63_INumberProvider(self_arg)    (issue63_INumberProvider){ .obj = self_arg, .obj_vtable = (const issue63_INumberProvider_vtable*)(&issue63_INumberProvider_vtable_imp.GetNumber) }
