#pragma once
#include <stdbool.h>
#include <stdio.h>

typedef struct app_Led {
    int pin;
} app_Led;

void app_Led_set_state(app_Led * self, bool state);
