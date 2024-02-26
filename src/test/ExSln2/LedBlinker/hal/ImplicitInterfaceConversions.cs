using finlang;

namespace hal;

public class ImplicitInterfaceConversions : FinObj
{
    public static void Test(GpioDigInOut gpio_dio)
    {
        IDigInOut dio = gpio_dio;
        IDigIn dig_in = gpio_dio;
        IDigOut dig_out = gpio_dio;

        take_dig_in(gpio_dio);
        take_dig_in(dio);

        take_dig_in(dig_in); // shouldn't have a conversion
        IDigIn dig_in2 = dig_in; // shouldn't have a conversion
    }

    public static void take_dig_in(IDigIn dig_in)
    {
    }
}
