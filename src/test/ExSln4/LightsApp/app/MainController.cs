using finlang;
namespace app;

public class MainController : FinObj
{
    public bool _state;
    public ILed _led1;
    public ILed _led2;
    [mem] public StopWatch _stopwatch;

    public MainController(ITimeProvider time_provider, ILed led1, ILed led2)
    {
        _stopwatch = mem.init(new StopWatch(time_provider));
        _led1 = led1;
        _led2 = led2;

        _state = true;
        _set_leds();
    }

    public void update()
    {
        if (_stopwatch.get_elapsed_ms() >= 1000)
        {
            _stopwatch.reset();
            _state = !_state;
            _set_leds();
        }
    }

    private void _set_leds()
    {
        _led1.set_state(_state);
        _led2.set_state(!_state);
    }
}
