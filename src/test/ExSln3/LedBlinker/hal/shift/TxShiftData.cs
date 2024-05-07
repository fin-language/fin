using finlang;

namespace hal;

public class TxShiftData : FinObj, IShiftDataFullAccess
{
    public u8 tx_data;

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
        return 0;
    }

    public void set_rx_data(u8 data)
    {
        FinC.ignore_unused(data);
    }
}
