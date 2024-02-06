// finlang generated file for c# app.MainApp class

#include "app_MainApp.h"
#include <string.h>



    void app_MainApp_ctor(app_MainApp * self, hal_Led * redLed)
    {
        memset(self, 0, sizeof(*self));
        _redLed = redLed;
    }

    void app_MainApp_step(app_MainApp * self, uint32_t ms_time)
    {
        finlang_math_unsafe_mode(finlang_math);

        if (ms_time >= _toggle_at_ms)
        {
            // comment out the following line and it all works fine.
            hal_Led_toggle(_redLed);   // this causes really weird Roslyn errors https://github.com/fin-language/fin/issues/22
            _toggle_at_ms = (uint32_t)(((uint64_t)(ms_time) + 1000)); // won't need when have a wrapping add
        }
    }

    uint32_t app_MainApp_self_declaration_example(app_MainApp * self)
    {
        finlang_math_unsafe_mode(finlang_math);
        return _toggle_at_ms + 20; //u32_ is self-declaration
    }
