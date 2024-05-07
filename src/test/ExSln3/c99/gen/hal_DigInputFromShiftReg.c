// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `hal.DigInputFromShiftReg` type.
// Source file: `LedBlinker/hal/shift/DigInputFromShiftReg.cs` (relative to C# solution).
// MD5 hash of source file: c2f5ac399fb2e492a752b49e60426134.


#include "hal_DigInputFromShiftReg.h"
#include "hal_BitHelper.h"
#include <string.h>



void hal_DigInputFromShiftReg_ctor(hal_DigInputFromShiftReg * self, hal_RxShiftData * shift_data, uint8_t bit_mask)
{
    memset(self, 0, sizeof(*self));
    self->_shift_data = shift_data;
    self->_bit_mask = bit_mask;

    hal_BitHelper_SimOnlyEnsureSingleBitInMask(bit_mask);
}

bool hal_DigInputFromShiftReg_read_input(hal_DigInputFromShiftReg * self)
{
    return (hal_RxShiftData_get_rx_data(self->_shift_data) & self->_bit_mask) == 1;
}

// virtual table implementation for IDigIn. Note that this is extern'd.
const hal_IDigIn_vtable hal_DigInputFromShiftReg__hal_IDigIn_vtable_imp = {
    .read_input = (bool (*)(void * self))hal_DigInputFromShiftReg_read_input,
};

