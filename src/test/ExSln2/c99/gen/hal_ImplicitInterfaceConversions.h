// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `hal.ImplicitInterfaceConversions` type.
// Source file: `LedBlinker/test_stuff/ImplicitInterfaceConversions.cs` (relative to C# solution).

#pragma once

#include "hal_GpioDigInOut.h"
#include "hal_IDigIn.h"


// Class has no fields. No struct generated.
void hal_ImplicitInterfaceConversions_test(hal_GpioDigInOut * gpio_dio);

void hal_ImplicitInterfaceConversions_take_dig_in(hal_IDigIn * dig_in);


hal_IDigIn hal_ImplicitInterfaceConversions_convert_gpio(hal_GpioDigInOut * gpio_dio);
