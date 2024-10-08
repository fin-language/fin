// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `app.Main` type.
// Source file: `LedBlinker/app/Main.cs` (relative to C# solution).


#include "app_Main.h"
#include "hal_Helper.h"
#include <stdint.h>
#include <string.h>



app_Main * app_Main_ctor(app_Main * self, hal_Led * redLed, uint16_t period_ms)
{
    memset(self, 0, sizeof(*self));
    self->_redLed = redLed;
    self->_redLed->my_public_var = 5;
    self->period_ms = period_ms;
    return self;
}

/// <summary>
/// This is the main loop of the application.
/// </summary>
void app_Main_step(app_Main * self, uint32_t ms_time)
{
    /* fin: math.unsafe_mode() */

    if (ms_time >= self->_toggle_at_ms) // this isn't rollover safe :P
    {
        hal_Led_toggle(self->_redLed);
        self->_toggle_at_ms = (uint32_t)(((uint64_t)(ms_time) + self->period_ms)); // this will be nicer when we have a wrapping add method
    }
}

uint32_t app_Main_transpilation_test_stuff(app_Main * self)
{
    /* fin: math.unsafe_mode() */
    hal_Helper_calc_stuff(3, 77);
    uint8_t a = 55;
    uint8_t b;
    int32_t my_i32 = 55;
    b = (uint8_t)my_i32;
    b = (uint8_t)(a + my_i32);
    b = (uint8_t)my_i32;
    b = (uint8_t)my_i32;
    (void)(b);

    return self->_toggle_at_ms + 20; //u32_ is self-declaration
}
