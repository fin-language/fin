namespace finlang.test.CSharpLangTests;

/// <summary>
/// This class helps ensure we understand the intracacies of C# interfaces.
/// </summary>
public class Interfaces
{
    public interface IGetInt
    {
        int get_int();
    }

    public interface IGetDouble
    {
        double get_double();
    }

    public class IntAndDouble : IGetInt, IGetDouble
    {
        public int get_int() => 1;
        public double get_double() => 1.0;
    }

    public class JustDouble : IGetDouble
    {
        public double get_double() => 2.0;
    }

    public class JustInt : IGetInt
    {
        public int get_int() => 2;
    }

    [Fact]
    public void CastBetweenIncompatibleInterfaces()
    {
        var obj = new IntAndDouble();

        // basic stuff
        IGetInt getInt = obj;
        getInt.get_int().Should().Be(1);
        IGetDouble getDouble = obj;
        getDouble.get_double().Should().Be(1.0);

        // Trouble! C# allows casting one interface to an incompatible interface as long as the object implements both.
        // This likely requires some type of Runtime Type Identification (RTTI) to work.
        // We aren't planning to support this in finlang yet.
        getDouble = (IGetDouble)getInt;
        getDouble.get_double().Should().Be(1.0);
    }

    [Fact]
    public void CastBetweenIncompatibleObjects()
    {
        JustInt justInt = new();

        // the below cast will throw an exception as the underlying object does not implement IGetDouble
        Action action = () => {
            var d = (IGetDouble)justInt;
        };
        action.Should().Throw<InvalidCastException>();
    }
}
