#include "app_Led_ffi.h"

void app_Led_set_state(app_Led * self, bool state)
{
    printf("Led %i set to %i\n", self->pin, state);

    // hack to make the output easier to follow
    if (self->pin == 2)
    {
        printf("\n");
    }
}
