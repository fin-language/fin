// finlang generated file for c# mcu.avr8.Avr8Gpio type

#include "mcu/avr8/Avr8Gpio.h"



// virtual table implementation for IGpio. Note that this is extern'd.
const hal_IGpio_vtable hal_IGpio_vtable_imp = {
    .read_state = (bool (*)(void * self))mcu_avr8_Avr8Gpio_read_state,
    .set_state = (void (*)(void * self, bool state))mcu_avr8_Avr8Gpio_set_state,
    .toggle = (void (*)(void * self))mcu_avr8_Avr8Gpio_toggle,
    .enable_pullup = (bool (*)(void * self))mcu_avr8_Avr8Gpio_enable_pullup,
    .enable_pulldown = (bool (*)(void * self))mcu_avr8_Avr8Gpio_enable_pulldown,
};

