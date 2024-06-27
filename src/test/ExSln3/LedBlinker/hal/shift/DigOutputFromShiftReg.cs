using finlang;

namespace hal;

/// <summary>
/// NOT THREAD SAFE!
/// </summary>
public class DigOutputFromShiftReg : FinObj, IDigOut
{
    public TxShiftData _shift_data;
    public u8 _bit_mask;

    /// <summary>
    /// When we support constexpr, we should pass bit index instead of bit mask.
    /// </summary>
    /// <param name="shift_data"></param>
    /// <param name="bit_mask"></param>
    /// <exception cref="System.ArgumentException"></exception>
    public DigOutputFromShiftReg(TxShiftData shift_data, u8 bit_mask)
    {
        _shift_data = shift_data;
        _bit_mask = bit_mask;

        BitHelper.SimOnlyEnsureSingleBitInMask(bit_mask);
    }

    public bool get_output_state()
    {
        return (_shift_data.get_tx_data() & _bit_mask) > 0;
    }

    /// <summary>
    /// NOT THREAD SAFE!!!
    /// </summary>
    /// <param name="state"></param>
    public void set_output_state(bool state)
    {
        var data = _shift_data.get_tx_data();
        if (state)
        {
            data |= _bit_mask;
        }
        else
        {
            data &= ~_bit_mask;
        }
        _shift_data.set_tx_data(data);
    }

    public void toggle()
    {
        if (get_output_state())
        {
            set_output_state(false);
        }
        else
        {
            set_output_state(true);
        }
    }
}