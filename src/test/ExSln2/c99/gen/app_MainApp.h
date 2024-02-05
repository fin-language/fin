// finlang generated file for c# app.MainApp class
#pragma once

#include <stdint.h>
#include "hal_Led.h"


typedef struct app_MainApp app_MainApp;  // forward declaration
struct app_MainApp
{
    uint32_t _toggle_at_ms;
    hal_Led * _redLed;
};

void app_MainApp_ctor(app_MainApp * self, hal_Led * redLed);
void app_MainApp_step(app_MainApp * self, uint32_t ms_time);
