using finlang;

namespace hal;

public class Led : FinObj
{
    public IDigInOut _dig_io;
    public u8 my_public_var;

    public Led(IDigInOut dig_out)
    {
        _dig_io = dig_out;
    }

    public void toggle_twice()
    {
        // to test that we can transpile a method call that doesn't have a parameter
        toggle();
        this.toggle(); // to test `this.` method invocations
    }

    public static void toggle_twice_static(Led led)
    {
        led.toggle();
        led.toggle();
    }

    // Will toggle the state of the LED
    public void toggle()
    {
        _dig_io.toggle();
    }
}