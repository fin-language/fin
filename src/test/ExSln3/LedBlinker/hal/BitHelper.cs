using finlang;

namespace hal;

public class BitHelper : FinObj
{
    public static bool is_bit_set(u8 data, u8 bit_mask)
    {
        return (data & bit_mask) > 0;
    }

    public static u8 set_bit(u8 data, u8 bit_mask)
    {
        return (u8)(data | bit_mask);
    }

    public static u8 clear_bit(u8 data, u8 bit_mask)
    {
        return (u8)(data & ~bit_mask);
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
