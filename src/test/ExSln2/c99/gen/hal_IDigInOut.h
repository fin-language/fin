// finlang generated file for c# hal.IDigInOut type
#pragma once

#include <stdbool.h>


typedef struct hal_IDigInOut hal_IDigInOut;
typedef struct hal_IDigInOut_vtable hal_IDigInOut_vtable;

struct hal_IDigInOut
{
    hal_IDigInOut_vtable const * /*const*/ vtable;
    void * /*const*/ self;
};

struct hal_IDigInOut_vtable
{
    bool (*read_state)(hal_IDigInOut * self);
    void (*set_state)(hal_IDigInOut * self, bool state);
    void (*toggle)(hal_IDigInOut * self);
};
