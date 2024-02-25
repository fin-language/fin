// finlang generated file for c# hal.GpioDigInOut type
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
#define M_hal_GpioDigInOut__to__hal_IDigInOut(self_arg)    (hal_IDigInOut){ .self = self_arg, .vtable = (hal_IDigInOut_vtable*)(&hal_IDigInOut_vtable_imp.read_state) }

// Up conversion from hal_GpioDigInOut to hal_IDigIn interface
#define M_hal_GpioDigInOut__to__hal_IDigIn(self_arg)    (hal_IDigIn){ .self = self_arg, .vtable = (hal_IDigIn_vtable*)(&hal_IDigInOut_vtable_imp.read_state) }

// Up conversion from hal_GpioDigInOut to hal_IDigOut interface
#define M_hal_GpioDigInOut__to__hal_IDigOut(self_arg)    (hal_IDigOut){ .self = self_arg, .vtable = (hal_IDigOut_vtable*)(&hal_IDigInOut_vtable_imp.set_state) }
