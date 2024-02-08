// finlang generated file for c# hal.CArrayDependencyTest type
#pragma once

#include <stdint.h>



typedef struct hal_CArrayDependencyTest hal_CArrayDependencyTest;
struct hal_CArrayDependencyTest
{
    // should have dependency on uint8_t
    uint8_t * _data;
};


void hal_CArrayDependencyTest_ctor(hal_CArrayDependencyTest * self, uint8_t * data);
