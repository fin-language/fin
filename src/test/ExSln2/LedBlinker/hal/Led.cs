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