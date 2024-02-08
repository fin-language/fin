using finlang;

namespace hal;

public class LedArray : FinObj
{
    public c_array<Led> _leds;
    public u8 _leds_length;

    public LedArray(c_array<Led> leds, u8 leds_length)
    {
        this._leds = leds;
        _leds_length = leds_length;
    }

    public Led getLed(u8 index)
    {
        return _leds.unsafe_get(index);
    }

    public Led? maybe_get_led(u8 index)
    {
        if (index < _leds_length)
        {
            return _leds.unsafe_get(index);
        }
        return null;
    }
}
