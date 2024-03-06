using finlang;
using static finlang.FinC; // for ignore_unused()

namespace hal;

public class ImplicitInterfaceConversions : FinObj
{
    public static void test(GpioDigInOut gpio_dio)
    {
        IDigInOut dio = gpio_dio;
        IDigIn dig_in = gpio_dio;
        IDigOut dig_out = gpio_dio;

        take_dig_in(gpio_dio);
        take_dig_in(dio);

        take_dig_in(dig_in); // shouldn't have a conversion
        IDigIn dig_in2 = dig_in; // shouldn't have a conversion

        dig_in = convert_gpio(gpio_dio); // can't do this. Can't take address of return value of method. `dig_in = &hal_ImplicitInterfaceConversions_convert_gpio(gpio_dio);`

        FinC.ignore_unused(dig_out);
        ignore_unused(dig_in2);
        ignore_unused(dig_in);
    }

    public static void take_dig_in(IDigIn dig_in)
    {
         FinC.ignore_unused(dig_in);
    }

    [mem]
    public static IDigIn convert_gpio(GpioDigInOut gpio_dio)
    {
        return gpio_dio;
    }
}
