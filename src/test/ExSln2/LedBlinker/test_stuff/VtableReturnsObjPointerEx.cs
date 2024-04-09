using finlang;

namespace issue58;

// This is just a dummy class to test the issue
// https://github.com/fin-language/fin/issues/58
public class Bike : FinObj
{
    public u8 _speed = 0;
}

// needs a method that returns a pointer to an object to test the issue
// https://github.com/fin-language/fin/issues/58
public interface IBikeProvider
{
    Bike get_bike();
}

// https://github.com/fin-language/fin/issues/58
public class VtableReturnsObjPointerEx : FinObj, IBikeProvider
{
    [mem] public Bike _bike = mem.init(new Bike());

    public Bike get_bike()
    {
        return _bike;
    }
}
