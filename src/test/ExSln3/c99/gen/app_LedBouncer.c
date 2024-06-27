// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `app.LedBouncer` type.
// Source file: `LedBlinker/app/LedBouncer.cs` (relative to C# solution).
// MD5 hash of source file: 098473b4d5caa232f4a100713e625a14.


#include "app_LedBouncer.h"
#include "hal_IDigOut.h"
#include <string.h>



void app_LedBouncer_ctor(app_LedBouncer * self, hal_DigOutArray * sweep_leds)
{
    memset(self, 0, sizeof(*self));
    self->_ms_per_frame = 100;
    self->_last_time_ms = 0;
    self->_led_index = 0;
    self->_led_direction = 1;
    self->_sweep_leds = sweep_leds;
    PRIVATE_app_LedBouncer_reset_frame_time_left(self);
}

void PRIVATE_app_LedBouncer_reset_frame_time_left(app_LedBouncer * self)
{
    self->_ms_before_next_frame = self->_ms_per_frame;
}


/// <summary>
/// This is the main loop of the application.
/// </summary>
void app_LedBouncer_step(app_LedBouncer * self, uint32_t cur_time_ms)
{
    /* fin: math.unsafe_mode() */
    uint32_t ms_since_last_frame = PRIVATE_app_LedBouncer_calc_ms_since_last_step(cur_time_ms, self->_last_time_ms);
    
    self->_last_time_ms = cur_time_ms;
    self->_ms_before_next_frame -= (int16_t)ms_since_last_frame;

    if (self->_ms_before_next_frame <= 0)
    {
        PRIVATE_app_LedBouncer_reset_frame_time_left(self);
        PRIVATE_app_LedBouncer_show_next_frame(self);
    }
}

void PRIVATE_app_LedBouncer_show_next_frame(app_LedBouncer * self)
{
    hal_DigOutArray * leds = self->_sweep_leds;

    // turn off recently lit led
    hal_IDigOut_set_output_state(
    // turn off recently lit led
    hal_DigOutArray_unsafe_get(leds, self->_led_index), false);

    // move to next led
    if (self->_led_index == 0)
    {
        self->_led_direction = 1;
    } 
    else if (self->_led_index == hal_DigOutArray_count(leds) - 1)
    {
        self->_led_direction = -1;
    }

    self->_led_index = (uint8_t)(self->_led_index + self->_led_direction);

    // turn on next led
    hal_IDigOut_set_output_state(
    // turn on next led
    hal_DigOutArray_unsafe_get(leds, self->_led_index), true);
}

uint32_t PRIVATE_app_LedBouncer_calc_ms_since_last_step(uint32_t cur_time_ms, uint32_t last_time_ms)
{
    uint32_t ms_since_last_frame;

    if (cur_time_ms >= last_time_ms)
    {
        ms_since_last_frame = cur_time_ms - last_time_ms;
    }
    else
    {
        // we've rolled over
        ms_since_last_frame = last_time_ms - cur_time_ms;
    }

    return ms_since_last_frame;
}
