// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `hal.ExTypeOverride` type.
// Source file: `LedBlinker/test_stuff/ExTypeOverride.cs` (relative to C# solution).


#include "hal_ExTypeOverride.h"
#include <string.h>



hal_ExTypeOverride * hal_ExTypeOverride_ctor(hal_ExTypeOverride * self, GPIO_TypeDef * port, uint16_t pin)
{
    memset(self, 0, sizeof(*self));
    self->port = port;
    self->pin = pin;
    return self;
}

void hal_ExTypeOverride_set_pin(hal_ExTypeOverride * self, uint16_t pin)
{
    self->pin = pin;
}

void hal_ExTypeOverride_set_pin_use_example(hal_ExTypeOverride * self)
{
    hal_ExTypeOverride_set_pin(self, GPIO_PIN_0);
}
