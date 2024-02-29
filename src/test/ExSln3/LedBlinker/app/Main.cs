using finlang;
using board;

namespace app;

public class Main : FinObj
{
    public readonly IGeneralBoard _board;
    public readonly LedBouncer _led_bouncer;

    public Main(IGeneralBoard board)
    {
        this._board = board;
        _led_bouncer = new LedBouncer(board);
    }

    public void step(u32 cur_time_ms)
    {
        _led_bouncer.step(cur_time_ms);
    }
}
