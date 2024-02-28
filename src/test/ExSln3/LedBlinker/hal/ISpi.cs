using finlang;

namespace hal;

public interface ISpi : IFinObj
{
    void tx_byte(u8 tx_byte);
    u8 rx_byte();
    u8 rx_tx_byte(u8 tx_byte);

    void rx_array(c_array<u8> data, u8 data_length);
    void tx_array(c_array<u8> data, u8 data_length);
    void rx_tx_array(c_array<u8> tx_data, c_array<u8> rx_data, u8 data_length);
}
