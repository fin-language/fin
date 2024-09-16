using finlang;
using static finlang.mem;

namespace ts; // test stuff



/// <summary>
/// https://github.com/fin-language/fin/issues/39
/// </summary>
public class MemStackEx : FinObj
{
    public class Bike1 : FinObj
    {
        public i32 speed;
    }

    public class Bike2 : FinObj
    {
        public u8 id;
        public i32 speed;

        public Bike2(i32 speed)
        {
            this.speed = speed;
        }

        public Bike2 set_speed(i32 speed)
        {
            this.speed = speed;
            return this;
        }

        public Bike2 set_id(u8 id)
        {
            this.id = id;
            return this;
        }
    }

    public static i32 chain_stack_creation()
    {
        return mem.stack(new Bike2(5)).set_speed(10).set_id(1).speed;
    }

    public static i32 calc_stuff_compound_literals_func_args()
    {
        return calc_stuff(mem.stack(new Bike2(5)), mem.stack(new Bike2(1)));
    }

    private static i32 calc_stuff(Bike2 b1, Bike2 b2)
    {
        return b1.speed + b2.speed;
    }

    public static i32 calc_stuff(i32 b1_speed, i32 b2_speed)
    {
        // leading comment
        Bike1 b1 = mem.stack(new Bike1()); // trailing comment
        b1.speed = b1_speed;

        // below uses: `using static finlang.mem;`
        Bike2 b2 = stack(new Bike2(b2_speed)); // implicit mem
        return b1.speed + b2.speed;
    }

    public static i32 calc_stuff_mult_vars_on_same_line(i32 b1_speed, i32 b2_speed)
    {
       Bike2 b1 = mem.stack(new Bike2(b1_speed)), b2 = mem.stack(new Bike2(b2_speed));
       return b1.speed + b2.speed;
    }
}
