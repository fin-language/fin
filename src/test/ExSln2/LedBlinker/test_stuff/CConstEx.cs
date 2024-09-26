using finlang;

// test stuff
namespace ts;

/// <summary>
/// https://github.com/fin-language/fin/issues/92
/// </summary>
public class CConstEx : FinObj
{
    //------------------- test classes -------------------
    public class Bike : FinObj
    {
        public i32 speed;
    }

    /// <summary>
    /// Use this class when you are working with generic types like `c_array_mem<ro_Bike>` as
    /// you can't use the `c_const` attribute on generic types.
    /// </summary>
    [c_const]
    public class ro_Bike : Bike { }

    //------------------- regular class stuff -------------------

    [c_const] Bike const_bike_ptr;
    ro_Bike const_bike_ptr_2;

    public CConstEx([c_const] Bike const_bike_ptr, ro_Bike const_bike_ptr_2)
    {
        this.const_bike_ptr = const_bike_ptr;
        this.const_bike_ptr_2 = const_bike_ptr_2;
    }

    public i32 sum_owned_const_bikes()
    {
        return const_bike_ptr.speed + const_bike_ptr_2.speed;
    }

    public static i32 sum_two_const_bikes_ro(ro_Bike bike1, ro_Bike bike2)
    {
        return bike1.speed + bike2.speed;
    }

    public static i32 sum_two_const_bikes_attr([c_const] Bike bike1, [c_const] Bike bike2)
    {
        return bike1.speed + bike2.speed;
    }

    public static i32 sum_two_bikes_in(in Bike bike1, in Bike bike2)
    {
        return bike1.speed + bike2.speed;
    }

    public static i32 sum_two_const_bikes_in([c_const] in Bike bike1, in ro_Bike bike2)
    {
        return bike1.speed + bike2.speed;
    }

    public static i32 sum_two_bikes_in_mutate(in Bike bike1, in Bike bike2)
    {
        bike1.speed *= 2;
        bike2.speed *= 3;
        return bike1.speed + bike2.speed;
    }

    public static u8 sum_two_const_u8([c_const] u8 a, [c_const] u8 b)
    {
        return a + b;
    }

    public static u8 sum_two_u8_in(in u8 a, in u8 b)
    {
        return a + b;
    }

    public static i32 sum_const_bikes_array(c_array_mem<ro_Bike> bikes, u8 length)
    {
        i32 sum = 0;

        for (u8 i = 0; i < length; i++)
        {
            sum += bikes.unsafe_get(i).speed;
        }

        return sum;
    }
    
    public static i32 sum_const_bikes_array_in(in c_array_mem<ro_Bike> bikes, u8 length)
    {
        return sum_const_bikes_array(bikes, length);
    }
}
