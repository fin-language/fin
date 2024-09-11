using finlang;

namespace ts; // test stuff

public class FuncPtrEx2 : FinObj
{
    public delegate i32 FuncPtr(i32 a, i32 b);
    public FuncPtr func = add;

    public class Bike : FinObj
    {
        public i32 speed;
    }

    /// <summary>
    /// This is a delegate that takes a Bike and returns an i32.
    /// </summary>
    public delegate i32 BikeSpeedFunc(Bike bike);

    /// <summary>
    /// This is a delegate that takes a Bike and returns a Bike. Maybe for method chaining, but really just for testing :)
    /// </summary>
    public delegate Bike BikeBikeFunc(Bike bike);

    /// <summary>
    /// This is a delegate that takes a Bike and returns a Bike. Maybe for method chaining, but really just for testing :)
    /// </summary>
    public delegate c_array<Bike> BikeArrayFunc(c_array<Bike> bikes, u8 length);

    public static i32 add(i32 a, i32 b)
    {
        return a + b;
    }

    public static i32 sub(i32 a, i32 b)
    {
        return a - b;
    }

    public void use_sub()
    {
        func = sub;
    }

    public void set(FuncPtr f)
    {
        func = f;
    }

    public i32 Run(i32 a, i32 b)
    {
        return func(a, b);
    }
}