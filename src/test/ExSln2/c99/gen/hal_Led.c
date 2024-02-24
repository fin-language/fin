// finlang generated file for c# hal.Led type

#include "hal_Led.h"
#include "hal_IDigOut.h"
#include <string.h>



void hal_Led_ctor(hal_Led * self, hal_IDigInOut * dig_out)
{
    memset(self, 0, sizeof(*self));
    self->_dig_io = dig_out;
}

void hal_Led_toggle_twice(hal_Led * self)
{
    // to test that we can transpile a method call that doesn't have a parameter
    hal_Led_toggle(self);
    hal_Led_toggle(self); // to test `this.` method invocations
}

void hal_Led_toggle_twice_static(hal_Led * led)
{
    hal_Led_toggle(led);
    hal_Led_toggle(led);
}

// Will toggle the state of the LED
void hal_Led_toggle(hal_Led * self)
{
    hal_IDigInOut_toggle(self->_dig_io);
}
