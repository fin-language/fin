using finlang;
using hal;
using mcu.avr8;

namespace board.tang;

public class ShiftRegChain : FinObj
{
    [mem] public required ShiftManager shift_manager;
    [mem] public required SpiMasterBitBang spi;
    [mem] public required Avr8Gpio clock;
    [mem] public required Avr8Gpio mosi;
    [mem] public required Avr8Gpio miso;
}

public class SweepLeds : FinObj, ISweepLeds
{
    ShiftRegChain shift_reg;
    Avr8Gpio storage_pin;
    TxShiftData data_0;
    TxShiftData data_1;

    c_array<IShiftDataFullAccess> data_array;

    public SweepLeds()
    {
        shift_reg = new ShiftRegChain();
    }

    public u8 get_count()
    {
        return 2 * 8;
    }

    public void set_led(u8 index, bool state)
    {
        if (index < 8)
        {
            BitHelper.ref_set_bit(ref data_0.tx_data, index, state);
        }
        else
        {
            BitHelper.ref_set_bit(ref data_1.tx_data, index - 8, state);
        }
    }
}

public class InputShiftReg : FinObj
{
    ShiftRegChain shift_reg;
    Avr8Gpio shift_or_load_pin;
    RxShiftData data_0;
    c_array<IShiftDataFullAccess> data_array;
}

public class SettingsSwitches : FinObj
{
    DigInputFromShiftReg switches_0;
    DigInputFromShiftReg switches_1;
    DigInputFromShiftReg switches_2;
    Avr8Gpio switches_3;
    DigInArray switches;
}

/// <summary>
/// This board is based on a blue Arduino Uno board. "Blue Tang".
/// Rev 1 has:
///     - 2 output shift registers chained on bit banged SPI1.
///     - 1 input shifters on bit banged SPI2.
/// </summary>
public class TangRev1 : FinObj, IGeneralBoard
{
    //InputShiftReg input_shift_reg;
    //OutputShiftReg output_shift_reg;

    [mem] Avr8Gpio _start_button = mem.init(new Avr8Gpio() { _port = 0, _pin = 2 });
    [mem] Avr8Gpio _trap_button;

    //DigOutArray _sweep_leds;

    public TangRev1()
    {
        _trap_button = mem.init(new Avr8Gpio() { _port = 0, _pin = 3 });
    }

    public DigInArray get_settings_switches()
    {
        throw new System.NotImplementedException();
    }

    public IDigIn get_start_button()
    {
        return _start_button;
    }

    public IDigIn get_trap_button()
    {
        return _trap_button;
    }

    public DigOutArray get_sweep_leds()
    {
        throw new System.NotImplementedException();
    }

    public u32 get_time_ms()
    {
        throw new System.NotImplementedException();
    }

    public void step()
    {
        throw new System.NotImplementedException();
    }
}
