// finlang generated file for c# app.Counter class
#pragma once

#include <stdint.h>


typedef struct app_Counter app_Counter;  // forward declaration
struct app_Counter
{
    uint32_t _count;
};

void app_Counter_increment(app_Counter * self);
void app_Counter_ctor(app_Counter * self);
