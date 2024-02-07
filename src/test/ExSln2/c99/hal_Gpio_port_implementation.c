#include "hal_Gpio.h"
#include <stdio.h>

hal_GpioPinState fake_gpio_state = hal_GpioPinState_Low;

hal_GpioPinState hal_Gpio_read(hal_Gpio * self)
{
    return fake_gpio_state;
}

void hal_Gpio_write(hal_Gpio * self, hal_GpioPinState state)
{
    fake_gpio_state = state;
    printf("hal_Gpio_write(%d)\n", state);
}