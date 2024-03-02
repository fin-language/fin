using finlang;

namespace hal;

/// <summary>
/// Example for https://github.com/fin-language/fin/issues/41, 
/// https://github.com/fin-language/fin/issues/43
/// https://github.com/fin-language/fin/issues/44
/// </summary>
[add_include("my_stm_gpio_type.h")]
[add_include("my_stm_gpio_defines.h")]
public class ExTypeOverride : FinObj
{
    /// <summary>
    /// This is a string type so that we can use C99 types like GPIO_TypeDef that fin doesn't know about.
    /// </summary>
    [override_type("GPIO_TypeDef *")]
    public string port;

    /// <summary>
    /// This is a string type so that we can use C99 defines like GPIO_PIN_0 that fin doesn't know about.
    /// </summary>
    [override_type("uint16_t")]
    public string pin;

    public ExTypeOverride([override_type("GPIO_TypeDef *")] string port, [override_type("uint16_t")] string pin)
    {
        this.port = port;
        this.pin = pin;
    }

    public void set_pin([override_type("uint16_t")] string pin)
    {
        this.pin = pin;
    }

    public void set_pin_use_example()
    {
        set_pin(FinC.EchoToC("GPIO_PIN_0"));
    }
}