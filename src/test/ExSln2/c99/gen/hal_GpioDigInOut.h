// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang v0.2.6-alpha generated this file for C# `hal.GpioDigInOut` type.
// Source file: `LedBlinker\hal\GpioDigInOut.cs` (relative to C# solution).
// MD5 hash of source file: b93faf7333e6c94975c3fc35d6fe7fb0.
// Generated 2024-02-27 12:36:55.

#pragma once

#include "hal_Gpio.h"
#include <stdbool.h>
#include "hal_IDigInOut.h"



typedef struct hal_GpioDigInOut hal_GpioDigInOut;
struct hal_GpioDigInOut
{
    hal_Gpio * _gpio;
};


void hal_GpioDigInOut_ctor(hal_GpioDigInOut * self, hal_Gpio * gpio);

bool hal_GpioDigInOut_read_state(hal_GpioDigInOut * self);

void hal_GpioDigInOut_set_state(hal_GpioDigInOut * self, bool state);

void hal_GpioDigInOut_toggle(hal_GpioDigInOut * self);

// vtable is extern to allow const initializations
extern const hal_IDigInOut_vtable hal_IDigInOut_vtable_imp;

// Up conversion from hal_GpioDigInOut to hal_IDigInOut interface
#define M_hal_GpioDigInOut__to__hal_IDigInOut(self_arg)    (hal_IDigInOut){ .obj = self_arg, .obj_vtable = (const hal_IDigInOut_vtable*)(&hal_IDigInOut_vtable_imp.read_state) }

// Up conversion from hal_GpioDigInOut to hal_IDigIn interface
#define M_hal_GpioDigInOut__to__hal_IDigIn(self_arg)    (hal_IDigIn){ .obj = self_arg, .obj_vtable = (const hal_IDigIn_vtable*)(&hal_IDigInOut_vtable_imp.read_state) }

// Up conversion from hal_GpioDigInOut to hal_IDigOut interface
#define M_hal_GpioDigInOut__to__hal_IDigOut(self_arg)    (hal_IDigOut){ .obj = self_arg, .obj_vtable = (const hal_IDigOut_vtable*)(&hal_IDigInOut_vtable_imp.set_state) }
