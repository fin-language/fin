namespace finlang.test;

public class Sample
{
    public Sample()
    {
        math.CurrentMode.Should().Be(math.Mode.Unsafe);
    }

    public int get_int2()
    {
        u8 a = 2, b = 44, c = 2;
        return (a + b) / c;
    }

    public void method()
    {
        int x = get_int();
        math.CurrentMode.Should().Be(math.Mode.Unsafe);
    }

    public int get_int()
    {
        math.CurrentMode.Should().Be(math.Mode.Unsafe);
        return 2234;
    }
}

public class ScopeTest
{
    [Fact]
    public void Test1()
    {
        math.capture_errors(new err.Err()); // Math.scoped_pragma
        math.CurrentMode.Should().Be(math.Mode.UserProvidedErr);

        Sample sample = new();
        math.CurrentMode.Should().Be(math.Mode.UserProvidedErr);

        sample.method();
        math.CurrentMode.Should().Be(math.Mode.UserProvidedErr);
    }

    [Fact]
    public void Test2()
    {
        ScopeTracker.CurrentScope.prevMathMode.Should().Be(math.Mode.NotSpecified);
    }
}
