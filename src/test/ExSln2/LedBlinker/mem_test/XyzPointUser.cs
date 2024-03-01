using finlang;

namespace ex_mem;

/// <summary>
/// Example for testing constructor with argument
/// </summary>
public class XyzPointUser : FinObj
{
    [mem] XyzPointI8 p1 = mem.init(new XyzPointI8(55));

    public XyzPointUser()
    {
    }
}