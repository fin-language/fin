// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `hal.LedArray` type.
// Source file: `LedBlinker\hal\LedArray.cs` (relative to C# solution).


#include "hal_LedArray.h"
#include <string.h>



void hal_LedArray_ctor(hal_LedArray * self, hal_Led * * leds, uint8_t leds_length, hal_LedArray_LedInfo * led_info)
{
    memset(self, 0, sizeof(*self));
    self->_leds = leds;
    self->_leds_length = leds_length;
    self->_led_info = led_info;
}

/// <summary>
/// This is actually a private method
/// </summary>
hal_Led * PRIVATE_hal_LedArray__get_led(hal_LedArray * self, uint8_t index)
{
    return self->_leds[index];
}

hal_Led * hal_LedArray_maybe_get_led(hal_LedArray * self, uint8_t index)
{
    if (index < self->_leds_length)
    {
        return PRIVATE_hal_LedArray__get_led(self, index);
    }
    return NULL;
}

void hal_LedArray_increment_index(hal_LedArray * self)
{
    self->_index = hal_LedArray_inc_wrap(self->_index, self->_leds_length);
}

// just an excuse for a static method :)    
uint8_t hal_LedArray_inc_wrap(uint8_t value, uint8_t max)
{
    value++;
    if (value >= max)
    {
        value = 0;
    }
    return value;
}
