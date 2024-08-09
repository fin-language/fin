using finlang;
namespace app;

/// <summary>
/// This is basically a fake implementation of ITimeProvider.
/// </summary>
[ffi]
public class TimeProvider : FinObj, ITimeProvider
{
    public u32 time_ms;

    public u32 get_time()
    {
        return time_ms;
    }
}
