// finlang generated file for c# app.Main type
#pragma once

#include <stdint.h>
#include <stdint.h>
#include "hal_Led.h"



typedef struct app_Main app_Main;
struct app_Main
{
    uint16_t period_ms; // keep without underscore so that `this.` is required in constructor
    uint32_t _toggle_at_ms;
    hal_Led * _redLed;
};


void app_Main_ctor(app_Main * self, hal_Led * redLed, uint16_t period_ms);

/// <summary>
/// This is the main loop of the application.
/// </summary>
void app_Main_step(app_Main * self, uint32_t ms_time);

uint32_t app_Main_transpilation_test_stuff(app_Main * self);
