// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang v0.2.6-alpha generated this file for C# `board.tang.TangRev1` type.
// Source file: `LedBlinker\board\tang\TangBoard.cs` (relative to C# solution).
// MD5 hash of source file: 7874b2c513e6c85ae22db7a7665e7072.


#include "board_tang_TangRev1.h"




hal_DigInArray * board_tang_TangRev1_get_settings_switches(board_tang_TangRev1 * self)
{
    throw new System_NotImplementedException *();
}

hal_IDigIn * board_tang_TangRev1_get_start_button(board_tang_TangRev1 * self)
{
    throw new System_NotImplementedException *();
}

hal_DigOutArray * board_tang_TangRev1_get_sweep_leds(board_tang_TangRev1 * self)
{
    throw new System_NotImplementedException *();
}

uint32_t board_tang_TangRev1_get_time_ms(board_tang_TangRev1 * self)
{
    throw new System_NotImplementedException *();
}

hal_IDigIn * board_tang_TangRev1_get_trap_button(board_tang_TangRev1 * self)
{
    throw new System_NotImplementedException *();
}

void board_tang_TangRev1_step(board_tang_TangRev1 * self)
{
    throw new System_NotImplementedException *();
}

// virtual table implementation for IGeneralBoard. Note that this is extern'd.
const board_IGeneralBoard_vtable board_IGeneralBoard_vtable_imp = {
    .get_sweep_leds = (hal_DigOutArray (*)(void * self))board_tang_TangRev1_get_sweep_leds,
    .get_trap_button = (hal_IDigIn (*)(void * self))board_tang_TangRev1_get_trap_button,
    .get_start_button = (hal_IDigIn (*)(void * self))board_tang_TangRev1_get_start_button,
    .get_settings_switches = (hal_DigInArray (*)(void * self))board_tang_TangRev1_get_settings_switches,
    .get_time_ms = (uint32_t (*)(void * self))board_tang_TangRev1_get_time_ms,
    .step = (void (*)(void * self))board_tang_TangRev1_step,
};

