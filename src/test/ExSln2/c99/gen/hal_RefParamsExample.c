// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `hal.RefParamsExample` type.
// Source file: `LedBlinker/test_stuff/RefParamsExample.cs` (relative to C# solution).


#include "hal_RefParamsExample.h"


void hal_RefParamsExample_test()
{
    uint8_t a = 1;
    hal_RefParamsExample_inc(&a);
    (void)(a);
}

void hal_RefParamsExample_inc(uint8_t * a)
{
    (*a)++;
}

uint8_t hal_RefParamsExample_echo(uint8_t const a)
{
    //a++; // can't assign to in parameter
    return a;
}
