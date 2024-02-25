using finlang;

namespace hal;

public class ImplicitInterfaceConversions : FinObj
{
    public static void Test(GpioDigInOut gpio_dio)
    {
        IDigInOut dio = gpio_dio;
        IDigIn digIn = gpio_dio;
        IDigOut digOut = gpio_dio;

        take_dig_in(gpio_dio);
        take_dig_in(dio);
    }

    public static void take_dig_in(IDigIn digIn)
    {
    }
}
