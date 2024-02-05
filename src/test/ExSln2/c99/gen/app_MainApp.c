// finlang generated file for c# app.MainApp class

#include "app_MainApp.h"



    MainApp(hal_Led redLed)
    {
        _redLed = redLed;
    }

    void app_MainApp_step(uint32_t ms_time)
    {
        finlang_math.finlang_math_unsafe_mode();

        if (ms_time >= _toggle_at_ms)
        {
            // comment out the following line and it all works fine.
            _redLed.hal_Led_toggle();   // this causes really weird Roslyn errors https://github.com/fin-language/fin/issues/22
            _toggle_at_ms = (ms_time.finlang_u32_u64 + 1000).finlang_u64_wrap_u32; // won't need when have a wrapping add
        }
    }
