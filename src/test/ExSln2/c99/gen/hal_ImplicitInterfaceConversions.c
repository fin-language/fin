// finlang generated file for c# hal.ImplicitInterfaceConversions type

#include "hal_ImplicitInterfaceConversions.h"
#include "hal_IDigInOut.h"
#include "hal_IDigOut.h"


void hal_ImplicitInterfaceConversions_Test(hal_GpioDigInOut * gpio_dio)
{
    hal_IDigInOut * dio = gpio_dio;
    hal_IDigIn * digIn = gpio_dio;
    hal_IDigOut * digOut = gpio_dio;

    hal_ImplicitInterfaceConversions_take_dig_in(&M_hal_GpioDigInOut__to__hal_IDigIn(gpio_dio));
    hal_ImplicitInterfaceConversions_take_dig_in(&M_hal_IDigInOut__to__hal_IDigIn(dio));
}

void hal_ImplicitInterfaceConversions_take_dig_in(hal_IDigIn * digIn)
{
}
