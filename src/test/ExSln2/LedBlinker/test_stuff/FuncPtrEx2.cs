using finlang;

namespace hal;

public class FuncPtrEx2 : FinObj
{
    public delegate i32 FuncPtr(i32 a, i32 b);

    public FuncPtr func = add;

    public FuncPtrEx2()
    {
        // empty constructor required for finlang right now
        // see https://github.com/fin-language/fin/issues/32
    }

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