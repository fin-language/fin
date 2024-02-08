// finlang generated file for c# hal.CArrayDependencyTest type

#include "hal_CArrayDependencyTest.h"
#include <string.h>



void hal_CArrayDependencyTest_ctor(hal_CArrayDependencyTest * self, uint8_t * data)
{
    memset(self, 0, sizeof(*self));
    self->_data = data;
}
