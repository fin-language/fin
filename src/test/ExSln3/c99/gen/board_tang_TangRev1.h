// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang v0.3.0-alpha generated this file for C# `board.tang.TangRev1` type.
// Source file: `LedBlinker/board/tang/TangRev1.cs` (relative to C# solution).
// MD5 hash of source file: 3204eee1f64a653c7753e45a1a690641.

#pragma once

#include "board_tang_TangRev1_port_implementation.h" // You need to provide this
#include "mcu/avr8/Avr8Gpio.h"
#include "hal_IDigOut.h"
#include "hal_IDigIn.h"
#include <stdint.h>



/// <summary>
/// This board is based on a blue Arduino Uno board. "Blue Tang".
/// </summary>
typedef struct board_tang_TangRev1 board_tang_TangRev1;
struct board_tang_TangRev1
{
    mcu_avr8_Avr8Gpio _start_button ; // Arduino pin A0. RED button.
    mcu_avr8_Avr8Gpio _trap_button ;  // Arduino pin A1
    mcu_avr8_Avr8Gpio _main_led ; // Arduino pin 13. The main BUILTIN LED.
};


void board_tang_TangRev1_ctor(board_tang_TangRev1 * self);

hal_IDigOut * board_tang_TangRev1_get_main_led(board_tang_TangRev1 * self);

hal_IDigIn * board_tang_TangRev1_get_start_button(board_tang_TangRev1 * self);

hal_IDigIn * board_tang_TangRev1_get_trap_button(board_tang_TangRev1 * self);

// FFI function. User code must provide the implementation
uint32_t board_tang_TangRev1_get_time_ms(board_tang_TangRev1 * self);

void board_tang_TangRev1_step(board_tang_TangRev1 * self);

// vtable is extern to allow const initializations
extern const board_IGeneralBoard_vtable board_IGeneralBoard_vtable_imp;

// Up conversion from board_tang_TangRev1 to board_IGeneralBoard interface
#define M_board_tang_TangRev1__to__board_IGeneralBoard(self_arg)    (board_IGeneralBoard){ .obj = self_arg, .obj_vtable = (const board_IGeneralBoard_vtable*)(&board_IGeneralBoard_vtable_imp.get_trap_button) }
