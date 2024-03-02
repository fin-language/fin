// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `hal.ExTypeOverride` type.
// Source file: `LedBlinker\test_stuff\ExTypeOverride.cs` (relative to C# solution).

#pragma once




/// <summary>
/// Example for https://github.com/fin-language/fin/issues/41
/// </summary>
typedef struct hal_ExTypeOverride hal_ExTypeOverride;
struct hal_ExTypeOverride
{
    /// <summary>
    /// This is a string type so that we can use C99 types like GPIO_TypeDef that fin doesn't know about.
    /// </summary>
    GPIO_TypeDef * port;

    /// <summary>
    /// This is a string type so that we can use C99 defines like GPIO_PIN_0 that fin doesn't know about.
    /// </summary>
    uint16_t pin;
};


void hal_ExTypeOverride_ctor(hal_ExTypeOverride * self, GPIO_TypeDef * port, uint16_t pin);

void hal_ExTypeOverride_set_pin(hal_ExTypeOverride * self, uint16_t pin);