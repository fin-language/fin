#include "app_Main.h"
#include "hal_Gpio.h"
#include "hal_Led.h"
#include <stdio.h>
#include "hal_GpioDigInOut.h"
#include "ts_FuncPtrEx2.h"
#include "ts_MemStackEx.h"
#include <assert.h>
#include "ts_CConstEx_Bike.h"
#include "ts_CConstEx.h"

// https://stackoverflow.com/a/4415646/7331858
#define COUNT_OF(x) ((sizeof(x)/sizeof(0[x])) / ((size_t)(!(sizeof(x) % sizeof(0[x])))))

static void test_interface_conversions(hal_GpioDigInOut * gpio_dio);
static void test_func_pointers(void);
static void test_mem_stack_ex(void);
static void test_c_const_ex(void);

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
    test_mem_stack_ex();
    test_c_const_ex();

    printf("Done!\n");

    return 0;
}

static void test_func_pointers(void)
{
    printf("\n######## Testing function pointers #######\n");

    ts_FuncPtrEx2 obj;
    ts_FuncPtrEx2_ctor(&obj);

    int32_t result = ts_FuncPtrEx2_Run(&obj, 10, 5);
    printf("Should be 15: %d\n", result);
    assert(result == 15);

    ts_FuncPtrEx2_use_sub(&obj);
    result = ts_FuncPtrEx2_Run(&obj, 10, 5);
    printf("Should be 5: %d\n", result);
    assert(result == 5);

    printf("\n\n");
}

static void test_interface_conversions(hal_GpioDigInOut * gpio_dio)
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

static void test_mem_stack_ex(void)
{
    printf("\n######## Testing mem stack ex #######\n");

    int32_t result = ts_MemStackEx_calc_stuff(10, 20);
    printf("Should be 30: %d\n", result);
    assert(result == 30);

    result = ts_MemStackEx_chain_stack_creation();
    printf("Should be 10: %d\n", result);
    assert(result == 10);

    result = ts_MemStackEx_calc_stuff_compound_literals_func_args();
    printf("Should be 6: %d\n", result);
    assert(result == 6);

    result = ts_MemStackEx_calc_stuff_mult_vars_on_same_line(-10, 4);
    printf("Should be -6: %d\n", result);
    assert(result == -6);

    printf("\n\n");
}

static void test_c_const_ex(void)
{
    printf("\n######## Testing c const ex #######\n");

    int32_t result;

    const ts_CConstEx_Bike const_bike1 = {10};
    const ts_CConstEx_Bike const_bike2 = {20};

    ts_CConstEx ex;
    ts_CConstEx_ctor(&ex, &const_bike1, &const_bike2);

    result = ts_CConstEx_sum_owned_const_bikes(&ex);
    printf("Should be 30: %d\n", result);
    assert(result == 30);

    /////// end of instance tests

    const ts_CConstEx_Bike bikes[2] = {
        {70},
        {30}
    };

    result = ts_CConstEx_sum_two_const_bikes_ro(&const_bike1, &const_bike2);
    printf("Should be 30: %d\n", result);
    assert(result == 30);

    result = ts_CConstEx_sum_two_const_bikes_attr(&const_bike1, &const_bike2);
    printf("Should be 30: %d\n", result);
    assert(result == 30);

    ts_CConstEx_Bike mutable_bike1 = {22};
    ts_CConstEx_Bike mutable_bike2 = {11};

    result = ts_CConstEx_sum_two_bikes_in(&mutable_bike1, &mutable_bike2);
    printf("Should be 33: %d\n", result);
    assert(result == 33);

    result = ts_CConstEx_sum_two_const_bikes_in(&const_bike1, &const_bike2);
    printf("Should be 30: %d\n", result);
    assert(result == 30);
    result = ts_CConstEx_sum_two_const_bikes_in(&mutable_bike1, &mutable_bike2);
    printf("Should be 33: %d\n", result);
    assert(result == 33);

    result = ts_CConstEx_sum_two_bikes_in_mutate(&mutable_bike1, &mutable_bike2);
    printf("Should be 44 + 33 = 77: %d\n", result);
    assert(result == 77);
    assert(mutable_bike1.speed == 44);
    assert(mutable_bike2.speed == 33);

    result = ts_CConstEx_sum_two_const_u8(15, 23);
    printf("Should be 38: %d\n", result);
    assert(result == 38);

    result = ts_CConstEx_sum_two_u8_in(15, 23);
    printf("Should be 38: %d\n", result);
    assert(result == 38);

    result = ts_CConstEx_sum_const_bikes_array(bikes, COUNT_OF(bikes));
    printf("Should be 100: %d\n", result);
    assert(result == 100);

    printf("\n\n");
}
