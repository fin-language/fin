using finlang;

namespace ex_mem;

/// <summary>
/// TODO https://github.com/fin-language/fin/issues/33
/// </summary>
public class InitializedPoint : FinObj
{
    [mem] XyPointU8 point = mem.init(new XyPointU8() { x = 1, y = 2 });

    public InitializedPoint()
    {

    }

    public void test()
    {
    }
}   
