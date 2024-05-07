using finlang;

namespace hal;

/// <summary>
/// Implementing classes may choose to ignore either the tx or rx data.
/// A Parallel In Serial Out (PISO) shift register would ignore the rx data.
/// </summary>
public interface IShiftDataUser
{
    /// <summary>
    /// Get the data to be shifted out to the shift register.
    /// </summary>
    /// <returns></returns>
    u8 get_tx_data();

    /// <summary>
    /// Set the data to be shifted out to the shift register.
    /// </summary>
    /// <param name="data"></param>
    void set_tx_data(u8 data);

    /// <summary>
    /// Get the data that was shifted in from the shift register.
    /// </summary>
    /// <returns></returns>
    u8 get_rx_data();
}
