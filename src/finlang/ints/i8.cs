//NOTE! AUTO GENERATED FILE
using System;

#pragma warning disable IDE1006 // Naming Styles.
#pragma warning disable CS0652  // Useless comparison for integer types. Stuff like `u8 < 0`. Not a priority.

namespace finlang;

public struct i8: IHasI8
{
    public const sbyte MAX = 127;
    public const sbyte MIN = -128;

    /// <summary>
    /// Size of this type in bytes. Equivalent to `sizeof(int8_t)` in C.
    /// </summary>
    public static readonly u8 SIZE = 1;

    /// <summary>
    /// C# backing value.
    /// </summary>
    internal sbyte _csValue;

    public i8()
    {
    }

    private i8(sbyte value)
    {
        _csValue = value;
    }

    private static void ThrowIfMathModeNotSpecified()
    {
        math.ThrowIfModeNotSpecified();
    }

    /// <summary>
    /// C# read only backing value.
    /// </summary>
    internal sbyte _csReadValue => _csValue;

    public i8 value
    {
        get
        {
            // TODO: _ThrowIfDestructed();
            return this;
        }

        set
        {
            // TODO: _ThrowIfDestructed();
            this._csValue = value._csValue;
        }
    }

    /// <summary>
    /// Useful for when you need to specify an integer literal's type that could be many types.<br/>
    /// For example, in the code below, the `2` integer literal could be many different int types, but we want an i16.
    /// <code>var c = i8.from(2) + my_i8;</code>
    /// See https://github.com/fin-language/fin/issues/13
    /// </summary>
    public static i8 from(i8 value) => value;

    /// <summary>
    /// Implicit conversion from C# numeric type to fin numeric type.
    /// </summary>
    public static implicit operator i8(sbyte num) { return new i8(num); }

    /// <summary>
    /// Implicit conversion from fin numeric type to C# numeric type.
    /// </summary>
    /// This is needed for technical reasons, but I don't remember them. Should be documented.
    public static implicit operator sbyte(i8 num) { return num._csReadValue; }

    //################################################################
    // widening conversions
    //################################################################
    
    
    /// <summary>
    /// Safe type self declaration. See https://github.com/fin-language/fin/issues/21
    /// </summary>
    public i8 i8_ => value;

    /// <summary>
    /// Safe explicit widening conversion.
    /// </summary>
    public i16 i16 => value;

    /// <summary>
    /// Safe explicit widening conversion.
    /// </summary>
    public i32 i32 => value;

    /// <summary>
    /// Safe explicit widening conversion.
    /// </summary>
    public i64 i64 => value;


    /// <summary>
    /// Safe implicit widening conversion.
    /// </summary>
    public static implicit operator i16(i8 num) { return num._csReadValue; }

    /// <summary>
    /// Safe implicit widening conversion.
    /// </summary>
    public static implicit operator i32(i8 num) { return num._csReadValue; }

    /// <summary>
    /// Safe implicit widening conversion.
    /// </summary>
    public static implicit operator i64(i8 num) { return num._csReadValue; }


    //################################################################
    // narrowing conversions
    //################################################################
    
    /// <summary>
    /// Narrowing conversion from i8 to u8 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public u8 narrow_to_u8()
    {
        ThrowIfMathModeNotSpecified();
        sbyte value = this._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < u8.MIN) { throw new OverflowException($"Underflow! i8 value `{value}` cannot be converted to type u8."); }
                if (value > u8.MAX) { throw new OverflowException($"Overflow! i8 value `{value}` cannot be converted to type u8."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u8.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u8.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }
        
        return unchecked((byte)value);
    }

    /// <summary>
    /// Narrowing conversion from i16 to i8 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static i8 narrow_from(i16 v)
    {
        ThrowIfMathModeNotSpecified();
        short value = v._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i8.MIN) { throw new OverflowException($"Underflow! i8 value `{value}` cannot be converted to type i8."); }
                if (value > i8.MAX) { throw new OverflowException($"Overflow! i8 value `{value}` cannot be converted to type i8."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < i8.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i8.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }
        
        return unchecked((sbyte)value);
    }

    /// <summary>
    /// Narrowing conversion from i32 to i8 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static i8 narrow_from(i32 v)
    {
        ThrowIfMathModeNotSpecified();
        int value = v._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i8.MIN) { throw new OverflowException($"Underflow! i8 value `{value}` cannot be converted to type i8."); }
                if (value > i8.MAX) { throw new OverflowException($"Overflow! i8 value `{value}` cannot be converted to type i8."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < i8.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i8.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }
        
        return unchecked((sbyte)value);
    }

    /// <summary>
    /// Narrowing conversion from i64 to i8 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static i8 narrow_from(i64 v)
    {
        ThrowIfMathModeNotSpecified();
        decimal value = v._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i8.MIN) { throw new OverflowException($"Underflow! i8 value `{value}` cannot be converted to type i8."); }
                if (value > i8.MAX) { throw new OverflowException($"Overflow! i8 value `{value}` cannot be converted to type i8."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < i8.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i8.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }
        
        return unchecked((sbyte)value);
    }

    /// <summary>
    /// Narrowing conversion from u8 to i8 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static i8 narrow_from(u8 v)
    {
        ThrowIfMathModeNotSpecified();
        byte value = v._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i8.MIN) { throw new OverflowException($"Underflow! i8 value `{value}` cannot be converted to type i8."); }
                if (value > i8.MAX) { throw new OverflowException($"Overflow! i8 value `{value}` cannot be converted to type i8."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < i8.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i8.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }
        
        return unchecked((sbyte)value);
    }

    /// <summary>
    /// Narrowing conversion from u16 to i8 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static i8 narrow_from(u16 v)
    {
        ThrowIfMathModeNotSpecified();
        ushort value = v._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i8.MIN) { throw new OverflowException($"Underflow! i8 value `{value}` cannot be converted to type i8."); }
                if (value > i8.MAX) { throw new OverflowException($"Overflow! i8 value `{value}` cannot be converted to type i8."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < i8.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i8.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }
        
        return unchecked((sbyte)value);
    }

    /// <summary>
    /// Narrowing conversion from u32 to i8 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static i8 narrow_from(u32 v)
    {
        ThrowIfMathModeNotSpecified();
        uint value = v._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i8.MIN) { throw new OverflowException($"Underflow! i8 value `{value}` cannot be converted to type i8."); }
                if (value > i8.MAX) { throw new OverflowException($"Overflow! i8 value `{value}` cannot be converted to type i8."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < i8.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i8.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }
        
        return unchecked((sbyte)value);
    }

    /// <summary>
    /// Narrowing conversion from u64 to i8 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static i8 narrow_from(u64 v)
    {
        ThrowIfMathModeNotSpecified();
        decimal value = v._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i8.MIN) { throw new OverflowException($"Underflow! i8 value `{value}` cannot be converted to type i8."); }
                if (value > i8.MAX) { throw new OverflowException($"Overflow! i8 value `{value}` cannot be converted to type i8."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < i8.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i8.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }
        
        return unchecked((sbyte)value);
    }

    /// <summary>
    /// Narrowing conversion from i16 to i8 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static explicit operator i8(i16 num) => i8.narrow_from(num);

    ///// <summary>
    ///// Narrowing conversion from short to i8 when you don't expect data loss.
    ///// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    ///// or an exception will be thrown during simulation (if math mode is unsafe).
    ///// </summary>
    public static explicit operator i8(short num) => i8.narrow_from(num);

    /// <summary>
    /// Narrowing conversion from i32 to i8 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static explicit operator i8(i32 num) => i8.narrow_from(num);

    ///// <summary>
    ///// Narrowing conversion from int to i8 when you don't expect data loss.
    ///// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    ///// or an exception will be thrown during simulation (if math mode is unsafe).
    ///// </summary>
    public static explicit operator i8(int num) => i8.narrow_from(num);

    /// <summary>
    /// Narrowing conversion from i64 to i8 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static explicit operator i8(i64 num) => i8.narrow_from(num);

    ///// <summary>
    ///// Narrowing conversion from long to i8 when you don't expect data loss.
    ///// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    ///// or an exception will be thrown during simulation (if math mode is unsafe).
    ///// </summary>
    public static explicit operator i8(long num) => i8.narrow_from(num);

    /// <summary>
    /// Narrowing conversion from u8 to i8 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static explicit operator i8(u8 num) => i8.narrow_from(num);

    ///// <summary>
    ///// Narrowing conversion from byte to i8 when you don't expect data loss.
    ///// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    ///// or an exception will be thrown during simulation (if math mode is unsafe).
    ///// </summary>
    public static explicit operator i8(byte num) => i8.narrow_from(num);

    /// <summary>
    /// Narrowing conversion from u16 to i8 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static explicit operator i8(u16 num) => i8.narrow_from(num);

    ///// <summary>
    ///// Narrowing conversion from ushort to i8 when you don't expect data loss.
    ///// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    ///// or an exception will be thrown during simulation (if math mode is unsafe).
    ///// </summary>
    public static explicit operator i8(ushort num) => i8.narrow_from(num);

    /// <summary>
    /// Narrowing conversion from u32 to i8 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static explicit operator i8(u32 num) => i8.narrow_from(num);

    ///// <summary>
    ///// Narrowing conversion from uint to i8 when you don't expect data loss.
    ///// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    ///// or an exception will be thrown during simulation (if math mode is unsafe).
    ///// </summary>
    public static explicit operator i8(uint num) => i8.narrow_from(num);

    /// <summary>
    /// Narrowing conversion from u64 to i8 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static explicit operator i8(u64 num) => i8.narrow_from(num);

    ///// <summary>
    ///// Narrowing conversion from ulong to i8 when you don't expect data loss.
    ///// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    ///// or an exception will be thrown during simulation (if math mode is unsafe).
    ///// </summary>
    public static explicit operator i8(ulong num) => i8.narrow_from(num);


    //################################################################
    // wrapping conversions (only for unsigned)
    //################################################################
    
    

    //################################################################
    // comparisons
    //################################################################
    
    
    /// <summary>
    /// Both operands stay of type i8 during this operation (no implicit promotion to platform dependent int).<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator ==(i8 a, i8 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue == b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[==]]> operation, both operands are treated as i16.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator ==(i8 a, i16 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue == b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[==]]> operation, both operands are treated as i32.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator ==(i8 a, i32 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue == b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[==]]> operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator ==(i8 a, i64 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue == b._csReadValue;
    }

    
    /// <summary>
    /// Both operands stay of type i8 during this operation (no implicit promotion to platform dependent int).<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator !=(i8 a, i8 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue != b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[!=]]> operation, both operands are treated as i16.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator !=(i8 a, i16 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue != b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[!=]]> operation, both operands are treated as i32.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator !=(i8 a, i32 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue != b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[!=]]> operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator !=(i8 a, i64 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue != b._csReadValue;
    }

    
    /// <summary>
    /// Both operands stay of type i8 during this operation (no implicit promotion to platform dependent int).<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator <(i8 a, i8 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue < b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[<]]> operation, both operands are treated as i16.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator <(i8 a, i16 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue < b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[<]]> operation, both operands are treated as i32.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator <(i8 a, i32 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue < b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[<]]> operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator <(i8 a, i64 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue < b._csReadValue;
    }

    
    /// <summary>
    /// Both operands stay of type i8 during this operation (no implicit promotion to platform dependent int).<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator <=(i8 a, i8 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue <= b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[<=]]> operation, both operands are treated as i16.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator <=(i8 a, i16 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue <= b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[<=]]> operation, both operands are treated as i32.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator <=(i8 a, i32 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue <= b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[<=]]> operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator <=(i8 a, i64 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue <= b._csReadValue;
    }

    
    /// <summary>
    /// Both operands stay of type i8 during this operation (no implicit promotion to platform dependent int).<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator >(i8 a, i8 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue > b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[>]]> operation, both operands are treated as i16.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator >(i8 a, i16 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue > b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[>]]> operation, both operands are treated as i32.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator >(i8 a, i32 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue > b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[>]]> operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator >(i8 a, i64 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue > b._csReadValue;
    }

    
    /// <summary>
    /// Both operands stay of type i8 during this operation (no implicit promotion to platform dependent int).<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator >=(i8 a, i8 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue >= b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[>=]]> operation, both operands are treated as i16.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator >=(i8 a, i16 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue >= b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[>=]]> operation, both operands are treated as i32.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator >=(i8 a, i32 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue >= b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[>=]]> operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator >=(i8 a, i64 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue >= b._csReadValue;
    }


    
    /// <summary>
    /// Both operands stay of type i8 during this operation (no implicit promotion to platform dependent int).<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i8 operator +(i8 a, i8 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (short)a._csReadValue + b._csReadValue; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i8.MIN) { throw new OverflowException($"Underflow! `{a} (i8) + {b} (i8)` result `{value}` is beyond i8 type MIN limit of `{i8.MIN}`. Explicitly widen before `+` operation."); }
            if (value > i8.MAX) { throw new OverflowException($"Overflow! `{a} (i8) + {b} (i8)` result `{value}` is beyond i8 type MAX limit of `{i8.MAX}`. Explicitly widen before `+` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < i8.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i8.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i8 result = unchecked((sbyte)value);
        return result;
    }

    /// <summary>
    /// NOTE: before the <![CDATA[+]]> operation, both operands are treated as i16.<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i16 operator +(i8 a, i16 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (int)a._csReadValue + b._csReadValue; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i16.MIN) { throw new OverflowException($"Underflow! `{a} (i16) + {b} (i16)` result `{value}` is beyond i16 type MIN limit of `{i16.MIN}`. Explicitly widen before `+` operation."); }
            if (value > i16.MAX) { throw new OverflowException($"Overflow! `{a} (i16) + {b} (i16)` result `{value}` is beyond i16 type MAX limit of `{i16.MAX}`. Explicitly widen before `+` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < i8.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i8.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i16 result = unchecked((short)value);
        return result;
    }

    /// <summary>
    /// NOTE: before the <![CDATA[+]]> operation, both operands are treated as i32.<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i32 operator +(i8 a, i32 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (long)a._csReadValue + b._csReadValue; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i32.MIN) { throw new OverflowException($"Underflow! `{a} (i32) + {b} (i32)` result `{value}` is beyond i32 type MIN limit of `{i32.MIN}`. Explicitly widen before `+` operation."); }
            if (value > i32.MAX) { throw new OverflowException($"Overflow! `{a} (i32) + {b} (i32)` result `{value}` is beyond i32 type MAX limit of `{i32.MAX}`. Explicitly widen before `+` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < i8.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i8.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i32 result = unchecked((int)value);
        return result;
    }

    /// <summary>
    /// NOTE: before the <![CDATA[+]]> operation, both operands are treated as i64.<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i64 operator +(i8 a, i64 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (decimal)a._csReadValue + b._csReadValue; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i64.MIN) { throw new OverflowException($"Underflow! `{a} (i64) + {b} (i64)` result `{value}` is beyond i64 type MIN limit of `{i64.MIN}`. Explicitly widen before `+` operation."); }
            if (value > i64.MAX) { throw new OverflowException($"Overflow! `{a} (i64) + {b} (i64)` result `{value}` is beyond i64 type MAX limit of `{i64.MAX}`. Explicitly widen before `+` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < i8.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i8.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i64 result = unchecked((long)value);
        return result;
    }


    
    /// <summary>
    /// Both operands stay of type i8 during this operation (no implicit promotion to platform dependent int).<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i8 operator -(i8 a, i8 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (short)a._csReadValue - b._csReadValue; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i8.MIN) { throw new OverflowException($"Underflow! `{a} (i8) - {b} (i8)` result `{value}` is beyond i8 type MIN limit of `{i8.MIN}`. Explicitly widen before `-` operation."); }
            if (value > i8.MAX) { throw new OverflowException($"Overflow! `{a} (i8) - {b} (i8)` result `{value}` is beyond i8 type MAX limit of `{i8.MAX}`. Explicitly widen before `-` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < i8.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i8.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i8 result = unchecked((sbyte)value);
        return result;
    }

    /// <summary>
    /// NOTE: before the <![CDATA[-]]> operation, both operands are treated as i16.<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i16 operator -(i8 a, i16 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (int)a._csReadValue - b._csReadValue; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i16.MIN) { throw new OverflowException($"Underflow! `{a} (i16) - {b} (i16)` result `{value}` is beyond i16 type MIN limit of `{i16.MIN}`. Explicitly widen before `-` operation."); }
            if (value > i16.MAX) { throw new OverflowException($"Overflow! `{a} (i16) - {b} (i16)` result `{value}` is beyond i16 type MAX limit of `{i16.MAX}`. Explicitly widen before `-` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < i8.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i8.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i16 result = unchecked((short)value);
        return result;
    }

    /// <summary>
    /// NOTE: before the <![CDATA[-]]> operation, both operands are treated as i32.<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i32 operator -(i8 a, i32 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (long)a._csReadValue - b._csReadValue; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i32.MIN) { throw new OverflowException($"Underflow! `{a} (i32) - {b} (i32)` result `{value}` is beyond i32 type MIN limit of `{i32.MIN}`. Explicitly widen before `-` operation."); }
            if (value > i32.MAX) { throw new OverflowException($"Overflow! `{a} (i32) - {b} (i32)` result `{value}` is beyond i32 type MAX limit of `{i32.MAX}`. Explicitly widen before `-` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < i8.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i8.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i32 result = unchecked((int)value);
        return result;
    }

    /// <summary>
    /// NOTE: before the <![CDATA[-]]> operation, both operands are treated as i64.<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i64 operator -(i8 a, i64 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (decimal)a._csReadValue - b._csReadValue; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i64.MIN) { throw new OverflowException($"Underflow! `{a} (i64) - {b} (i64)` result `{value}` is beyond i64 type MIN limit of `{i64.MIN}`. Explicitly widen before `-` operation."); }
            if (value > i64.MAX) { throw new OverflowException($"Overflow! `{a} (i64) - {b} (i64)` result `{value}` is beyond i64 type MAX limit of `{i64.MAX}`. Explicitly widen before `-` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < i8.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i8.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i64 result = unchecked((long)value);
        return result;
    }


    
    /// <summary>
    /// Both operands stay of type i8 during this operation (no implicit promotion to platform dependent int).<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i8 operator *(i8 a, i8 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (short)a._csReadValue * b._csReadValue; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i8.MIN) { throw new OverflowException($"Underflow! `{a} (i8) * {b} (i8)` result `{value}` is beyond i8 type MIN limit of `{i8.MIN}`. Explicitly widen before `*` operation."); }
            if (value > i8.MAX) { throw new OverflowException($"Overflow! `{a} (i8) * {b} (i8)` result `{value}` is beyond i8 type MAX limit of `{i8.MAX}`. Explicitly widen before `*` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < i8.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i8.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i8 result = unchecked((sbyte)value);
        return result;
    }

    /// <summary>
    /// NOTE: before the <![CDATA[*]]> operation, both operands are treated as i16.<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i16 operator *(i8 a, i16 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (int)a._csReadValue * b._csReadValue; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i16.MIN) { throw new OverflowException($"Underflow! `{a} (i16) * {b} (i16)` result `{value}` is beyond i16 type MIN limit of `{i16.MIN}`. Explicitly widen before `*` operation."); }
            if (value > i16.MAX) { throw new OverflowException($"Overflow! `{a} (i16) * {b} (i16)` result `{value}` is beyond i16 type MAX limit of `{i16.MAX}`. Explicitly widen before `*` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < i8.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i8.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i16 result = unchecked((short)value);
        return result;
    }

    /// <summary>
    /// NOTE: before the <![CDATA[*]]> operation, both operands are treated as i32.<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i32 operator *(i8 a, i32 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (long)a._csReadValue * b._csReadValue; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i32.MIN) { throw new OverflowException($"Underflow! `{a} (i32) * {b} (i32)` result `{value}` is beyond i32 type MIN limit of `{i32.MIN}`. Explicitly widen before `*` operation."); }
            if (value > i32.MAX) { throw new OverflowException($"Overflow! `{a} (i32) * {b} (i32)` result `{value}` is beyond i32 type MAX limit of `{i32.MAX}`. Explicitly widen before `*` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < i8.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i8.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i32 result = unchecked((int)value);
        return result;
    }

    /// <summary>
    /// NOTE: before the <![CDATA[*]]> operation, both operands are treated as i64.<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i64 operator *(i8 a, i64 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (decimal)a._csReadValue * b._csReadValue; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i64.MIN) { throw new OverflowException($"Underflow! `{a} (i64) * {b} (i64)` result `{value}` is beyond i64 type MIN limit of `{i64.MIN}`. Explicitly widen before `*` operation."); }
            if (value > i64.MAX) { throw new OverflowException($"Overflow! `{a} (i64) * {b} (i64)` result `{value}` is beyond i64 type MAX limit of `{i64.MAX}`. Explicitly widen before `*` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < i8.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i8.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i64 result = unchecked((long)value);
        return result;
    }



    //################################################################
    // wrapping numeric methods (unsigned only for now)
    //################################################################
    
    

    

    

            
    //################################################################
    // shift methods (unsigned only for now)
    //################################################################
    
    

    
    //################################################################
    // bit methods (unsigned only for now)
    //################################################################
    
    

    

    

    


    //################################################################
    // misc
    //################################################################
    

    public override string ToString()
    {
        return _csReadValue.ToString();
    }

    public override int GetHashCode()
    {
        return value.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) { return false; }

        decimal obj_value;

        switch (obj)
        {
            case sbyte  i: obj_value = i; break;
            case short  i: obj_value = i; break;
            case int    i: obj_value = i; break;
            case long   i: obj_value = i; break;
            case byte   i: obj_value = i; break;
            case ushort i: obj_value = i; break;
            case uint   i: obj_value = i; break;
            case ulong  i: obj_value = i; break;

            case i8  i: obj_value = i._csReadValue; break;
            case i16 i: obj_value = i._csReadValue; break;
            case i32 i: obj_value = i._csReadValue; break;
            case i64 i: obj_value = i._csReadValue; break;
            case u8  i: obj_value = i._csReadValue; break;
            case u16 i: obj_value = i._csReadValue; break;
            case u32 i: obj_value = i._csReadValue; break;
            case u64 i: obj_value = i._csReadValue; break;

            default: return false;
        }

        if (obj_value < MIN || obj_value > MAX) { return false; }
        return obj_value == (sbyte)value;
    }
}