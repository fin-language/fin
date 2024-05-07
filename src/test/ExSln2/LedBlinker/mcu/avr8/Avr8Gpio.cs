using fakes;
using finlang;
using hal;

namespace mcu.avr8;

/// <summary>
/// We want a .c/h file to be generated for this class and have it setup the vtable.
/// </summary>
[ffi]
public class Avr8Gpio : FinObj, IGpio
{
    public bool enable_pulldown()
    {
        throw new System.NotImplementedException();
    }

    public bool enable_pullup()
    {
        throw new System.NotImplementedException();
    }

    public bool read_state()
    {
        throw new System.NotImplementedException();
    }

    public void set_state(bool state)
    {
        throw new System.NotImplementedException();
    }

    public void toggle()
    {
        throw new System.NotImplementedException();
    }
}
