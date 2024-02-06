// finlang generated file for c# hal.Led class

#include "hal_Led.h"
#include <string.h>



    void hal_Led_ctor(hal_Led * self, hal_Gpio * gpio)
    {
        memset(self, 0, sizeof(*self));
        _gpio = gpio;
    }

    // Will toggle the state of the LED
    void hal_Led_toggle(hal_Led * self)
    {
        if (_gpio.hal_Gpio_read())
        {
            _gpio.hal_Gpio_write(false); // Turn off
        }
        else
        {
            _gpio.hal_Gpio_write(true); // Turn on
        }
    }
