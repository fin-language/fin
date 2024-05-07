using finlang;

namespace hal;

/// <summary>
/// When you want to use a variable as a digital input or buffer some other digital input.
/// </summary>
public class DigInputFromVar : FinObj, IDigIn
{
    /// <summary>
    /// This is the last state of the digital input when read by the SPI manager.
    /// </summary>
    public bool last_state;

    public bool read_input()
    {
        return last_state;
    }
}
