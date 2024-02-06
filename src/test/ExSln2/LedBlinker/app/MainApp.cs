using hal;
using finlang;

namespace app;

public class MainApp : FinObj
{
    u16 period_ms; // keep without underscore so that `this.` is required in constructor
    u32 _toggle_at_ms;
    readonly Led _redLed;

    public MainApp(Led redLed, u16 period_ms)
    {
        _redLed = redLed;
        this.period_ms = period_ms;
    }

    public void step(u32 ms_time)
    {
        math.unsafe_mode();

        if (ms_time >= _toggle_at_ms)
        {
            // comment out the following line and it all works fine.
            _redLed.toggle();   // this causes really weird Roslyn errors https://github.com/fin-language/fin/issues/22
            _toggle_at_ms = (ms_time.u64 + period_ms).wrap_u32; // won't need when have a wrapping add
        }
    }

    public u32 transpilation_test_stuff()
    {
        math.unsafe_mode();
        Helper.calc_stuff(3, 77);
        u8 a = u8.from(55);
        u8 b;
        i32 i32 = 55;
        b = i32.narrow_to_u8();
        b = (a + i32).narrow_to_u8();

        return _toggle_at_ms.u32_ + 20; //u32_ is self-declaration
    }
}
