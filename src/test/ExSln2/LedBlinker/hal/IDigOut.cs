namespace hal;

using finlang;

public interface IDigOut : IFinObj
{
    void set_state(bool state);
    void toggle();
}
