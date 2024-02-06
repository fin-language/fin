// finlang generated file for c# app.Counter class
#pragma once

#include <stdint.h>


typedef struct app_Counter app_Counter;  // forward declaration
struct app_Counter
{
    finlang_c_array * _counts;
    uint32_t _count;
};

void app_Counter_ctor(app_Counter * self, finlang_c_array * counts);
void app_Counter_increment(app_Counter * self);
