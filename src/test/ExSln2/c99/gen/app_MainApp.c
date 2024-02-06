// finlang generated file for c# app.MainApp class

#include "app_MainApp.h"
#include "hal_Helper.h"
#include <string.h>



    void app_MainApp_ctor(app_MainApp * self, hal_Led * redLed)
    {
        memset(self, 0, sizeof(*self));
        self->_redLed = redLed;
    }

    void app_MainApp_step(app_MainApp * self, uint32_t ms_time)
    {

        if (ms_time >= self->_toggle_at_ms)
        {
            // comment out the following line and it all works fine.
            hal_Led_toggle(self->_redLed);   // this causes really weird Roslyn errors https://github.com/fin-language/fin/issues/22
            self->_toggle_at_ms = (uint32_t)(((uint64_t)(ms_time) + 1000)); // won't need when have a wrapping add
        }
    }

    uint32_t app_MainApp_self_declaration_example(app_MainApp * self)
    {
        hal_Helper_calc_stuff(3, 77); // FIXME: add dependency to c file
        return self->_toggle_at_ms + 20; //u32_ is self-declaration
    }
