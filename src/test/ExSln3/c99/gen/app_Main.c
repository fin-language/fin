// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `app.Main` type.
// Source file: `LedBlinker/app/Main.cs` (relative to C# solution).
// MD5 hash of source file: 5f94fb6a6e1ee5309e7be6c04f67702f.


#include "app_Main.h"
#include "hal_IDigOut.h"
#include <stdbool.h>
#include "hal_IDigIn.h"
#include <string.h>


//public readonly LedBouncer _led_bouncer;

void app_Main_ctor(app_Main * self, board_IGeneralBoard * board)
{
    memset(self, 0, sizeof(*self));
    self->_board = board;
    //_led_bouncer = new LedBouncer(board);
}

void app_Main_step(app_Main * self, uint32_t cur_time_ms)
{
    hal_IDigOut * main_led = board_IGeneralBoard_get_main_led(self->_board);
    bool start_button_pressed = hal_IDigIn_read_input(board_IGeneralBoard_get_start_button(self->_board)) == false; // active low

    hal_IDigOut_set_output_state(main_led, start_button_pressed);

    //_led_bouncer.step(cur_time_ms);
    (void)(cur_time_ms);
}
