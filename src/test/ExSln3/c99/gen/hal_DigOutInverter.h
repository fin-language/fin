// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang v0.3.0-alpha generated this file for C# `hal.DigOutInverter` type.
// Source file: `LedBlinker\hal\DigOutInverter.cs` (relative to C# solution).
// MD5 hash of source file: cdc7ba80ab017a89529bb7084cabc5b1.

#pragma once

#include "hal_IDigOut.h"
#include <stdbool.h>



typedef struct hal_DigOutInverter hal_DigOutInverter;
struct hal_DigOutInverter
{
    hal_IDigOut * _dig_out;
};


void hal_DigOutInverter_ctor(hal_DigOutInverter * self, hal_IDigOut * dig_out);

void hal_DigOutInverter_set_output_state(hal_DigOutInverter * self, bool state);

void hal_DigOutInverter_toggle(hal_DigOutInverter * self);

bool hal_DigOutInverter_get_output_state(hal_DigOutInverter * self);

// vtable is extern to allow const initializations
extern const hal_IDigOut_vtable hal_IDigOut_vtable_imp;

// Up conversion from hal_DigOutInverter to hal_IDigOut interface
#define M_hal_DigOutInverter__to__hal_IDigOut(self_arg)    (hal_IDigOut){ .obj = self_arg, .obj_vtable = (const hal_IDigOut_vtable*)(&hal_IDigOut_vtable_imp.set_output_state) }
