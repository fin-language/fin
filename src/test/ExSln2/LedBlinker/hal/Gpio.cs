using finlang;

namespace hal;

/// <summary>
/// This class will not be generated. The user will have to provide the struct definition
/// and the function definitions
/// </summary>
[ffi]
public class Gpio : FinObj
{
    public GpioPinState _state;

    public GpioPinState read()
    {
        // STM HAL code:
        // GPIO_PinState pinState = HAL_GPIO_ReadPin(GPIOx, GPIO_PIN_x);

        return _state;
    }

    public void write(GpioPinState state)
    {
        // STM HAL code:
        // HAL_GPIO_WritePin(GPIOx, GPIO_PIN_x, GPIO_PIN_RESET); // Set pin low
        // HAL_GPIO_WritePin(GPIOx, GPIO_PIN_x, GPIO_PIN_SET);   // Set pin high
        _state = state;
    }
}
