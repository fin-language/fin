using finlang;

namespace hal;

public class Led : FinObj
{
    public Gpio _gpio;

    public Led(Gpio gpio)
    {
        _gpio = gpio;
    }

    // Will toggle the state of the LED
    public void toggle()
    {
        if (_gpio.read())
        {
            _gpio.write(false); // Turn off
        }
        else
        {
            _gpio.write(true); // Turn on
        }
    }
}