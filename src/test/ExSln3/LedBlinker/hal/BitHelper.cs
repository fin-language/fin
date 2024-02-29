using finlang;

namespace hal;

public class BitHelper : FinObj
{
    public static void ref_set_bit(ref u8 data, u8 bit_index)
    {
        math.unsafe_mode();
        data |= (u8)(1 << bit_index);
    }

    public static void ref_clear_bit(ref u8 data, u8 bit_index)
    {
        math.unsafe_mode();
        data &= ~((u8)1 << bit_index);
    }

    public static void ref_set_bit(ref u8 data, u8 bit_index, bool state)
    {
        if (state)
        {
            ref_set_bit(ref data, bit_index);
        }
        else
        {
            ref_clear_bit(ref data, bit_index);
        }
    }

    [simonly]
    public static void SimOnlyEnsureSingleBitInMask(u8 bit_mask)
    {
        math.unsafe_mode();

        if (bit_mask == 0)
        {
            throw new System.ArgumentException("bit_mask cannot be 0");
        }
        if ((bit_mask & (bit_mask - 1)) != 0)
        {
            throw new System.ArgumentException("bit_mask must have only one bit set");
        }
    }
}
