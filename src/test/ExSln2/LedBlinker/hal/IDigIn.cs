namespace hal;

using finlang;

public interface IDigIn : IFinObj
{
    bool read_state();
}
