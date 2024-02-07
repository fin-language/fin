// finlang generated file for c# app.Counter class

#include "app_Counter.h"
#include <string.h>



    void app_Counter_ctor(app_Counter * self, , uint8_t count_length)
    {
        memset(self, 0, sizeof(*self));
        self->_counts = counts;
        self->_count_length = count_length;
    }

    uint8_t app_Counter_get(app_Counter * self, uint8_t index)
    {
        uint8_t result = 0;
        if (index < self->_count_length)
        {
            result = finlang_c_array_unsafe_get(self->_counts, index);
        }
        return result;
    }

    void app_Counter_set(app_Counter * self, uint8_t index, uint8_t value)
    {
        if (index < self->_count_length)
            finlang_c_array_unsafe_set(self->_counts, index, value);
    }
