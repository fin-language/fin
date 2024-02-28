using finlang;

namespace hal;

public class TxRxShiftData : FinObj, IShiftDataFullAccess
{
    public u8 tx_data;
    public u8 rx_data;

    public u8 get_tx_data()
    {
        return tx_data;
    }

    public void set_tx_data(u8 data)
    {
        tx_data = data;
    }

    public u8 get_rx_data()
    {
        return rx_data;
    }

    public void set_rx_data(u8 data)
    {
        rx_data = data;
    }
}