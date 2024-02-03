using finlang;

namespace Hal;

public class Led : FinObj
{
    public Gpio _gpio;

    public Led(Gpio gpio)
    {
        _gpio = gpio;
    }

    public void toggle()
    {
        if (_gpio.read())
        {
            _gpio.write(false);
        }
        else
        {
            _gpio.write(true);
        }
    }
}