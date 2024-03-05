using finlang;
using hal;

namespace board;

public interface IGeneralBoard : IFinObj
{
    /// <summary>
    /// Button that player presses to trap the light beam.
    /// </summary>
    /// <returns></returns>
    IDigIn get_trap_button();

    /// <summary>
    /// Button that player presses to start the game.
    /// </summary>
    /// <returns></returns>
    IDigIn get_start_button();

    IDigOut get_main_led();

    //DigInArray get_settings_switches();
    //DigOutArray get_sweep_leds();

    u32 get_time_ms();

    void step();
}
