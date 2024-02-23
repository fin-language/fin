// finlang generated file for c# hal.GpioDigInOut type

#include "hal_GpioDigInOut.h"
#include <string.h>



void hal_GpioDigInOut_ctor(hal_GpioDigInOut * self, hal_Gpio * gpio)
{
    memset(self, 0, sizeof(*self));
    self->_gpio = gpio;
}

bool hal_GpioDigInOut_read_state(hal_GpioDigInOut * self)
{
    return hal_Gpio_read(self->_gpio) == hal_GpioPinState_High;
}

void hal_GpioDigInOut_set_state(hal_GpioDigInOut * self, bool state)
{
    hal_Gpio_write(self->_gpio, state ? hal_GpioPinState_High : hal_GpioPinState_Low);
}

void hal_GpioDigInOut_toggle(hal_GpioDigInOut * self)
{
    hal_GpioPinState next_state = hal_Gpio_read(self->_gpio) == hal_GpioPinState_High ? hal_GpioPinState_Low : hal_GpioPinState_High;
    hal_Gpio_write(self->_gpio, next_state);
}
