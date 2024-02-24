// finlang generated file for c# hal.GpioDigInOut type
#pragma once

#include "hal_Gpio.h"
#include <stdbool.h>
#include "hal_IDigInOut.h"
#include "hal_IDigIn.h"
#include "hal_IDigOut.h"



typedef struct hal_GpioDigInOut hal_GpioDigInOut;
struct hal_GpioDigInOut
{
    hal_Gpio * _gpio;
};


void hal_GpioDigInOut_ctor(hal_GpioDigInOut * self, hal_Gpio * gpio);

bool hal_GpioDigInOut_read_state(hal_GpioDigInOut * self);

void hal_GpioDigInOut_set_state(hal_GpioDigInOut * self, bool state);

void hal_GpioDigInOut_toggle(hal_GpioDigInOut * self);


const hal_IDigInOut_vtable* hal_GpioDigInOut__get__hal_IDigInOut_vtable(void);

#define M_hal_GpioDigInOut__to__hal_IDigInOut(self_arg)    &(hal_IDigInOut){ .vtable = hal_GpioDigInOut__get__hal_IDigInOut_vtable(), .self = self_arg }



// Up conversion from hal_GpioDigInOut to hal_IDigInOut interface
hal_IDigInOut hal_GpioDigInOut__to__hal_IDigInOut(hal_GpioDigInOut * self);


// Up conversion from hal_GpioDigInOut to hal_IDigIn interface
hal_IDigIn hal_GpioDigInOut__to__hal_IDigIn(hal_GpioDigInOut * self);


// Up conversion from hal_GpioDigInOut to hal_IDigOut interface
hal_IDigOut hal_GpioDigInOut__to__hal_IDigOut(hal_GpioDigInOut * self);

