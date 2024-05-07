using finlang;

namespace hal;

[ffi]
public class Spi : FinObj, ISpi
{
    public virtual void write_array(c_array<u8> data, u8 data_length)
    {
        throw new System.NotImplementedException();
    }

    public virtual void read_array(c_array<u8> data, u8 data_length)
    {
        throw new System.NotImplementedException();
    }

    public virtual void read_write_array(c_array<u8> write_data, c_array<u8> read_data, u8 data_length)
    {
        throw new System.NotImplementedException();
    }

    public virtual void tx_byte(u8 tx_byte)
    {
        throw new System.NotImplementedException();
    }

    public virtual u8 rx_byte()
    {
        throw new System.NotImplementedException();
    }

    public virtual u8 rx_tx_byte(u8 tx_byte)
    {
        throw new System.NotImplementedException();
    }

    public virtual void rx_array(c_array<u8> data, u8 data_length)
    {
        throw new System.NotImplementedException();
    }

    public virtual void tx_array(c_array<u8> data, u8 data_length)
    {
        throw new System.NotImplementedException();
    }

    public virtual void rx_tx_array(c_array<u8> tx_data, c_array<u8> rx_data, u8 data_length)
    {
        throw new System.NotImplementedException();
    }
}