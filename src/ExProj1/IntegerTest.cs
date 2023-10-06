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
}