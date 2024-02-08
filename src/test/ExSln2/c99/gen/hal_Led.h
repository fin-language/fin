// finlang generated file for c# hal.Led type
#pragma once

#include "hal_Gpio.h"
#include <stdint.h>
#include "hal_Led.h"



typedef struct hal_Led hal_Led;
struct hal_Led
{
    hal_Gpio * _gpio;
    uint8_t my_public_var;
};


void hal_Led_ctor(hal_Led * self, hal_Gpio * gpio);

void hal_Led_toggle_twice(hal_Led * self);

void hal_Led_toggle_twice_static(hal_Led * led);

// Will toggle the state of the LED
void hal_Led_toggle(hal_Led * self);
