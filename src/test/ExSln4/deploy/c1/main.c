#include "app_MainController.h"
#include <stdio.h>
#include <assert.h>
#include "app_Led.h"
#include "app_TimeProvider.h"
#include <time.h>

static void sleep_ms(int ms);


int main(void)
{
    printf("Starting!\n");

    app_MainController main_controller;
    app_Led led1 = { .pin = 1 };
    app_Led led2 = { .pin = 2 };
    app_TimeProvider time_provider = { 0 };

    app_ILed i_led1 = MCL_app_Led__to__app_ILed(&led1);
    app_ILed i_led2 = MCL_app_Led__to__app_ILed(&led2);
    app_ITimeProvider i_time_provider = MCL_app_TimeProvider__to__app_ITimeProvider(&time_provider);

    app_MainController_ctor(&main_controller, &i_time_provider, &i_led1, &i_led2);

    while (true) {
        sleep_ms(10);
        app_MainController_update(&main_controller);
    }

    return 0;
}

static void sleep_ms(int ms)
{
    struct timespec ts = { .tv_sec = 0, .tv_nsec = ms * 1000*1000 };
    nanosleep(&ts, NULL);
}
