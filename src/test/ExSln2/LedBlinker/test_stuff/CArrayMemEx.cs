using finlang;

// test stuff
namespace ts;

public class CArrayMemEx : FinObj
{
    public class Bike : FinObj
    {
        public u8 speed = 0;
    }

    c_array_mem<Bike> my_bikes;

    public CArrayMemEx(c_array_mem<Bike> my_bikes)
    {
        this.my_bikes = my_bikes;
    }

    public static u16 sum_c_array(c_array_mem<Bike> bikes, u8 length)
    {
        u16 sum = 0;
        for (u8 i = 0; i < length; i++)
        {
            sum += bikes.unsafe_get(i).speed;
        }
        return sum;
    }
}
