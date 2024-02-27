using finlang;
using hal;

namespace fakes;

[simonly]
public class FakeGpio : FinObj, IGpio
{
    public bool _pullup_enabled;
    public bool _pulldown_enabled;
    public bool _state;

    public virtual bool enable_pulldown()
    {
        _pulldown_enabled = true;
        return true;
    }

    public virtual bool enable_pullup()
    {
        _pulldown_enabled = true;
        return true;
    }

    public virtual bool read_state()
    {
        return _state;
    }

    public virtual void set_state(bool state)
    {
        _state = state;
    }

    public virtual void toggle()
    {
        _state = !_state;
    }
}
