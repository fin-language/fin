using finlang.err;
using finlang;
using FluentAssertions;
using System;

namespace finlang.test;

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
    public void U8AddOverflow_UnsafeMode()
    {
        math.unsafe_mode();
        u8 a = 255, b = 255;
        Action action = () => { var c = a + b; };
        action.Should().Throw<OverflowException>().WithMessage("Overflow! `255 (u8) + 255 (u8)` result `510` is beyond u8 type MAX limit of `255`. Explicitly widen before `+` operation.");
    }

    [Fact]
    public void U8AddOverflow_UserProvidedErr()
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
    public void U8SubUnderflow_UnsafeMode()
    {
        math.unsafe_mode();
        u8 a = 1;
        Action action = () => { var c = a - 5; };
        action.Should().Throw<OverflowException>().WithMessage("Underflow! `1 (u8) - 5 (u8)` result `-4` is beyond u8 type MIN limit of `0`. Explicitly widen before `-` operation.");
    }

    [Fact]
    public void U8SubUnderflow_UserProvidedErr()
    {
        Err err = mem.stack(new Err());
        math.capture_errors(err);
        u8 a = 0, b = 1;
        var c = a - b;
        err.provide_context();
        c.Should().Be(255); // wrapped around

        err.has_error().Should().BeTrue();
        err.get_error().Should().BeOfType<UnderflowError>();
        err.clear();
    }

    [Fact]
    public void I8AddUnderflow_UnsafeMode()
    {
        math.unsafe_mode();
        i8 a = -120, b = -120;
        Action action = () => { var c = a + b; };
        action.Should().Throw<OverflowException>().WithMessage("Underflow! `-120 (i8) + -120 (i8)` result `-240` is beyond i8 type MIN limit of `-128`. Explicitly widen before `+` operation.");
    }

    [Fact]
    public void I8AddUnderflow_UserProvidedErr()
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
    public void I8SubUnderflow_UnsafeMode()
    {
        math.unsafe_mode();
        i8 a = -120;
        Action action = () => { var c = a - 9; };
        action.Should().Throw<OverflowException>().WithMessage("Underflow! `-120 (i8) - 9 (i8)` result `-129` is beyond i8 type MIN limit of `-128`. Explicitly widen before `-` operation.");
    }

    [Fact]
    public void I8SubOverflow_UnsafeMode()
    {
        math.unsafe_mode();
        i8 a = 127, b = -1;
        Action action = () => { var c = a - b; };
        action.Should().Throw<OverflowException>().WithMessage("Overflow! `127 (i8) - -1 (i8)` result `128` is beyond i8 type MAX limit of `127`. Explicitly widen before `-` operation.");
    }

    [Fact]
    public void I8SubUnderflow_UserProvidedErr()
    {
        Err err = mem.stack(new Err());
        math.capture_errors(err);
        i8 a = -120;
        i8 c = a - 20;
        c.Should().Be(127 - (140 - 128 - 1));
        ErrorShouldBe<UnderflowError>(err);
    }

    [Fact]
    public void I8U8Mult_OK()
    {
        math.unsafe_mode();
        i8 a = 127; u8 b = 10;
        var c = a * b;
        c.Should().BeOfType<i16>();
        c.Should().Be(1270);
    }

    [Fact]
    public void U8I8Mult_OK()
    {
        math.unsafe_mode();
        u8 a = 127; i8 b = 10;
        var c = a * b;
        c.Should().BeOfType<i16>();
        c.Should().Be(1270);
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
    public void I8Sub1()
    {
        math.unsafe_mode();
        i8 a = -120;
        i8 c = a - 5;
        c.Should().Be(-125);
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

    private static void ErrorShouldBe<T>(Err err)
    {
        err.get_error().Should().BeOfType<T>();
        err.clear();
    }

    //--------------------------------------------------------------------------------

    // confirms that C# << will not cause an overflow exception even in a checked block
    [Fact]
    public void CSharpLangTest_ShiftLeft_byte()
    {
        checked
        {
            uint a = uint.MaxValue;
            a = (a << 1);
        }
    }

    [Fact]
    public void wrap_lshift_captured_overshift()
    {
        Err err = mem.stack(new Err());
        math.capture_errors(err);
        u8 a = u8.MAX;
        a = a.wrap_lshift(10);

        a.Should().Be(0);
        err.has_error().Should().BeTrue();
        err.clear();
    }

    [Fact]
    public void ShiftLeft_unsafe_overshift_throws()
    {
        u8 a = u8.MAX;

        math.unsafe_mode();
        Action action = () => {
            u8 b = (a << 10);
            b.Should().Be(0); // avoid unused variable warning
        };
        a.Should().Be(u8.MAX);
        action.Should().Throw<OverflowException>();
    }

    [Fact]
    public void ShiftLeft_captured_overshift()
    {
        Err err = mem.stack(new Err());
        math.capture_errors(err);
        u8 a = u8.MAX;
        u8 b = (a << 10);

        a.Should().Be(u8.MAX);
        b.Should().Be(0);
        err.has_error().Should().BeTrue();
        err.clear();
    }

    [Fact]
    public void ShiftLeft_u8()
    {
        math.unsafe_mode();

        u8 a = 40;
        a <<= 2;
        a.Should().Be(160);
    }

    [Fact]
    public void ShiftLeft_u16()
    {
        math.unsafe_mode();

        u16 a = 4000;
        a <<= 2;
        a.Should().Be(16000);
    }

    [Fact]
    public void ShiftRight_u16()
    {
        math.unsafe_mode();

        u16 a = 16000;
        a >>= 2;
        a.Should().Be(4000);

        a = 3;
        a >>= 1;
        a.Should().Be(1);
        a >>= 1;
        a.Should().Be(0);
    }

    [Fact]
    public void ShiftLeft_u8_unsafe_overflow_throws()
    {
        u8 a = u8.MAX;

        math.unsafe_mode();
        Action action = () => {
            u8 b = (a << 1);
            b.Should().Be(0); // avoid unused variable warning
        };
        a.Should().Be(u8.MAX);
        action.Should().Throw<OverflowException>();
    }

    [Fact]
    public void ShiftLeft_u64_unsafe_overflow_throws()
    {
        u64 a = u64.MAX;

        math.unsafe_mode();
        Action action = () => {
            u64 b = (a << 1);
            b.Should().Be(0); // avoid unused variable warning
        };
        a.Should().Be(u64.MAX);
        action.Should().Throw<OverflowException>();
    }

    [Fact]
    public void ShiftLeft_captured_overflow()
    {
        Err err = mem.stack(new Err());
        math.capture_errors(err);
        u8 a = u8.MAX;
        u8 b = (a << 1);

        a.Should().Be(u8.MAX);
        b.Should().Be(0);
        err.has_error().Should().BeTrue();
        err.clear();
    }

    //--------------------------------------------------------------------------------

    [Fact]
    public void U8BitAnd()
    {
        // math.unsafe_mode(); // not required because no error is possible
        u8 a = 0b_1111_0000;
        u8 result = a & 0b_0001_1111;
        result.Should().Be(0b_0001_0000);
    }

    [Fact]
    public void U32BitAnd()
    {
        // math.unsafe_mode(); // not required because no error is possible
        u32 mask = 0b_0000_0000_0000_0000_0000_0000_0001_1111;
        u8 a = 0b_1111_0000;
        { u32 result = a & mask; result.Should().Be(0b_0001_0000); }
        { u32 result = a & 0b_1000_0000_0000_0000_0000_0000_0001_1111; result.Should().Be(0b_0001_0000); }
    }

    [Fact]
    public void U8BitOr()
    {
        // math.unsafe_mode(); // not required because no error is possible
        u8 a = 0b_1111_0000;
        u8 result = a | 0b_0001_1111;
        result.Should().Be(0b_1111_1111);
    }

    [Fact]
    public void U8BitXor()
    {
        // math.unsafe_mode(); // not required because no error is possible
        u8 a = 0b_1111_0000;
        u8 result = a ^ 0b_0001_1111;
        result.Should().Be(0b_1110_1111);
    }

    [Fact]
    public void U8BitInvert()
    {
        // math.unsafe_mode(); // not required because no error is possible
        u8 a = 0b_1111_0000;
        u8 result = ~a;
        result.Should().Be(0b_0000_1111);
    }
}








