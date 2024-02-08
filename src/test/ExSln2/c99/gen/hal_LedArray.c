// finlang generated file for c# hal.LedArray type

#include "hal_LedArray.h"
#include <string.h>



void hal_LedArray_ctor(hal_LedArray * self, hal_Led ** leds)
{
    memset(self, 0, sizeof(*self));
    self->leds = leds;
}

hal_Led hal_LedArray_getLed(hal_LedArray * self, uint8_t index)
{
    return self->leds[index];
}
