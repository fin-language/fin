namespace hal;

using finlang;

public interface IDigOut : IFinObj
{
    /// <summary>
    /// Sets the state of the digital output.
    /// </summary>
    /// <param name="state"></param>
    void set_output_state(bool state);

    bool get_output_state();

    void toggle();
}
