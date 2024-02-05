// finlang generated file for c# hal.Led class

#include "hal_Led.h"



    Led(hal_Gpio gpio)
    {
        _gpio = gpio;
    }

    void hal_Led_toggle()
    {
        if (_gpio.hal_Gpio_read())
        {
            _gpio.hal_Gpio_write(false);
        }
        else
        {
            _gpio.hal_Gpio_write(true);
        }
    }
