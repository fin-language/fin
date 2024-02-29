#pragma once
#include <stdint.h>

typedef struct mcu_avr8_Avr8Gpio mcu_avr8_Avr8Gpio;

struct mcu_avr8_Avr8Gpio {
    volatile uint8_t * port;
    uint8_t pin;
};
