// finlang generated file for c# hal.Gpio type
#pragma once

#include "hal_Gpio_port_implementation.h" // You need to provide this
#include "hal_GpioPinState.h"


// Class is a Foreign Function Interface. No struct generated.

hal_GpioPinState hal_Gpio_read(hal_Gpio * self);

void hal_Gpio_write(hal_Gpio * self, hal_GpioPinState state);
