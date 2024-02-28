using finlang;

namespace hal;

public class ShiftRegDigIn : FinObj, IDigIn
{
    /// <summary>
    /// This is the last state of the digital input when read by the SPI manager.
    /// </summary>
    public bool last_state;

    public bool read_input()
    {
        return last_state;
    }
}

public class ShiftInRegManager : FinObj
{
    public c_array<ShiftRegDigIn> dig_ins;
    public u8 dig_ins_count;

    public c_array<u8> shift_reg_data;
    public u8 shift_reg_data_count;

    public IGpio shift_or_load_pin;

    public ISpi spi;

    public ShiftInRegManager(c_array<ShiftRegDigIn> dig_ins, u8 dig_ins_count, c_array<u8> shift_reg_data, u8 shift_reg_data_count, IGpio shift_or_load_pin, ISpi spi)
    {
        this.dig_ins = dig_ins;
        this.dig_ins_count = dig_ins_count;
        this.shift_reg_data = shift_reg_data;
        this.shift_reg_data_count = shift_reg_data_count;
        this.shift_or_load_pin = shift_or_load_pin;
        this.spi = spi;
    }

    public void _ic_load_data()
    {
        // a falling edge on the shift register's shift/load pin will load the shift register
        shift_or_load_pin.set_output_state(false);

        // wait long enough for the shift register to load. 120ns is the worst case minimum time for the 74HC165
        SpinDelay.wait_120ns();

        // leave shift register in shift mode
        shift_or_load_pin.set_output_state(true);

        // wait long enough for the shift register to be ready to shift. 120ns is the worst case minimum time for the 74HC165
        SpinDelay.wait_120ns();
    }

    public void step()
    {
        _ic_load_data();
        spi.read(shift_reg_data, shift_reg_data_count);
    }
}
