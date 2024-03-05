using finlang;
using hal;
using mcu.avr8;

namespace board.tang;

/// <summary>
/// This board is based on a blue Arduino Uno board. "Blue Tang".
/// </summary>
public class TangRev1 : FinObj, IGeneralBoard
{
    [mem] Avr8Gpio _start_button = mem.init(new Avr8Gpio(FinC.EchoToC("PORTC"), 0)); // Arduino pin A0. RED button.
    [mem] Avr8Gpio _trap_button = mem.init(new Avr8Gpio(FinC.EchoToC("PORTC"), 1));  // Arduino pin A1
    [mem] Avr8Gpio _main_led = mem.init(new Avr8Gpio(FinC.EchoToC("PORTB"), 5)); // Arduino pin 13. The main BUILTIN LED.

    public TangRev1()
    {
        this._start_button.enable_pullup();
        _start_button.set_direction(GpioDirection.Input);
        _trap_button.enable_pullup();
        _trap_button.set_direction(GpioDirection.Input);
        _main_led.set_direction(GpioDirection.Output);
    }

    public IDigOut get_main_led()
    {
        return _main_led;
    }

    public IDigIn get_start_button()
    {
        return _start_button;
    }

    public IDigIn get_trap_button()
    {
        return _trap_button;
    }

    [ffi]
    public u32 get_time_ms()
    {
        return 0;
    }

    public void step()
    {
        FinC.ignore_unused(this);
    }

}
