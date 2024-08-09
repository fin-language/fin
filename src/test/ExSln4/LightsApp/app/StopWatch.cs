using finlang;
namespace app;

public class StopWatch : FinObj
{
    public u32 _started_at_ms;
    public ITimeProvider _time_provider;

    public StopWatch(ITimeProvider time_provider)
    {
        _time_provider = time_provider;
    }

    public u32 get_elapsed_ms()
    {
        math.unsafe_mode();
        return (uint)_time_provider.get_time() - (uint)_started_at_ms;  // todo - need wrapping subtraction
    }

    public void reset()
    {
        _started_at_ms = _time_provider.get_time();
    }
}
