using finlang;

namespace hal;

public interface ISpi : IFinObj
{
    void read(c_array<u8> data, u8 data_length);
    void write(c_array<u8> data, u8 data_length);
    void write_read(c_array<u8> write_data, c_array<u8> read_data, u8 data_length);
}
