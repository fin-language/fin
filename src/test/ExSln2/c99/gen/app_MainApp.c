// finlang generated file for c# app.MainApp class

#include "app_MainApp.h"
#include "hal_Helper.h"
#include <string.h>



    void app_MainApp_ctor(app_MainApp * self, hal_Led * redLed, uint16_t period_ms)
    {
        memset(self, 0, sizeof(*self));
        self->_redLed = redLed;
        self->period_ms = period_ms;
    }
    
    /// <summary>
    /// This is the main loop of the application.
    /// </summary>
    void app_MainApp_step(app_MainApp * self, uint32_t ms_time)
    {
        /* fin: math.unsafe_mode() */

        if (ms_time >= self->_toggle_at_ms) // this isn't rollover safe :P
        {
            hal_Led_toggle(self->_redLed);
            self->_toggle_at_ms = (uint32_t)(((uint64_t)(ms_time) + self->period_ms)); // this will be nicer when we have a wrapping add method
        }
    }

    uint32_t app_MainApp_transpilation_test_stuff(app_MainApp * self)
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

        return self->_toggle_at_ms + 20; //u32_ is self-declaration
    }
