using finlang;
using hal;

namespace board.tang;

/// <summary>
/// This board is based on a blue Arduino Uno board. "Blue Tang".
/// Rev 1 has 2 output shift registers on SPI and 1 input shift register (bit banged).
/// </summary>
public class TangRev1 : FinObj, IGeneralBoard
{


    public DigInArray get_settings_switches()
    {
        throw new System.NotImplementedException();
    }

    public IDigIn get_start_button()
    {
        throw new System.NotImplementedException();
    }

    public DigOutArray get_sweep_leds()
    {
        throw new System.NotImplementedException();
    }

    public u32 get_time_ms()
    {
        throw new System.NotImplementedException();
    }

    public IDigIn get_trap_button()
    {
        throw new System.NotImplementedException();
    }

    public void step()
    {
        throw new System.NotImplementedException();
    }
}
