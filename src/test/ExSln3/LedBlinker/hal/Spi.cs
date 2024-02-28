using finlang;

namespace hal;

[ffi]
public class Spi : FinObj, ISpi
{
    public virtual void write(c_array<u8> data, u8 data_length)
    {
        throw new System.NotImplementedException();
    }

    public virtual void read(c_array<u8> data, u8 data_length)
    {
        throw new System.NotImplementedException();
    }

    public virtual void write_read(c_array<u8> write_data, c_array<u8> read_data, u8 data_length)
    {
        throw new System.NotImplementedException();
    }
}