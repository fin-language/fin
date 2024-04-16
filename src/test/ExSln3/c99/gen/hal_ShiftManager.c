// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `hal.ShiftManager` type.
// Source file: `LedBlinker/hal/shift/ShiftManager.cs` (relative to C# solution).
// MD5 hash of source file: afcd53135145f3cec022b8e26b4f8090.


#include "hal_ShiftManager.h"
#include "hal_IDigOut.h"
#include "hal_BusyWait.h"
#include <string.h>



void hal_ShiftManager_ctor(hal_ShiftManager * self, hal_ISpi * spi, hal_IShiftDataFullAccess * * data, uint8_t data_count, hal_IGpio * piso_shift_or_load_pin, hal_IGpio * sipo_storage_pin)
{
    memset(self, 0, sizeof(*self));
    self->_spi = spi;
    self->_data = data;
    self->_data_count = data_count;
    self->_piso_shift_or_load_pin = piso_shift_or_load_pin;
    self->_sipo_storage_pin = sipo_storage_pin;
}

void PRIVATE_hal_ShiftManager__piso_load_data(hal_ShiftManager * self)
{
    if (self->_piso_shift_or_load_pin == NULL)
        return;

    // a falling edge on the shift register's shift/load pin will load the shift register
    hal_IGpio_set_output_state(self->_piso_shift_or_load_pin, false);

    // wait long enough for the shift register to load. 120ns is the worst case minimum time for the 74HC165
    hal_BusyWait_delay_120ns();

    // leave shift register in shift mode
    hal_IGpio_set_output_state(self->_piso_shift_or_load_pin, true);

    // wait long enough for the shift register to be ready to shift. 120ns is the worst case minimum time for the 74HC165
    hal_BusyWait_delay_120ns();
}

void PRIVATE_hal_ShiftManager__sipo_latch_data(hal_ShiftManager * self)
{
    if (self->_sipo_storage_pin == NULL)
        return;


    hal_BusyWait_delay_120ns();

    // a rising edge on the shift register's storage pin will store the shift register's input latches
    hal_IGpio_set_output_state(self->_sipo_storage_pin, true);

    // wait long enough for the shift register to store the data. 120ns is the worst case minimum time for the 74HC595
    hal_BusyWait_delay_120ns();

    // leave shift register in shift mode
    hal_IGpio_set_output_state(self->_sipo_storage_pin, false);

}

void hal_ShiftManager_step(hal_ShiftManager * self)
{
    PRIVATE_hal_ShiftManager__piso_load_data(self);

    for (uint8_t i = self->_data_count - 1; i >= 0; i--)
    {
        hal_IShiftDataFullAccess * shift_data = self->_data[i];
        uint8_t rx_data = hal_ISpi_rx_tx_byte(self->_spi, hal_IShiftDataFullAccess_get_tx_data(shift_data));
        hal_IShiftDataFullAccess_set_rx_data(shift_data, rx_data);
    }

    PRIVATE_hal_ShiftManager__sipo_latch_data(self);
}
