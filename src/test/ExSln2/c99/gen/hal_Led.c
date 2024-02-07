// finlang generated file for c# hal.Led type

#include "hal_Led.h"
#include <string.h>



void hal_Led_ctor(hal_Led * self, hal_Gpio * gpio)
{
    memset(self, 0, sizeof(*self));
    self->_gpio = gpio;
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
