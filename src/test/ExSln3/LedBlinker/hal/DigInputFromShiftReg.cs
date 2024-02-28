using finlang;

namespace hal;

/// <summary>
/// MAY NOT THREAD SAFE
/// </summary>
public class DigInputFromShiftReg : FinObj, IDigIn
{
    public IShiftDataUser _shift_data;
    public u8 _bit_mask;

    public DigInputFromShiftReg(IShiftDataUser shift_data, u8 bit_mask)
    {
        _shift_data = shift_data;
        _bit_mask = bit_mask;
    }

    public bool read_input()
    {
        return (_shift_data.get_rx_data() & _bit_mask) == 1;
    }
}