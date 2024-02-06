// finlang generated file for c# app.Counter class

#include "app_Counter.h"
#include <string.h>



    void app_Counter_ctor(app_Counter * self, )
    {
        memset(self, 0, sizeof(*self));
        self->_counts = counts;
    }

    void app_Counter_increment(app_Counter * self)
    {
        self->_count += 1;
    }
