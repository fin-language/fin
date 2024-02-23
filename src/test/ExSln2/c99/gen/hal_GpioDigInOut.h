// finlang generated file for c# hal.GpioDigInOut type
#pragma once

#include "hal_Gpio.h"
#include <stdbool.h>



typedef struct hal_GpioDigInOut hal_GpioDigInOut;
struct hal_GpioDigInOut
{
    hal_Gpio * _gpio;
};


void hal_GpioDigInOut_ctor(hal_GpioDigInOut * self, hal_Gpio * gpio);

bool hal_GpioDigInOut_read_state(hal_GpioDigInOut * self);

void hal_GpioDigInOut_set_state(hal_GpioDigInOut * self, bool state);

void hal_GpioDigInOut_toggle(hal_GpioDigInOut * self);
