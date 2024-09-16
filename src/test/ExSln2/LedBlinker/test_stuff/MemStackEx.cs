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
        public i32 speed;

        public Bike2(i32 speed)
        {
            this.speed = speed;
        }
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

    // Not supported yet
    //public static i32 calc_stuff_2(i32 b1_speed, i32 b2_speed)
    //{
    //    Bike2 b1 = mem.stack(new Bike2(b1_speed)), b2 = mem.stack(new Bike2(b2_speed));
    //            finlang.Transpiler.TranspilerException: mem.stack() can only be used to declare one variable for now
    //            File: MemStackEx.cs, Line: 35, Char: 15
    //            Code: `b1 = mem.stack(new Bike2(b1_speed))`
    //    return b1.speed + b2.speed;
    //}
}
