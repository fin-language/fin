// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang v0.3.0-alpha generated this file for C# `app.Main` type.
// Source file: `LedBlinker/app/Main.cs` (relative to C# solution).
// MD5 hash of source file: 5f94fb6a6e1ee5309e7be6c04f67702f.

#pragma once

#include <stdint.h>



typedef struct app_Main app_Main;
struct app_Main
{
    board_IGeneralBoard * _board;
};

//public readonly LedBouncer _led_bouncer;

void app_Main_ctor(app_Main * self, board_IGeneralBoard * board);

void app_Main_step(app_Main * self, uint32_t cur_time_ms);
