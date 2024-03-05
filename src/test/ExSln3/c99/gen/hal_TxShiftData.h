// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang v0.3.0-alpha generated this file for C# `hal.TxShiftData` type.
// Source file: `LedBlinker\hal\shift\TxShiftData.cs` (relative to C# solution).
// MD5 hash of source file: 01e6e0f8b52dcaa24b2a434c7bfe566a.

#pragma once

#include <stdint.h>



typedef struct hal_TxShiftData hal_TxShiftData;
struct hal_TxShiftData
{
    uint8_t tx_data;
};


uint8_t hal_TxShiftData_get_tx_data(hal_TxShiftData * self);

void hal_TxShiftData_set_tx_data(hal_TxShiftData * self, uint8_t data);

uint8_t hal_TxShiftData_get_rx_data(hal_TxShiftData * self);

void hal_TxShiftData_set_rx_data(hal_TxShiftData * self, uint8_t data);

// vtable is extern to allow const initializations
extern const hal_IShiftDataFullAccess_vtable hal_IShiftDataFullAccess_vtable_imp;

// Up conversion from hal_TxShiftData to hal_IShiftDataFullAccess interface
#define M_hal_TxShiftData__to__hal_IShiftDataFullAccess(self_arg)    (hal_IShiftDataFullAccess){ .obj = self_arg, .obj_vtable = (const hal_IShiftDataFullAccess_vtable*)(&hal_IShiftDataFullAccess_vtable_imp.get_tx_data) }

// Up conversion from hal_TxShiftData to hal_IShiftDataUser interface
#define M_hal_TxShiftData__to__hal_IShiftDataUser(self_arg)    (hal_IShiftDataUser){ .obj = self_arg, .obj_vtable = (const hal_IShiftDataUser_vtable*)(&hal_IShiftDataFullAccess_vtable_imp.get_tx_data) }
