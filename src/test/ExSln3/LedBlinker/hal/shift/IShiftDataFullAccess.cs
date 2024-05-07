using finlang;

namespace hal;

public interface IShiftDataFullAccess : IShiftDataUser
{
    /// <summary>
    /// This should typically be used by a shift register manager to set the data that was received from the shift register.
    /// </summary>
    /// <param name="data"></param>
    void set_rx_data(u8 data);
}
