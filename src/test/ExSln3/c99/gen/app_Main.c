// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang v0.2.6-alpha generated this file for C# `app.Main` type.
// Source file: `LedBlinker\app\Main.cs` (relative to C# solution).
// MD5 hash of source file: c79a7b35ccd679ff97ee04d55829edea.


#include "app_Main.h"
#include <string.h>



void app_Main_ctor(app_Main * self, uint16_t period_ms)
{
    memset(self, 0, sizeof(*self));
    self->period_ms = period_ms;
}

/// <summary>
/// This is the main loop of the application.
/// </summary>
void app_Main_step(app_Main * self, uint32_t ms_time)
{
    /* fin: math.unsafe_mode() */

    if (ms_time >= self->_toggle_at_ms) // this isn't rollover safe :P
    {
    }
}