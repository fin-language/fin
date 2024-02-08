using finlang;

namespace hal;

public class LedArray : FinObj
{
    public c_array<Led> leds;

    public LedArray(c_array<Led> leds)
    {
        this.leds = leds;
    }

    public Led getLed(u8 index)
    {
        return leds.unsafe_get(index);
    }
}
