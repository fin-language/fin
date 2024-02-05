// finlang generated file for c# hal.Led class
#pragma once

#include "hal_Gpio.h"


typedef struct hal_Led hal_Led;  // forward declaration
struct hal_Led
{
    hal_Gpio * _gpio;
};

void hal_Led_ctor(hal_Led * self, hal_Gpio * gpio);
void hal_Led_toggle(hal_Led * self);
