using finlang;

namespace ts;

public class CArrayEx : FinObj
{
    // NO [mem] attribute
    public c_array<u8> _data;

    public CArrayEx(c_array<u8> data)
    {
        this._data = data;
    }
}
