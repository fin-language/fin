#pragma once

#include <stdint.h>

typedef struct app_TimeProvider {
    char _unused;
} app_TimeProvider;

uint32_t app_TimeProvider_get_time(app_TimeProvider * self);
