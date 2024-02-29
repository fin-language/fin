using finlang;

namespace hal;

public class SpiMasterBitBang : FinObj, ISpi
{
    public IDigOut _clock;
    public IDigOut _mosi;
    public IDigIn _miso;
    public IDelayObj _delay_obj;

    public SpiMasterBitBang(IDigOut clock, IDigOut mosi, IDigIn miso, IDelayObj delay_obj)
    {
        _clock = clock;
        _mosi = mosi;
        _miso = miso;
        _delay_obj = delay_obj;
    }

    public void rx_array(c_array<u8> data, u8 data_length)
    {
        SimOnly.ThrowNotImplemented();
    }

    public u8 rx_byte()
    {
        SimOnly.ThrowNotImplemented();
        return 0;
    }

    public void rx_tx_array(c_array<u8> tx_data, c_array<u8> rx_data, u8 data_length)
    {
        SimOnly.ThrowNotImplemented();
    }

    public void _delay()
    {
        _delay_obj.delay();
    }

    public void _clock_high()
    {
        _clock.set_output_state(true);
    }

    public void _clock_low()
    {
        _clock.set_output_state(false);
    }

    public u8 rx_tx_byte(u8 tx_byte)
    {
        u8 read_byte = 0;
        _clock_low(); // just to make sure

        for (u8 i = 0; i < 8; i++)
        {
            rx_tx_bit_lsb(ref tx_byte, ref read_byte);
        }

        return read_byte;
    }

    public void rx_tx_bit_lsb(ref u8 tx_byte, ref u8 read_byte)
    {
        math.unsafe_mode();
        
        read_byte >>= 1;
        if (_miso.read_input())
            read_byte |= 0b1000_0000;

        _mosi.set_output_state((tx_byte & 0x01) != 0);
        _delay();
        _clock_high();
        _delay();
        _clock_low();

        tx_byte >>= 1;
    }

    public void tx_array(c_array<u8> data, u8 data_length)
    {
        SimOnly.ThrowNotImplemented();
    }

    public void tx_byte(u8 tx_byte)
    {
        SimOnly.ThrowNotImplemented();
    }
}
