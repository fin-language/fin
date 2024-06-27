using finlang;
using FluentAssertions;
using hal;
using NSubstitute;

namespace Tests;

public class SpiMasterBitBangTest
{
    private IDigOut clock;
    private IDigOut mosi;
    private IDigIn miso;
    private IDelayObj delayObject;
    private SpiMasterBitBang spi;

    public SpiMasterBitBangTest()
    {
        clock = Substitute.For<IDigOut>();
        mosi = Substitute.For<IDigOut>();
        miso = Substitute.For<IDigIn>();
        delayObject = Substitute.For<IDelayObj>();
        spi = new SpiMasterBitBang(clock, mosi, miso, delayObject);
    }

    [Fact]
    public void RxTxBitLsb()
    {
        u8 tx_byte = 0b1010_0110;
        u8 rx_byte = 0;

        // test first shift
        {
            // fake hardware will shift out '1'
            miso.read_input().Returns(true);

            spi.rx_tx_bit_lsb(ref tx_byte, ref rx_byte);

            Received.InOrder(() =>
            {
                miso.read_input();
                mosi.set_output_state(false); // shift out '0'
                delayObject.delay();
                clock.set_output_state(true);
                delayObject.delay();
                clock.set_output_state(false);
            });

            rx_byte.Should().Be(0b1000_0000);
            ClearMockCallHistory();
        }

        // test next shift
        {
            // fake hardware will shift out '0'
            miso.read_input().Returns(false);

            spi.rx_tx_bit_lsb(ref tx_byte, ref rx_byte);

            Received.InOrder(() =>
            {
                miso.read_input();
                mosi.set_output_state(true); // shift out '1'
                delayObject.delay();
                clock.set_output_state(true);
                delayObject.delay();
                clock.set_output_state(false);
            });

            rx_byte.Should().Be(0b0100_0000);
            ClearMockCallHistory();
        }
    }

    [Fact]
    public void RxTxByteLsb()
    {
        u8 tx_byte = 0b1010_0010;
        u8 rx_byte;

        // have fake hardware shift out 0b1110_0101
        miso.read_input().Returns(true, false, true, false, false, true, true, true);

        rx_byte = spi.rx_tx_byte(tx_byte);

        rx_byte.Should().Be(0b1110_0101);
        clock.Received(1 + 8).set_output_state(false); // +1 for the initial low clock
        clock.Received(8).set_output_state(true);

        Received.InOrder(() =>
        {
            // should tx 0 1 0 0 0 1 0 1
            mosi.set_output_state(false);
            mosi.set_output_state(true);
            mosi.set_output_state(false);
            mosi.set_output_state(false);
            //
            mosi.set_output_state(false);
            mosi.set_output_state(true);
            mosi.set_output_state(false);
            mosi.set_output_state(true);
        });
    }

    private void ClearMockCallHistory()
    {
        clock.ClearReceivedCalls();
        mosi.ClearReceivedCalls();
        miso.ClearReceivedCalls();
        delayObject.ClearReceivedCalls();
    }
}