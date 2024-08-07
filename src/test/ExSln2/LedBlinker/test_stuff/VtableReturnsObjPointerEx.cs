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
public interface IBikeProvider : IFinObj
{
    Bike get_bike();
}

// https://github.com/fin-language/fin/issues/79
// https://github.com/fin-language/fin/issues/58
public class VtableReturnsObjPointerEx : FinObj, IBikeProvider
{
    [mem] public Bike _bike = mem.init(new Bike());
    public Bike _bike_ptr; // C# won't allow `public Bike _bike_ptr = this._bike;`. It must be done in the constructor which is good for us.

    public VtableReturnsObjPointerEx()
    {
        _bike._speed = 10;
        _bike_ptr = _bike;
    }

    // https://github.com/fin-language/fin/issues/79
    public Bike get_bike()
    {
        return _bike;
    }

    // https://github.com/fin-language/fin/issues/79
    public u8 get_speed()
    {
        return _bike._speed;
    }

    // https://github.com/fin-language/fin/issues/79
    public u8 calc_bike_stuff()
    {
        // pointer to mem object
        Bike b = _bike;
        b = _bike;
        b = get_bike();
        return b._speed;
    }
}
