#pragma once
#include <stdint.h>

typedef struct mcu_stm32_Stm32Gpio mcu_stm32_Stm32Gpio;

struct mcu_stm32_Stm32Gpio {
    GPIO_TypeDef * port;    // like GPIOA
    uint16_t pin;           // like GPIO_Pin_12
};
