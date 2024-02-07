// finlang generated file for c# app.MainApp class
#pragma once

#include <stdint.h>
#include <stdint.h>
#include "hal_Led.h"



typedef struct app_MainApp app_MainApp;
struct app_MainApp
{
    uint16_t period_ms; // keep without underscore so that `this.` is required in constructor
    uint32_t _toggle_at_ms;
    hal_Led * _redLed;
};


void app_MainApp_ctor(app_MainApp * self, hal_Led * redLed, uint16_t period_ms);

/// <summary>
/// This is the main loop of the application.
/// </summary>
void app_MainApp_step(app_MainApp * self, uint32_t ms_time);

uint32_t app_MainApp_transpilation_test_stuff(app_MainApp * self);
