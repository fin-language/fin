using finlang;

namespace app;

public class Counter : FinObj
{
    c_array<u8> _counts;    // `uint8_t * _counts;` in C
    u32 _count;

    public Counter(c_array<u8> counts)
    {
        _counts = counts;
    }

    public void increment()
    {
        _count += 1;
    }
}
