namespace hal;

using finlang;

public interface IDigIn : IFinObj
{
    /// <summary>
    /// Reads the state of the digital input.
    /// </summary>
    /// <returns></returns>
    bool read_input();
}
