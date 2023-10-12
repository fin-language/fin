using fin.sim.err;
using FluentAssertions;
using System;
using Xunit;

namespace ExProj1;

public class IntegerTest
{
    [Fact]
    public void UserProvidedErrorTracking()
    {
        Err err = new();
        math.capture_errors(err);
        i8 a = 1;
        a.Should().NotBe(4);
    }

    [Fact]
    public void Test2()
    {
        math.unsafe_mode();
        u32 u32 = 1;
        u8 u8 = 1;

        (u32 > u8).Should().BeFalse();
        (u32 < u8).Should().BeFalse();

        (u32 == u8).Should().BeTrue();
        (u32 >= u8).Should().BeTrue();
        (u32 <= u8).Should().BeTrue();
    }

    private u8 add(u8 a, u8 b)
    {
        // this will throw because mode not specified
        return a + b;
    }

    /// <summary>
    /// https://github.com/fin-language/fin/issues/10
    /// </summary>
    [Fact]
    public void LambdaScopeTest()
    {
        math.capture_errors(new Err());

        u8 a = 1, b = 1;
        Action action = () => {
            math.CurrentMode.Should().Be(math.Mode.UserProvidedErr);    //should inherit from parent scope
            math.unsafe_mode(); // should not affect parent scope
            var c = a + b;
            math.CurrentMode.Should().Be(math.Mode.Unsafe);
        };

        math.CurrentMode.Should().Be(math.Mode.UserProvidedErr);    //should not be effected by lambda
        action();
    }

    [Fact]
    public void ThrowIfModeNotSpecified()
    {
        u8 a = 1, b = 1;
        Action action = () => { var c = a + b; };
        action.Should().Throw<InvalidOperationException>().WithMessage("*Math mode must be specified*");
    }

    [Fact]
    public void ThrowIfModeNotSpecifiedNestedFunc()
    {
        u8 a = 1, b = 1;
        Action action = () => { add(a, b); };
        action.Should().Throw<InvalidOperationException>().WithMessage("*Math mode must be specified*");
    }

    [Fact]
    public void Unsafe_TestOverflowMessageU8()
    {
        math.unsafe_mode();
        u8 a = 255, b = 255;
        Action action = () => { var c = a + b; };
        action.Should().Throw<OverflowException>().WithMessage("Overflow! `255 (u8) + 255 (u8)` result `510` is beyond u8 type MAX limit of `255`. Explicitly widen before `+` operation.");
    }

    [Fact]
    public void UserErr_TestOverflowMessageU8()
    {
        Err err = new();
        math.capture_errors(err);
        u8 a = 255, b = 255;
        var c = a + b;
        err.provide_context();

        err.has_error().Should().BeTrue();
        err.get_error().Should().BeOfType<OverflowError>();
    }

    [Fact]
    public void Unsafe_TestUnderflowMessageI8()
    {
        math.unsafe_mode();
        i8 a = -120, b = -120;
        Action action = () => { var c = a + b; };
        action.Should().Throw<OverflowException>().WithMessage("Underflow! `-120 (i8) + -120 (i8)` result `-240` is beyond i8 type MIN limit of `-128`. Explicitly widen before `+` operation.");
    }

    [Fact]
    public void Unsafe_UnsafeTo()
    {
        math.unsafe_mode();
        i8 a = 127;
        u8 b = a.unsafe_to_u8(); // should not throw
        b.Should().Be(127);
    }

    [Fact]
    public void Unsafe_UnsafeToException()
    {
        math.unsafe_mode();
        i8 a = -120;
        Action action = () => { var c = a.unsafe_to_u8(); };
        action.Should().Throw<OverflowException>().WithMessage("i8 value `-120` cannot be converted to type u8.");
    }
}