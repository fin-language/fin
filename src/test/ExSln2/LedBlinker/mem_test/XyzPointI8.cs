using finlang;

namespace ex_mem;

/// <summary>
/// Example for field declaration initialization
/// </summary>
public class XyzPointI8 : FinObj
{
    public i8 x = -1, y = 1;
    public i8 z;

    public XyzPointI8(i8 z)
    {
        this.z = z;
    }
}
