using hal;
using finlang;

namespace app;

public class Main : FinObj
{
    public u16 period_ms; // keep without underscore so that `this.` is required in constructor
    public u32 _toggle_at_ms;

    public Main(u16 period_ms)
    {
        this.period_ms = period_ms;
    }
    
    /// <summary>
    /// This is the main loop of the application.
    /// </summary>
    public void step(u32 ms_time)
    {
        math.unsafe_mode();

        if (ms_time >= _toggle_at_ms) // this isn't rollover safe :P
        {
        }
    }
}
