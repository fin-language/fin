﻿using hal;
using finlang;
using board;

namespace app;

public class LedBouncer : FinObj
{
    public i16 _ms_per_frame = 100;
    public i16 _ms_before_next_frame;
    public u32 _last_time_ms = 0;
    public u8 _led_index = 0;
    public i8 _led_direction = 1;
    public DigOutArray _sweep_leds;

    public LedBouncer(DigOutArray sweep_leds)
    {
        _sweep_leds = sweep_leds;
        reset_frame_time_left();
    }

    private void reset_frame_time_left()
    {
        _ms_before_next_frame = _ms_per_frame;
    }


    /// <summary>
    /// This is the main loop of the application.
    /// </summary>
    public void step(u32 cur_time_ms)
    {
        math.unsafe_mode();
        u32 ms_since_last_frame = calc_ms_since_last_step(cur_time_ms, _last_time_ms);
        
        _last_time_ms = cur_time_ms;
        _ms_before_next_frame -= ms_since_last_frame.narrow_to_i16();

        if (_ms_before_next_frame <= 0)
        {
            reset_frame_time_left();
            show_next_frame();
        }
    }

    private void show_next_frame()
    {
        var leds = _sweep_leds;

        // turn off recently lit led
        leds.unsafe_get(_led_index).set_output_state(false);

        // move to next led
        if (_led_index == 0)
        {
            _led_direction = 1;
        } 
        else if (_led_index == leds.count() - 1)
        {
            _led_direction = -1;
        }

        _led_index = (_led_index + _led_direction).narrow_to_u8();

        // turn on next led
        leds.unsafe_get(_led_index).set_output_state(true);
    }

    private static u32 calc_ms_since_last_step(u32 cur_time_ms, u32 last_time_ms)
    {
        u32 ms_since_last_frame;

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
}
