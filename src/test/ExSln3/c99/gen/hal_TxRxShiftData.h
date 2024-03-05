// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang v0.3.0-alpha generated this file for C# `hal.TxRxShiftData` type.
// Source file: `LedBlinker/hal/shift/TxRxShiftData.cs` (relative to C# solution).
// MD5 hash of source file: f8ba2c5903d778ec3566fe8b8d2ad018.

#pragma once

#include <stdint.h>



typedef struct hal_TxRxShiftData hal_TxRxShiftData;
struct hal_TxRxShiftData
{
    uint8_t tx_data;
    uint8_t rx_data;
};


uint8_t hal_TxRxShiftData_get_tx_data(hal_TxRxShiftData * self);

void hal_TxRxShiftData_set_tx_data(hal_TxRxShiftData * self, uint8_t data);

uint8_t hal_TxRxShiftData_get_rx_data(hal_TxRxShiftData * self);

void hal_TxRxShiftData_set_rx_data(hal_TxRxShiftData * self, uint8_t data);

// vtable is extern to allow const initializations
extern const hal_IShiftDataFullAccess_vtable hal_IShiftDataFullAccess_vtable_imp;

// Up conversion from hal_TxRxShiftData to hal_IShiftDataFullAccess interface
#define M_hal_TxRxShiftData__to__hal_IShiftDataFullAccess(self_arg)    (hal_IShiftDataFullAccess){ .obj = self_arg, .obj_vtable = (const hal_IShiftDataFullAccess_vtable*)(&hal_IShiftDataFullAccess_vtable_imp.get_tx_data) }

// Up conversion from hal_TxRxShiftData to hal_IShiftDataUser interface
#define M_hal_TxRxShiftData__to__hal_IShiftDataUser(self_arg)    (hal_IShiftDataUser){ .obj = self_arg, .obj_vtable = (const hal_IShiftDataUser_vtable*)(&hal_IShiftDataFullAccess_vtable_imp.get_tx_data) }
