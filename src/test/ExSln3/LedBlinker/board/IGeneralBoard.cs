using hal;

namespace board;

public interface IGeneralBoard
{
    DigOutArray get_sweep_leds();

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

    DigInArray get_settings_switches();
}
