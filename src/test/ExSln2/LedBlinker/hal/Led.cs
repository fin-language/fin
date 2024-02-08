using finlang;

namespace hal;

public class Led : FinObj
{
    public Gpio _gpio;
    public u8 my_public_var;

    public Led(Gpio gpio)
    {
        _gpio = gpio;
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
        if (_gpio.read() == GpioPinState.High)
        {
            _gpio.write(GpioPinState.Low); // Turn off
        }
        else
        {
            _gpio.write(GpioPinState.High); // Turn on
        }
    }
}