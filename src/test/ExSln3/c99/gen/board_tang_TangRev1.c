// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `board.tang.TangRev1` type.
// Source file: `LedBlinker/board/tang/TangRev1.cs` (relative to C# solution).
// MD5 hash of source file: 20909cee92d47d3a8a9d99776259beff.


#include "board_tang_TangRev1.h"
#include <string.h>



void board_tang_TangRev1_ctor(board_tang_TangRev1 * self)
{
    memset(self, 0, sizeof(*self));
    mcu_avr8_Avr8Gpio_ctor(&self->_start_button, &PORTC, 0);
    mcu_avr8_Avr8Gpio_ctor(&self->_trap_button, &PORTC, 1);
    mcu_avr8_Avr8Gpio_ctor(&self->_main_led, &PORTB, 5);
    mcu_avr8_Avr8Gpio_enable_pullup(&self->_start_button);
    mcu_avr8_Avr8Gpio_set_direction(&self->_start_button, hal_GpioDirection_Input);
    mcu_avr8_Avr8Gpio_enable_pullup(&self->_trap_button);
    mcu_avr8_Avr8Gpio_set_direction(&self->_trap_button, hal_GpioDirection_Input);
    mcu_avr8_Avr8Gpio_set_direction(&self->_main_led, hal_GpioDirection_Output);
}

hal_IDigOut * board_tang_TangRev1_get_main_led(board_tang_TangRev1 * self)
{
    return self->_main_led;
}

hal_IDigIn * board_tang_TangRev1_get_start_button(board_tang_TangRev1 * self)
{
    return self->_start_button;
}

hal_IDigIn * board_tang_TangRev1_get_trap_button(board_tang_TangRev1 * self)
{
    return self->_trap_button;
}

void board_tang_TangRev1_step(board_tang_TangRev1 * self)
{
    (void)(self);
}

// virtual table implementation for IGeneralBoard. Note that this is extern'd.
const board_IGeneralBoard_vtable board_tang_TangRev1__board_IGeneralBoard_vtable_imp = {
    .get_trap_button = (hal_IDigIn * (*)(void * self))board_tang_TangRev1_get_trap_button,
    .get_start_button = (hal_IDigIn * (*)(void * self))board_tang_TangRev1_get_start_button,
    .get_main_led = (hal_IDigOut * (*)(void * self))board_tang_TangRev1_get_main_led,
    .get_time_ms = (uint32_t (*)(void * self))board_tang_TangRev1_get_time_ms,
    .step = (void (*)(void * self))board_tang_TangRev1_step,
};

