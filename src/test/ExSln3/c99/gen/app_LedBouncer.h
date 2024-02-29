// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang v0.2.6-alpha generated this file for C# `app.LedBouncer` type.
// Source file: `LedBlinker\app\LedBouncer.cs` (relative to C# solution).
// MD5 hash of source file: 948f91f1e23090b699300a88ed830e1c.

#pragma once

#include <stdint.h>
#include <stdint.h>
#include <stdint.h>
#include <stdint.h>



typedef struct app_LedBouncer app_LedBouncer;
struct app_LedBouncer
{
    board_IGeneralBoard * _board;
    int16_t _ms_per_frame = 100;
    int16_t _ms_before_next_frame;
    uint32_t _last_time_ms = 0;
    uint8_t _led_index = 0;
    int8_t _led_direction = 1; 
};


void app_LedBouncer_ctor(app_LedBouncer * self, board_IGeneralBoard * board);

void PRIVATE_app_LedBouncer_reset_frame_time_left(app_LedBouncer * self);


/// <summary>
/// This is the main loop of the application.
/// </summary>
void app_LedBouncer_step(app_LedBouncer * self, uint32_t cur_time_ms);

void PRIVATE_app_LedBouncer_show_next_frame(app_LedBouncer * self);

uint32_t PRIVATE_app_LedBouncer_calc_ms_since_last_step(uint32_t cur_time_ms, uint32_t last_time_ms);