#include "app_Main.h"
#include "hal_Gpio.h"
#include "hal_Led.h"
#include <stdio.h>
#include "hal_GpioDigInOut.h"
#include <assert.h>

void test_interface_conversions(hal_GpioDigInOut * gpio_dio);

int main(void)
{
    printf("Starting!\n");

    hal_Gpio gpio = {13};
    hal_GpioDigInOut gpio_dio;
    hal_GpioDigInOut_ctor(&gpio_dio, &gpio);

    hal_Led redLed;
    hal_Led_ctor(&redLed, &M_hal_GpioDigInOut__to__hal_IDigInOut(&gpio_dio));

    app_Main mainApp;
    app_Main_ctor(&mainApp, &redLed, 2000);

    for (uint32_t i = 0; i < 6; i++)
    {
        uint32_t system_time_ms = i * 1000;
        printf("step @%d ms\n", system_time_ms);
        app_Main_step(&mainApp, system_time_ms);
        printf("\n");
    }

    test_interface_conversions(&gpio_dio);

    printf("Done!\n");

    return 0;
}

void test_interface_conversions(hal_GpioDigInOut * gpio_dio)
{
    printf("\n######## Testing interface conversions #######\n");
    hal_IDigInOut * dio = &M_hal_GpioDigInOut__to__hal_IDigInOut(gpio_dio);
    assert(dio->self == gpio_dio);
    
    // cast to uint64_t to avoid warnings about comparing function pointers that take different arguments (void* vs hal_GpioDigInOut*)

    assert(dio->self == gpio_dio);
    assert((uint64_t)dio->vtable->read_state == (uint64_t)hal_GpioDigInOut_read_state);
    assert((uint64_t)dio->vtable->set_state == (uint64_t)hal_GpioDigInOut_set_state);
    assert((uint64_t)dio->vtable->toggle == (uint64_t)hal_GpioDigInOut_toggle);

    hal_IDigIn * di = &M_hal_GpioDigInOut__to__hal_IDigIn(gpio_dio);
    assert(di->self == gpio_dio);
    assert((uint64_t)di->vtable->read_state == (uint64_t)hal_GpioDigInOut_read_state);

    hal_IDigOut * dout = &M_hal_GpioDigInOut__to__hal_IDigOut(gpio_dio);
    assert(dout->self == gpio_dio);
    assert((uint64_t)dout->vtable->set_state == (uint64_t)hal_GpioDigInOut_set_state);
    assert((uint64_t)dout->vtable->toggle == (uint64_t)hal_GpioDigInOut_toggle);

    di = &M_hal_IDigInOut__to__hal_IDigIn(dio);
    assert(di->self == gpio_dio);
    assert((uint64_t)di->vtable->read_state == (uint64_t)hal_GpioDigInOut_read_state);

    dout = &M_hal_IDigInOut__to__hal_IDigOut(dio);
    assert(dout->self == gpio_dio);
    assert((uint64_t)dout->vtable->set_state == (uint64_t)hal_GpioDigInOut_set_state);
    assert((uint64_t)dout->vtable->toggle == (uint64_t)hal_GpioDigInOut_toggle);

    printf("Should toggle twice (IDigOut)\n");
    hal_IDigOut_toggle(dout);
    hal_IDigOut_toggle(dout);

    printf("Should toggle twice (IDigInOut)\n");
    hal_IDigInOut_toggle(dio);
    hal_IDigInOut_toggle(dio);
}

