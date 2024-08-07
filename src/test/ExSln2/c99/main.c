#include "app_Main.h"
#include "hal_Gpio.h"
#include "hal_Led.h"
#include <stdio.h>
#include "hal_GpioDigInOut.h"
#include "hal_FuncPtrEx2.h"
#include <assert.h>

void test_interface_conversions(hal_GpioDigInOut * gpio_dio);
void test_func_pointers(void);

int main(void)
{
    printf("Starting!\n");

    hal_Gpio gpio = {13};
    hal_GpioDigInOut gpio_dio;
    hal_GpioDigInOut_ctor(&gpio_dio, &gpio);

    hal_Led redLed;
    hal_Led_ctor(&redLed, &MCL_hal_GpioDigInOut__to__hal_IDigInOut(&gpio_dio));

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

    test_func_pointers();

    printf("Done!\n");

    return 0;
}

void test_func_pointers(void)
{
    printf("\n######## Testing function pointers #######\n");

    hal_FuncPtrEx2 obj;
    hal_FuncPtrEx2_ctor(&obj);

    int32_t result = hal_FuncPtrEx2_Run(&obj, 10, 5);
    printf("Should be 15: %d\n", result);
    assert(result == 15);

    hal_FuncPtrEx2_use_sub(&obj);
    result = hal_FuncPtrEx2_Run(&obj, 10, 5);
    printf("Should be 5: %d\n", result);
    assert(result == 5);

    printf("\n\n");
}

void test_interface_conversions(hal_GpioDigInOut * gpio_dio)
{
    printf("\n######## Testing interface conversions #######\n");
    hal_IDigInOut * dio = &MCL_hal_GpioDigInOut__to__hal_IDigInOut(gpio_dio);
    assert(dio->obj == gpio_dio);
    
    // cast to uint64_t to avoid warnings about comparing function pointers that take different arguments (void* vs hal_GpioDigInOut*)

    assert(dio->obj == gpio_dio);
    assert((uint64_t)dio->obj_vtable->read_state == (uint64_t)hal_GpioDigInOut_read_state);
    assert((uint64_t)dio->obj_vtable->set_state == (uint64_t)hal_GpioDigInOut_set_state);
    assert((uint64_t)dio->obj_vtable->toggle == (uint64_t)hal_GpioDigInOut_toggle);

    hal_IDigIn * di = &MCL_hal_GpioDigInOut__to__hal_IDigIn(gpio_dio);
    assert(di->obj == gpio_dio);
    assert((uint64_t)di->obj_vtable->read_state == (uint64_t)hal_GpioDigInOut_read_state);

    hal_IDigOut * dout = &MCL_hal_GpioDigInOut__to__hal_IDigOut(gpio_dio);
    assert(dout->obj == gpio_dio);
    assert((uint64_t)dout->obj_vtable->set_state == (uint64_t)hal_GpioDigInOut_set_state);
    assert((uint64_t)dout->obj_vtable->toggle == (uint64_t)hal_GpioDigInOut_toggle);

    di = &MCL_hal_IDigInOut__to__hal_IDigIn(dio);
    assert(di->obj == gpio_dio);
    assert((uint64_t)di->obj_vtable->read_state == (uint64_t)hal_GpioDigInOut_read_state);

    dout = &MCL_hal_IDigInOut__to__hal_IDigOut(dio);
    assert(dout->obj == gpio_dio);
    assert((uint64_t)dout->obj_vtable->set_state == (uint64_t)hal_GpioDigInOut_set_state);
    assert((uint64_t)dout->obj_vtable->toggle == (uint64_t)hal_GpioDigInOut_toggle);

    printf("Should toggle twice (IDigOut)\n");
    hal_IDigOut_toggle(dout);
    hal_IDigOut_toggle(dout);

    printf("Should toggle twice (IDigInOut)\n");
    hal_IDigInOut_toggle(dio);
    hal_IDigInOut_toggle(dio);

    printf("\n\n");
}

