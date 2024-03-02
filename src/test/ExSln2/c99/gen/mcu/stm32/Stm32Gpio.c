// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `mcu.stm32.Stm32Gpio` type.
// Source file: `LedBlinker\mcu\stm32\Stm32Gpio.cs` (relative to C# solution).


#include "mcu/stm32/Stm32Gpio.h"
#include <string.h>



void mcu_stm32_Stm32Gpio_ctor(mcu_stm32_Stm32Gpio * self, GPIO_TypeDef * port, uint16_t pin)
{
    memset(self, 0, sizeof(*self));
    self->port = port;
    self->pin = pin;
}

// virtual table implementation for IGpio. Note that this is extern'd.
const hal_IGpio_vtable hal_IGpio_vtable_imp = {
    .read_state = (bool (*)(void * self))mcu_stm32_Stm32Gpio_read_state,
    .set_state = (void (*)(void * self, bool state))mcu_stm32_Stm32Gpio_set_state,
    .toggle = (void (*)(void * self))mcu_stm32_Stm32Gpio_toggle,
    .enable_pullup = (bool (*)(void * self))mcu_stm32_Stm32Gpio_enable_pullup,
    .enable_pulldown = (bool (*)(void * self))mcu_stm32_Stm32Gpio_enable_pulldown,
};

