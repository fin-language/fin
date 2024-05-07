using finlang;

namespace board;

public interface ISweepLeds
{
    u8 get_count();
    void set_led(u8 index, bool state);
}
