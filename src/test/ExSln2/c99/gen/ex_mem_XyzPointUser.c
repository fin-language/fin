// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `ex_mem.XyzPointUser` type.
// Source file: `LedBlinker/mem_test/XyzPointUser.cs` (relative to C# solution).


#include "ex_mem_XyzPointUser.h"
#include <string.h>



ex_mem_XyzPointUser * ex_mem_XyzPointUser_ctor(ex_mem_XyzPointUser * self)
{
    memset(self, 0, sizeof(*self));
    (void)ex_mem_XyzPointI8_ctor(&self->p1, 55);
    return self;
}
