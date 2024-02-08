// finlang generated file for c# hal.LedArray type

#include "hal_LedArray.h"
#include <string.h>



void hal_LedArray_ctor(hal_LedArray * self, hal_Led * * leds, uint8_t leds_length)
{
    memset(self, 0, sizeof(*self));
    self->_leds = leds;
    self->_leds_length = leds_length;
}

hal_Led * hal_LedArray_getLed(hal_LedArray * self, uint8_t index)
{
    return self->_leds[index];
}

hal_Led * hal_LedArray_maybe_get_led(hal_LedArray * self, uint8_t index)
{
    if (index < self->_leds_length)
    {
        return self->_leds[index];
    }
    return NULL;
}
