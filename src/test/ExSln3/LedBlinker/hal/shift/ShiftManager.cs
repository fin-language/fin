using finlang;

namespace hal;

/// <summary>
/// Allows chaining PISO and/or SIPO shift registers together to create a shift register chain.
/// </summary>
public class ShiftManager : FinObj
{
    public ISpi _spi;

    public c_array<IShiftDataFullAccess> _data;
    public u8 _data_count;

    /// <summary>
    /// Parallel In Serial Out (PISO) shift register shift or load pin. Causes load when it goes low.
    /// May be null if the shift register(s) does not have a shift or load pin.
    /// </summary>
    public IGpio? _piso_shift_or_load_pin;

    /// <summary>
    /// Serial In Parallel Out (SIPO) shift register storage pin. When this pin goes high, 
    /// the shifted in data is stored in the shift register's output latches.
    /// May be null if the shift register(s) does not have a storage pin.
    /// </summary>
    public IGpio? _sipo_storage_pin;

    public ShiftManager(ISpi spi, c_array<IShiftDataFullAccess> data, u8 data_count, IGpio? piso_shift_or_load_pin, IGpio? sipo_storage_pin)
    {
        _spi = spi;
        _data = data;
        _data_count = data_count;
        _piso_shift_or_load_pin = piso_shift_or_load_pin;
        _sipo_storage_pin = sipo_storage_pin;
    }

    public void _piso_load_data()
    {
        if (_piso_shift_or_load_pin == null)
            return;

        // a falling edge on the shift register's shift/load pin will load the shift register
        _piso_shift_or_load_pin.set_output_state(false);

        // wait long enough for the shift register to load. 120ns is the worst case minimum time for the 74HC165
        SpinDelay.wait_120ns();

        // leave shift register in shift mode
        _piso_shift_or_load_pin.set_output_state(true);

        // wait long enough for the shift register to be ready to shift. 120ns is the worst case minimum time for the 74HC165
        SpinDelay.wait_120ns();
    }

    public void _sipo_latch_data()
    {
        if (_sipo_storage_pin == null)
            return;


        SpinDelay.wait_120ns();

        // a rising edge on the shift register's storage pin will store the shift register's input latches
        _sipo_storage_pin.set_output_state(true);

        // wait long enough for the shift register to store the data. 120ns is the worst case minimum time for the 74HC595
        SpinDelay.wait_120ns();

        // leave shift register in shift mode
        _sipo_storage_pin.set_output_state(false);

    }

    public void step()
    {
        _piso_load_data();

        for (u8 i = _data_count - 1; i >= 0; i--)
        {
            IShiftDataFullAccess shift_data = _data.unsafe_get(i);
            u8 rx_data = _spi.rx_tx_byte(shift_data.get_tx_data());
            shift_data.set_rx_data(rx_data);
        }

        _sipo_latch_data();
    }
}
