using finlang;
namespace app;

/// <summary>
/// This is basically a fake implementation of ILed.
/// </summary>
[ffi]
public class Led : FinObj, ILed
{
    public bool state;

    public void set_state(bool state)
    {
        this.state = state;
    }
}
