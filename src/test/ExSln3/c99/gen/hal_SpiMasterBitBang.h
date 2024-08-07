// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `hal.SpiMasterBitBang` type.
// Source file: `LedBlinker/hal/shift/SpiMasterBitBang.cs` (relative to C# solution).
// MD5 hash of source file: d69e0fbc2bbf1e09cdd1f75d54a5ead5.

#pragma once

#include "hal_IDigOut.h"
#include "hal_IDigIn.h"
#include <stdint.h>
#include "hal_ISpi.h"



typedef struct hal_SpiMasterBitBang hal_SpiMasterBitBang;
struct hal_SpiMasterBitBang
{
    hal_IDigOut * _clock;
    hal_IDigOut * _mosi;
    hal_IDigIn * _miso;
    hal_IDelayObj * _delay_obj;
};


void hal_SpiMasterBitBang_ctor(hal_SpiMasterBitBang * self, hal_IDigOut * clock, hal_IDigOut * mosi, hal_IDigIn * miso, hal_IDelayObj * delay_obj);

void hal_SpiMasterBitBang_rx_array(hal_SpiMasterBitBang * self, uint8_t * data, uint8_t data_length);

uint8_t hal_SpiMasterBitBang_rx_byte(hal_SpiMasterBitBang * self);

void hal_SpiMasterBitBang_rx_tx_array(hal_SpiMasterBitBang * self, uint8_t * tx_data, uint8_t * rx_data, uint8_t data_length);

void PRIVATE_hal_SpiMasterBitBang__delay(hal_SpiMasterBitBang * self);

void PRIVATE_hal_SpiMasterBitBang__clock_high(hal_SpiMasterBitBang * self);

void PRIVATE_hal_SpiMasterBitBang__clock_low(hal_SpiMasterBitBang * self);

uint8_t hal_SpiMasterBitBang_rx_tx_byte(hal_SpiMasterBitBang * self, uint8_t tx_byte);

void hal_SpiMasterBitBang_rx_tx_bit_lsb(hal_SpiMasterBitBang * self, uint8_t * tx_byte, uint8_t * read_byte);

void hal_SpiMasterBitBang_tx_array(hal_SpiMasterBitBang * self, uint8_t * data, uint8_t data_length);

void hal_SpiMasterBitBang_tx_byte(hal_SpiMasterBitBang * self, uint8_t tx_byte);

// vtable is extern to allow const initializations
extern const hal_ISpi_vtable hal_SpiMasterBitBang__hal_ISpi_vtable_imp;

// Up conversion from hal_SpiMasterBitBang to hal_ISpi interface
#define M_hal_SpiMasterBitBang__to__hal_ISpi(self_arg)    (hal_ISpi){ .obj = self_arg, .obj_vtable = (const hal_ISpi_vtable*)(&hal_SpiMasterBitBang__hal_ISpi_vtable_imp.tx_byte) }
