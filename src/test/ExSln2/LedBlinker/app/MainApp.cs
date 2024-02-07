using hal;
using finlang;

namespace app;

public class MainApp : FinObj
{
    public u16 period_ms; // keep without underscore so that `this.` is required in constructor
    public u32 _toggle_at_ms;
    public readonly Led _redLed;

    public MainApp(Led redLed, u16 period_ms)
    {
        _redLed = redLed;
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
            _redLed.toggle();
            _toggle_at_ms = (ms_time.u64 + period_ms).wrap_u32; // this will be nicer when we have a wrapping add method
        }
    }

    public u32 transpilation_test_stuff()
    {
        math.unsafe_mode();
        Helper.calc_stuff(3, 77);
        u8 a = u8.from(55);
        u8 b;
        i32 my_i32 = 55;
        b = my_i32.narrow_to_u8();
        b = (a + my_i32).narrow_to_u8();
        b = (u8)my_i32;
        b = u8.narrow_from(my_i32);

        return _toggle_at_ms.u32_ + 20; //u32_ is self-declaration
    }
}
