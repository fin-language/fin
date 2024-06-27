using finlang;

namespace hal;

public class CArrayDependencyTest : FinObj
{
    // should have dependency on uint8_t
    public c_array<u8> _data;

    public CArrayDependencyTest(c_array<u8> data)
    {
        this._data = data;
    }
}
