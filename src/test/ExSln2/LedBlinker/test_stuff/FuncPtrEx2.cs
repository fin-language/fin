using finlang;

namespace hal;

public class FuncPtrEx2 : FinObj
{
    public delegate i32 FuncPtr(i32 a, i32 b);

    public FuncPtr func = Add;

    public FuncPtrEx2()
    {
        // empty constructor required for finlang right now
    }

    public static i32 Add(i32 a, i32 b)
    {
        return a + b;
    }

    public static i32 Sub(i32 a, i32 b)
    {
        return a - b;
    }

    public i32 Run(i32 a, i32 b)
    {
        return func(a, b);
    }
}