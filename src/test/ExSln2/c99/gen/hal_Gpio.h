// finlang generated file for c# hal.Gpio class
#pragma once

#include "hal_Gpio_port_implementation.h" // You need to provide this
#include <stdbool.h>


// Class is a Foreign Function Interface. No struct generated.

    bool hal_Gpio_read(hal_Gpio * self);

    void hal_Gpio_write(hal_Gpio * self, bool state);
