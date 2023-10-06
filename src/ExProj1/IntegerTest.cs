using FluentAssertions;
using Xunit;

namespace ExProj1;

public class IntegerTest
{
    [Fact]
    public void Test1()
    {
        Err err = new();
        Math.capture_err(err);
        i8 a = 1;
        a.Should().NotBe(4);
    }

    [Fact]
    public void Test2()
    {
        u32 u32 = 1;
        u8 u8 = 1;

        (u32 > u8).Should().BeFalse();
        (u32 < u8).Should().BeFalse();

        (u32 == u8).Should().BeTrue();
        (u32 >= u8).Should().BeTrue();
        (u32 <= u8).Should().BeTrue();
    }
}