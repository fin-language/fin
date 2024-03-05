// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang v0.3.0-alpha generated this file for C# `mcu.avr8.Avr8Gpio` type.
// Source file: `LedBlinker/mcu/avr8/Avr8Gpio.cs` (relative to C# solution).
// MD5 hash of source file: 3f27477320d365de87489a2eedb31727.

#pragma once

#include "mcu_avr8_Avr8Gpio_port_implementation.h" // You need to provide this
#include <stdint.h>
#include "hal_GpioDirection.h"
#include <stdbool.h>
#include "hal_IGpio.h"



/// <summary>
/// We want a .c/h file to be generated for this class and have it setup the vtable.
/// </summary>
typedef struct mcu_avr8_Avr8Gpio mcu_avr8_Avr8Gpio;
struct mcu_avr8_Avr8Gpio
{
    /// <summary>
    /// Example: PORTB. 
    /// Note: DDRx = PORTx - 1. PINx = PORTx - 2. See Section 30 of data for Register Summary.
    /// </summary>
    volatile uint8_t * _port;

    uint8_t _pin_mask;
};


void mcu_avr8_Avr8Gpio_ctor(mcu_avr8_Avr8Gpio * self, volatile uint8_t * port, uint8_t pin);

void PRIVATE_mcu_avr8_Avr8Gpio_ThrowIfAlreadyInstantiated(char const * port, uint8_t pin);

// FFI function. User code must provide the implementation
void mcu_avr8_Avr8Gpio_set_direction(mcu_avr8_Avr8Gpio * self, hal_GpioDirection direction);

// FFI function. User code must provide the implementation
bool mcu_avr8_Avr8Gpio_enable_pullup(mcu_avr8_Avr8Gpio * self);

// FFI function. User code must provide the implementation
bool mcu_avr8_Avr8Gpio_disable_pullup(mcu_avr8_Avr8Gpio * self);

bool mcu_avr8_Avr8Gpio_get_output_state(mcu_avr8_Avr8Gpio * self);

// FFI function. User code must provide the implementation
bool mcu_avr8_Avr8Gpio_read_input(mcu_avr8_Avr8Gpio * self);

// FFI function. User code must provide the implementation
void mcu_avr8_Avr8Gpio_set_output_state(mcu_avr8_Avr8Gpio * self, bool state);

void mcu_avr8_Avr8Gpio_toggle(mcu_avr8_Avr8Gpio * self);

// vtable is extern to allow const initializations
extern const hal_IGpio_vtable hal_IGpio_vtable_imp;

// Up conversion from mcu_avr8_Avr8Gpio to hal_IGpio interface
#define M_mcu_avr8_Avr8Gpio__to__hal_IGpio(self_arg)    (hal_IGpio){ .obj = self_arg, .obj_vtable = (const hal_IGpio_vtable*)(&hal_IGpio_vtable_imp.read_input) }

// Up conversion from mcu_avr8_Avr8Gpio to hal_IDigInOut interface
#define M_mcu_avr8_Avr8Gpio__to__hal_IDigInOut(self_arg)    (hal_IDigInOut){ .obj = self_arg, .obj_vtable = (const hal_IDigInOut_vtable*)(&hal_IGpio_vtable_imp.read_input) }

// Up conversion from mcu_avr8_Avr8Gpio to hal_IDigIn interface
#define M_mcu_avr8_Avr8Gpio__to__hal_IDigIn(self_arg)    (hal_IDigIn){ .obj = self_arg, .obj_vtable = (const hal_IDigIn_vtable*)(&hal_IGpio_vtable_imp.read_input) }

// Up conversion from mcu_avr8_Avr8Gpio to hal_IDigOut interface
#define M_mcu_avr8_Avr8Gpio__to__hal_IDigOut(self_arg)    (hal_IDigOut){ .obj = self_arg, .obj_vtable = (const hal_IDigOut_vtable*)(&hal_IGpio_vtable_imp.set_output_state) }
