using finlang;

namespace App;

public class Counter : FinObj
{
    u32 _count;

    public void increment()
    {
        _count += 1;
    }
}
