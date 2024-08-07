﻿using finlang;

namespace hal;

public class LedArray : FinObj
{
    public enum LedArrayStatus { OFF, ON, BLINKING }
    public class LedInfo : FinObj
    {
        public u8 something;
    }

    required public c_array<Led> _leds;
    public u8 _leds_length;
    public u8 _index;
    public LedInfo _led_info;

    public LedArray(c_array<Led> leds, u8 leds_length, LedInfo led_info)
    {
        this._leds = leds;
        _leds_length = leds_length;
        _led_info = led_info;
    }

    /// <summary>
    /// This is actually a private method
    /// </summary>
    public Led _get_led(u8 index)
    {
        return _leds.unsafe_get(index);
    }

    public virtual Led? maybe_get_led(u8 index)
    {
        if (index < _leds_length)
        {
            return _get_led(index);
        }
        return null;
    }

    public void increment_index()
    {
        _index = inc_wrap(_index, _leds_length);
    }

    // just an excuse for a static method :)    
    public static u8 inc_wrap(u8 value, u8 max)
    {
        value++;
        if (value >= max)
        {
            value = 0;
        }
        return value;
    }
}
