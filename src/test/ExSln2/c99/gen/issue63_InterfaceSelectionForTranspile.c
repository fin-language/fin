// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `issue63.InterfaceSelectionForTranspile` type.
// Source file: `LedBlinker/test_stuff/InterfaceSelectionForTranspile.cs` (relative to C# solution).


#include "issue63_InterfaceSelectionForTranspile.h"
#include <string.h>


issue63_InterfaceSelectionForTranspile * issue63_InterfaceSelectionForTranspile_ctor(issue63_InterfaceSelectionForTranspile * self)
{
    memset(self, 0, sizeof(*self));
    self->my_number = 42;
    return self;
}

uint8_t issue63_InterfaceSelectionForTranspile_GetNumber(issue63_InterfaceSelectionForTranspile * self)
{
    return self->my_number;
}

// virtual table implementation for INumberProvider. Note that this is extern'd.
const issue63_INumberProvider_vtable issue63_InterfaceSelectionForTranspile__issue63_INumberProvider_vtable_imp = {
    .GetNumber = (uint8_t (*)(void * self))issue63_InterfaceSelectionForTranspile_GetNumber,
};

