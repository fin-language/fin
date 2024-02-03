using finlang;
using Hal;

namespace App;

public class MainApp : FinObj
{
    u32 _toggle_at_ms;
    readonly Led _redLed;

    public MainApp(Led redLed)
    {
        _redLed = redLed;
    }

    //public void step(u32 ms_time)
    //{
    //    math.unsafe_mode();

    //    if (ms_time >= _toggle_at_ms)
    //    {
    //        _redLed.toggle();
    //        _toggle_at_ms = (ms_time.u64 + 1000).wrap_u32; // won't need when have a wrapping add
    //    }
    //}
}
