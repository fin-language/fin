using finlang;

namespace app;

public class Counter : FinObj
{
    u32 _count;

    public void increment()
    {
        _count += 1;
    }
}
