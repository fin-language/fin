using finlang;

namespace hal;

/// <summary>
/// Returns i32, takes two ints. We don't have generics yet.
/// </summary>
[ffi]
public class FuncIntIntInt : FinObj
{
    public delegate i32 FuncPtr(i32 a, i32 b);

    public FuncPtr? ptr;

    public FuncIntIntInt(FuncPtr func_ptr)
    {
        this.ptr = func_ptr;
    }

    public void set(FuncPtr func_ptr)
    {
        this.ptr = func_ptr;
    }

    public i32 call(i32 a, i32 b)
    {
        if (ptr == null)
        {
            return 0;
        }

        return ptr(a, b);
    }

    //public static implicit operator FuncIntIntInt(FuncPtr func_ptr)
    //{
    //    return new FuncIntIntInt(func_ptr);
    //}
}
