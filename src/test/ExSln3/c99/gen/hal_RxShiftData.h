// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `hal.RxShiftData` type.
// Source file: `LedBlinker/hal/shift/RxShiftData.cs` (relative to C# solution).
// MD5 hash of source file: e6e38cd33a0eed94a8171dbd34ba71f6.

#pragma once

#include <stdint.h>



typedef struct hal_RxShiftData hal_RxShiftData;
struct hal_RxShiftData
{
    uint8_t rx_data;
};


uint8_t hal_RxShiftData_get_tx_data(hal_RxShiftData * self);

void hal_RxShiftData_set_tx_data(hal_RxShiftData * self, uint8_t data);

uint8_t hal_RxShiftData_get_rx_data(hal_RxShiftData * self);

void hal_RxShiftData_set_rx_data(hal_RxShiftData * self, uint8_t data);
