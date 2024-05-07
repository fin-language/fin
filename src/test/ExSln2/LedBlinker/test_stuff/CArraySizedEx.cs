﻿using finlang;

namespace hal;

public class CArraySizedEx : FinObj
{
    // Some comment
    [mem] c_array_sized<u8> data = mem.init(new c_array_sized<u8>(5));
    
    public CArraySizedEx()
    {

    }

    public u16 sum()
    {
        u16 sum = 0;
        for (u8 i = 0; i < data.length; i++)
        {
            sum += data.unsafe_get(i);
        }
        return sum;
    }

    public u16 sum2()
    {
        return sum_c_array(data, data.length.narrow_to_u8());
    }

    public u8 get_element(u8 index)
    {
        return data.unsafe_get(index);
    }

    public static u16 sum_c_array(c_array<u8> arr, u8 length)
    {
        u16 sum = 0;
        for (u8 i = 0; i < length; i++)
        {
            sum += arr.unsafe_get(i);
        }
        return sum;
    }
}
