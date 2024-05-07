namespace hal;

using finlang;

public class DigInInverter : FinObj, IDigIn
{
    public IDigIn _dig_in;

    public DigInInverter(IDigIn dig_in)
    {
        _dig_in = dig_in;
    }

    public bool read_input()
    {
        return !_dig_in.read_input();
    }
}