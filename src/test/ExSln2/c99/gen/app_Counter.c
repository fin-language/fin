// finlang generated file for c# app.Counter type

#include "app_Counter.h"
#include <string.h>



void app_Counter_ctor(app_Counter * self, uint8_t * counts, uint8_t count_length)
{
    memset(self, 0, sizeof(*self));
    self->_counts = counts;
    self->_count_length = count_length;
}

/// <summary>
/// 
/// </summary>
/// <param name="index"></param>
/// <returns></returns>
uint8_t app_Counter_get(app_Counter * self, uint8_t index)
{
    uint8_t result = 0;
    if (index < self->_count_length)
    {
        result = self->_counts[index];
    }
    return result;
}

/// <summary>
/// 
/// </summary>
/// <param name="index"></param>
/// <param name="value"></param>
void app_Counter_set(app_Counter * self, uint8_t index, uint8_t value)
{
    /* fin: math.unsafe_mode() */

    if (index >= self->_count_length)
        return;

    self->_counts[index] = value;
    self->_set_invocations_counts++;
}
