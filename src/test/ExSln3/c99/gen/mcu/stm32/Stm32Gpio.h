// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang v0.2.6-alpha generated this file for C# `mcu.stm32.Stm32Gpio` type.
// Source file: `LedBlinker\mcu\stm32\Stm32Gpio.cs` (relative to C# solution).
// MD5 hash of source file: 61a1d010c3d2fb08cbd6f867e7621914.

#pragma once

#include "mcu_stm32_Stm32Gpio_port_implementation.h" // You need to provide this
#include <stdbool.h>
#include "hal_IGpio.h"


// Class is a Foreign Function Interface. No struct generated.
bool mcu_stm32_Stm32Gpio_enable_pulldown(mcu_stm32_Stm32Gpio * self);

bool mcu_stm32_Stm32Gpio_enable_pullup(mcu_stm32_Stm32Gpio * self);

bool mcu_stm32_Stm32Gpio_get_output_state(mcu_stm32_Stm32Gpio * self);

bool mcu_stm32_Stm32Gpio_read_input(mcu_stm32_Stm32Gpio * self);

void mcu_stm32_Stm32Gpio_set_output_state(mcu_stm32_Stm32Gpio * self, bool state);

void mcu_stm32_Stm32Gpio_toggle(mcu_stm32_Stm32Gpio * self);

// vtable is extern to allow const initializations
extern const hal_IGpio_vtable hal_IGpio_vtable_imp;

// Up conversion from mcu_stm32_Stm32Gpio to hal_IGpio interface
#define M_mcu_stm32_Stm32Gpio__to__hal_IGpio(self_arg)    (hal_IGpio){ .obj = self_arg, .obj_vtable = (const hal_IGpio_vtable*)(&hal_IGpio_vtable_imp.read_input) }

// Up conversion from mcu_stm32_Stm32Gpio to hal_IDigInOut interface
#define M_mcu_stm32_Stm32Gpio__to__hal_IDigInOut(self_arg)    (hal_IDigInOut){ .obj = self_arg, .obj_vtable = (const hal_IDigInOut_vtable*)(&hal_IGpio_vtable_imp.read_input) }

// Up conversion from mcu_stm32_Stm32Gpio to hal_IDigIn interface
#define M_mcu_stm32_Stm32Gpio__to__hal_IDigIn(self_arg)    (hal_IDigIn){ .obj = self_arg, .obj_vtable = (const hal_IDigIn_vtable*)(&hal_IGpio_vtable_imp.read_input) }

// Up conversion from mcu_stm32_Stm32Gpio to hal_IDigOut interface
#define M_mcu_stm32_Stm32Gpio__to__hal_IDigOut(self_arg)    (hal_IDigOut){ .obj = self_arg, .obj_vtable = (const hal_IDigOut_vtable*)(&hal_IGpio_vtable_imp.set_output_state) }
