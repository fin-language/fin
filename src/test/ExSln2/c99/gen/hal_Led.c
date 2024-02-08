// finlang generated file for c# hal.Led type

#include "hal_Led.h"
#include <string.h>



void hal_Led_ctor(hal_Led * self, hal_Gpio * gpio)
{
    memset(self, 0, sizeof(*self));
    self->_gpio = gpio;
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
    if (hal_Gpio_read(self->_gpio) == hal_GpioPinState_High)
    {
        hal_Gpio_write(self->_gpio, hal_GpioPinState_Low); // Turn off
    }
    else
    {
        hal_Gpio_write(self->_gpio, hal_GpioPinState_High); // Turn on
    }
}
