#include "app_MainApp.h"
#include "hal_Gpio.h"
#include "hal_Led.h"
#include <stdio.h>

bool fake_gpio_state = false;

int main(void)
{
    printf("Starting!\n");
    hal_Gpio gpio = {13};
    
    hal_Led redLed;
    hal_Led_ctor(&redLed, &gpio);
    
    app_MainApp mainApp;
    app_MainApp_ctor(&mainApp, &redLed, 2000);

    for (uint32_t i = 0; i < 6; i++)
    {
        uint32_t system_time_ms = i * 1000;
        printf("step @%d ms\n", system_time_ms);
        app_MainApp_step(&mainApp, system_time_ms);
        printf("\n");
    }
    printf("Done!\n");

    return 0;
}

bool hal_Gpio_read(hal_Gpio * self)
{
    return fake_gpio_state;
}

void hal_Gpio_write(hal_Gpio * self, bool state)
{
    fake_gpio_state = state;
    printf("hal_Gpio_write(%d)\n", state);
}

