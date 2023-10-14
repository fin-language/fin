using fin.sim.err;
using FluentAssertions;
using Xunit;

namespace ExProj1;

public class Simple
{
    [Fact]
    public void Test1()
    {
        math.unsafe_mode();
        u8 a = 255;
        u8 b = 1;
        //u8 c = a + b; // throws during simulation
    }

    [Fact]
    public void Test2()
    {
        Err err = mem.stack(new Err());
        math.capture_errors(err);
        u8 a = 255;
        u8 b = 1;
        u8 c = a + b;

        // test code:
        c.Should().Be(0);
        err.has_error().Should().BeTrue();
        err.clear();
    }
}
