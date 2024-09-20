using finlang;

// test stuff
namespace ts_;


public class MemFieldEx : FinObj
{
    public class Bike : FinObj
    {
        public i32 speed;
    }

    [mem] Bike bike = mem.init(new Bike());

    public MemFieldEx()
    {
        bike.speed = 5;
    }

    /// <summary>
    /// mem field should decay to a pointer when passed to a function
    /// https://github.com/fin-language/fin/issues/90
    /// </summary>
    public i32 use_mem_field_as_arg()
    {
        return calc_bike_stuff(bike);
    }

    /// <summary>
    ///  mem field should decay to a pointer on assignment
    /// https://github.com/fin-language/fin/issues/90
    /// </summary>
    public i32 assign_mem_field_to_ptr()
    {
        Bike temp = bike;
        return calc_bike_stuff(temp);
    }

    static i32 calc_bike_stuff(Bike b1)
    {
        return b1.speed * 7;
    }
}
