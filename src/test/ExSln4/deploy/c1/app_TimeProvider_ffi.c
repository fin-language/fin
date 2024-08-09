#define _POSIX_C_SOURCE 200809L
#include <time.h>

#include "app_TimeProvider.h"

uint32_t app_TimeProvider_get_time(app_TimeProvider * self)
{
    struct timespec spec;

    clock_gettime(CLOCK_MONOTONIC, &spec);

    long long milliseconds = spec.tv_sec * 1000LL + spec.tv_nsec / 1000000;

    return (uint32_t)milliseconds;
}
