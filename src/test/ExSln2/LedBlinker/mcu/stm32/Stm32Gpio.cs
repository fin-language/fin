using fakes;
using finlang;
using hal;

namespace mcu.stm32;

/// <summary>
/// We want a .c/h file to be generated for this class and have it setup the vtable.
/// </summary>
public class Stm32Gpio : FinObj, IGpio
{
    /// <summary>
    /// This is a string type so that we can use C99 types like GPIO_TypeDef that fin doesn't know about.
    /// </summary>
    [override_type("GPIO_TypeDef *")]
    string port;

    /// <summary>
    /// This is a string type so that we can use C99 defines like GPIO_PIN_0 that fin doesn't know about.
    /// </summary>
    [override_type("uint16_t")]
    string pin;

    public Stm32Gpio([override_type("GPIO_TypeDef *")] string port, [override_type("uint16_t")] string pin)
    {
        this.port = port;
        this.pin = pin;
    }

    [ffi]
    public bool enable_pulldown()
    {
        throw new System.NotImplementedException();
    }

    [ffi]
    public bool enable_pullup()
    {
        throw new System.NotImplementedException();
    }

    [ffi]
    public bool read_state()
    {
        throw new System.NotImplementedException();
    }

    [ffi]
    public void set_state(bool state)
    {
        throw new System.NotImplementedException();
    }

    [ffi]
    public void toggle()
    {
        throw new System.NotImplementedException();
    }
}
