#include "hal_Gpio.h"
#include <stdio.h>

bool fake_gpio_state = false;

bool hal_Gpio_read(hal_Gpio * self)
{
    return fake_gpio_state;
}

void hal_Gpio_write(hal_Gpio * self, bool state)
{
    fake_gpio_state = state;
    printf("hal_Gpio_write(%d)\n", state);
}