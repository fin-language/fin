// finlang generated file for c# mcu.stm32.Stm32Gpio type

#include "mcu/stm32/Stm32Gpio.h"



// virtual table implementation for IGpio. Note that this is extern'd.
const hal_IGpio_vtable hal_IGpio_vtable_imp = {
    .read_state = (bool (*)(void * self))mcu_stm32_Stm32Gpio_read_state,
    .set_state = (void (*)(void * self, bool state))mcu_stm32_Stm32Gpio_set_state,
    .toggle = (void (*)(void * self))mcu_stm32_Stm32Gpio_toggle,
    .enable_pullup = (bool (*)(void * self))mcu_stm32_Stm32Gpio_enable_pullup,
    .enable_pulldown = (bool (*)(void * self))mcu_stm32_Stm32Gpio_enable_pulldown,
};

