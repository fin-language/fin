// finlang generated file for c# hal.Led class

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
    if (hal_Gpio_read(self->_gpio))
    {
        hal_Gpio_write(self->_gpio, false); // Turn off
    }
    else
    {
        hal_Gpio_write(self->_gpio, true); // Turn on
    }
}
