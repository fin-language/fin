namespace hal;

using finlang;

public class DigOutInverter : FinObj, IDigOut
{
    public IDigOut _dig_out;

    public DigOutInverter(IDigOut dig_out)
    {
        _dig_out = dig_out;
    }

    public void set_output_state(bool state)
    {
        _dig_out.set_output_state(!state);
    }

    public void toggle()
    {
        _dig_out.toggle();
    }

    public bool get_output_state()
    {
        return !_dig_out.get_output_state();
    }
}