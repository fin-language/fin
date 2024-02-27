// finlang generated file for c# mcu.avr8.Avr8Gpio type
#pragma once

#include "mcu_avr8_Avr8Gpio_port_implementation.h" // You need to provide this
#include <stdbool.h>
#include "hal_IGpio.h"


// Class is a Foreign Function Interface. No struct generated.
bool mcu_avr8_Avr8Gpio_enable_pulldown(mcu_avr8_Avr8Gpio * self);

bool mcu_avr8_Avr8Gpio_enable_pullup(mcu_avr8_Avr8Gpio * self);

bool mcu_avr8_Avr8Gpio_read_state(mcu_avr8_Avr8Gpio * self);

void mcu_avr8_Avr8Gpio_set_state(mcu_avr8_Avr8Gpio * self, bool state);

void mcu_avr8_Avr8Gpio_toggle(mcu_avr8_Avr8Gpio * self);

// vtable is extern to allow const initializations
extern const hal_IGpio_vtable hal_IGpio_vtable_imp;

// Up conversion from mcu_avr8_Avr8Gpio to hal_IGpio interface
#define M_mcu_avr8_Avr8Gpio__to__hal_IGpio(self_arg)    (hal_IGpio){ .obj = self_arg, .obj_vtable = (const hal_IGpio_vtable*)(&hal_IGpio_vtable_imp.read_state) }

// Up conversion from mcu_avr8_Avr8Gpio to hal_IDigInOut interface
#define M_mcu_avr8_Avr8Gpio__to__hal_IDigInOut(self_arg)    (hal_IDigInOut){ .obj = self_arg, .obj_vtable = (const hal_IDigInOut_vtable*)(&hal_IGpio_vtable_imp.read_state) }

// Up conversion from mcu_avr8_Avr8Gpio to hal_IDigIn interface
#define M_mcu_avr8_Avr8Gpio__to__hal_IDigIn(self_arg)    (hal_IDigIn){ .obj = self_arg, .obj_vtable = (const hal_IDigIn_vtable*)(&hal_IGpio_vtable_imp.read_state) }

// Up conversion from mcu_avr8_Avr8Gpio to hal_IDigOut interface
#define M_mcu_avr8_Avr8Gpio__to__hal_IDigOut(self_arg)    (hal_IDigOut){ .obj = self_arg, .obj_vtable = (const hal_IDigOut_vtable*)(&hal_IGpio_vtable_imp.set_state) }
