using fin.sim.err;
using FluentAssertions;
using System;
using Xunit;

namespace ExProj1;

public class IntegerTest
{
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


    /// <summary>
    /// https://github.com/fin-language/fin/issues/10
    /// </summary>
    [Fact]
    public void LambdaScopeTest()
    {
        Err err = mem.stack(new Err());
        math.capture_errors(err);

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
        Action action = () => { add_that_will_throw(a, b); };
        action.Should().Throw<InvalidOperationException>().WithMessage("*Math mode must be specified*");
    }

    private u8 add_that_will_throw(u8 a, u8 b)
    {
        // this will throw because mode not specified
        return a + b;
    }

    [Fact]
    public void U8Overflow_UnsafeMode()
    {
        math.unsafe_mode();
        u8 a = 255, b = 255;
        Action action = () => { var c = a + b; };
        action.Should().Throw<OverflowException>().WithMessage("Overflow! `255 (u8) + 255 (u8)` result `510` is beyond u8 type MAX limit of `255`. Explicitly widen before `+` operation.");
    }

    [Fact]
    public void U8Overflow_UserProvidedErr()
    {
        Err err = mem.stack(new Err());
        math.capture_errors(err);
        u8 a = 255, b = 255;
        var c = a + b;
        err.provide_context();
        c.Should().Be(254); // wrapped around

        err.has_error().Should().BeTrue();
        err.get_error().Should().BeOfType<OverflowError>();
        err.clear();
    }

    [Fact]
    public void I8Underflow_UnsafeMode()
    {
        math.unsafe_mode();
        i8 a = -120, b = -120;
        Action action = () => { var c = a + b; };
        action.Should().Throw<OverflowException>().WithMessage("Underflow! `-120 (i8) + -120 (i8)` result `-240` is beyond i8 type MIN limit of `-128`. Explicitly widen before `+` operation.");
    }

    [Fact]
    public void I8AddNegToMin()
    {
        math.unsafe_mode();
        i8 a = -120, b = -8;
        i8 c = a + b;
        c.Should().Be(-128);
    }

    [Fact]
    public void I8Underflow_UserProvidedErr()
    {
        Err err = mem.stack(new Err());
        math.capture_errors(err);
        i8 a = -120, b = -9;
        i8 c = a + b;
        c.Should().Be(127);
        err.get_error().Should().BeOfType<UnderflowError>();
        err.clear();
    }

    [Fact]
    public void Unsafe_TestOverflowI32()
    {
        math.unsafe_mode();
        i32 a = i32.MAX, b = 2;
        Action action = () => { var c = a + b; };
        action.Should().Throw<OverflowException>().WithMessage("Overflow! `2147483647 (i32) + 2 (i32)` result `2147483649` is beyond i32 type MAX limit of `2147483647`. Explicitly widen before `+` operation.");
    }

    [Fact]
    public void Unsafe_TestOverflowU64()
    {
        math.unsafe_mode();
        u64 a = u64.MAX, b = 2;
        Action action = () => { var c = a + b; };
        action.Should().Throw<OverflowException>().WithMessage("Overflow! `18446744073709551615 (u64) + 2 (u64)` result `18446744073709551617` is beyond u64 type MAX limit of `18446744073709551615`. Explicitly widen before `+` operation.");
    }


    //--------------------------------------------------------------------------------

    [Fact]
    public void UnsafeTo_ModeUnsafe_OK()
    {
        math.unsafe_mode();
        i8 a = 127;
        u8 b = a.unsafe_to_u8(); // should not throw
        b.Should().Be(127);
    }

    [Fact]
    public void UnsafeTo_ModeUserProvidedErr_OK()
    {
        Err err = mem.stack(new Err());
        math.capture_errors(err);
        i8 a = 127;
        u8 b = a.unsafe_to_u8(); // should not throw
        b.Should().Be(127);
    }

    [Fact]
    public void UnsafeTo_ModeUnsafe_Exception()
    {
        math.unsafe_mode();
        i8 a = -1;

        Action action = () => { u8 b = a.unsafe_to_u8(); };
        action.Should().Throw<OverflowException>().WithMessage("Underflow! i8 value `-1` cannot be converted to type u8.");
    }

    [Fact]
    public void UnsafeTo_ModeUserProvidedErr()
    {
        Err err = mem.stack(new Err());
        math.capture_errors(err);
        i8 a = -1;
        u8 b = a.unsafe_to_u8();
        err.get_error().Should().BeOfType<UnderflowError>();
        b.Should().Be(255);

        err.clear(); // require for simulation to not throw
    }


    //--------------------------------------------------------------------------------


    [Fact]
    public void Err_NotChecked_Simple()
    {
        Action action = () => {
            Err err = mem.stack(new Err());
            add_underflow_error(err);
        };

        action.Should().Throw<ErrMisuseException>();
    }

    [Fact]
    public void Err_NotChecked_Nested()
    {
        Action action = () => { method_that_doesnt_clear_err(); };
        action.Should().Throw<ErrMisuseException>();
    }

    private static void method_that_doesnt_clear_err()
    {
        Err err = mem.stack(new Err());
        add_underflow_error(err);
        err.get_error().Should().BeOfType<UnderflowError>();
    }

    [Fact]
    public void Err_ClearWithoutCheck()
    {
        Action action = () => {
            Err err = mem.stack(new Err());
            add_underflow_error(err);
            err.clear();
        };
        action.Should().Throw<ErrMisuseException>();
    }

    [Fact]
    public void Err_DisregardError()
    {
        Err err = mem.stack(new Err());
        add_underflow_error(err);
        err.disregard_any_error();
    }


    //--------------------------------------------------------------------------------

    private static void add_underflow_error(Err err)
    {
        math.capture_errors(err);
        i8 a = -1;
        a.unsafe_to_u8(); //sets err
    }


    //--------------------------------------------------------------------------------

    //test that checks if C# << causes overflow exception
    [Fact]
    public void CSharpLangTest_ShiftLeft()
    {
        Action action = () =>
        {
            checked
            {
                byte a = 255;
                a = ((byte)(a << 1));
            }
        };

        action.Should().Throw<OverflowException>();
    }
}








