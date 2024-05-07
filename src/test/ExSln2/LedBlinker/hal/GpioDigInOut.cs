using finlang;

namespace hal;

public class GpioDigInOut : FinObj, IDigInOut
{
    public Gpio _gpio;

    public GpioDigInOut(Gpio gpio)
    {
        _gpio = gpio;
    }

    public bool read_state()
    {
        return _gpio.read() == GpioPinState.High;
    }

    public void set_state(bool state)
    {
        _gpio.write(state ? GpioPinState.High : GpioPinState.Low);
    }

    public void toggle()
    {
        GpioPinState next_state = _gpio.read() == GpioPinState.High ? GpioPinState.Low : GpioPinState.High;
        _gpio.write(next_state);
    }

    [simonly]
    public void _SimOnlyStuff()
    {
        
    }
}
