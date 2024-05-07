using finlang;
using board;

namespace app;

public class Main : FinObj
{
    public readonly IGeneralBoard _board;
    //public readonly LedBouncer _led_bouncer;

    public Main(IGeneralBoard board)
    {
        this._board = board;
        //_led_bouncer = new LedBouncer(board);
    }

    public void step(u32 cur_time_ms)
    {
        var main_led = this._board.get_main_led();
        bool start_button_pressed = this._board.get_start_button().read_input() == false; // active low

        main_led.set_output_state(start_button_pressed);

        //_led_bouncer.step(cur_time_ms);
        FinC.ignore_unused(cur_time_ms);
    }
}
