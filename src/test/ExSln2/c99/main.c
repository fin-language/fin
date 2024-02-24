#include "app_Main.h"
#include "hal_Gpio.h"
#include "hal_Led.h"
#include <stdio.h>
#include "hal_GpioDigInOut.h"


int main(void)
{
    printf("Starting!\n");

    hal_Gpio gpio = {13};
    hal_GpioDigInOut gpio_dio;
    hal_GpioDigInOut_ctor(&gpio_dio, &gpio);

    hal_Led redLed;
    hal_Led_ctor(&redLed, M_hal_GpioDigInOut__to__hal_IDigInOut(&gpio_dio));

    app_Main mainApp;
    app_Main_ctor(&mainApp, &redLed, 2000);

    for (uint32_t i = 0; i < 6; i++)
    {
        uint32_t system_time_ms = i * 1000;
        printf("step @%d ms\n", system_time_ms);
        app_Main_step(&mainApp, system_time_ms);
        printf("\n");
    }
    printf("Done!\n");

    return 0;
}



