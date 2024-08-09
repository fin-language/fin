using finlang;
namespace app;

public interface ILed : IFinObj
{
    void set_state(bool on);
}
