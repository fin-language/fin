// finlang generated file for c# hal.IDigOut type
#pragma once

#include <stdbool.h>


typedef struct hal_IDigOut hal_IDigOut;
typedef struct hal_IDigOut_vtable hal_IDigOut_vtable;

struct hal_IDigOut
{
    hal_IDigOut_vtable const * /*const*/ vtable;
    void * /*const*/ self;
};

struct hal_IDigOut_vtable
{
    void (*set_state)(hal_IDigOut * self, bool state);
    void (*toggle)(hal_IDigOut * self);
};
