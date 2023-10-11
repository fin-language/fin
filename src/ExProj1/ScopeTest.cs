using FluentAssertions;
using Xunit;

namespace ExProj1;

public class Sample
{
    public Sample()
    {
        Math.CurrentMode.Should().Be(Math.Mode.Checked);
    }

    public int get_int2()
    {
        u8 a = 2, b = 44, c = 2;
        //Math.force_inlined();
        return (a + b) / c;
    }

    public void method()
    {
        int x = get_int();
        Math.CurrentMode.Should().Be(Math.Mode.Checked);
    }

    public int get_int()
    {
        Math.CurrentMode.Should().Be(Math.Mode.Checked);
        return 2234;
    }
}

public class ScopeTest
{
    [Fact]
    public void Test1()
    {
        Math.unsafe_mode(); // Math.scoped_pragma
        Math.CurrentMode.Should().Be(Math.Mode.Unsafe);

        Sample sample = new();
        Math.CurrentMode.Should().Be(Math.Mode.Unsafe);

        sample.method();
        Math.CurrentMode.Should().Be(Math.Mode.Unsafe);
    }

    [Fact]
    public void Test2()
    {
        ScopeTracker.CurrentScope.mode.Should().Be(Math.Mode.NotSpecified);
    }
}
