using finlang;

namespace hal;

public class FuncPtrEx : FinObj
{
    [mem] public FuncIntIntInt func = mem.init(new FuncIntIntInt(Add));

    public FuncPtrEx()
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
        return func.call(a, b);
    }
}
