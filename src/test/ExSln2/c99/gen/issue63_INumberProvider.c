// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `issue63.INumberProvider` type.
// Source file: `LedBlinker/test_stuff/InterfaceSelectionForTranspile.cs` (relative to C# solution).


#include "issue63_INumberProvider.h"


uint8_t issue63_INumberProvider_GetNumber(issue63_INumberProvider * self)
{
    return self->obj_vtable->GetNumber(self->obj);
}

