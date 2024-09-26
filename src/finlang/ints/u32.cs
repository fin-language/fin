//NOTE! AUTO GENERATED FILE
using System;

#pragma warning disable IDE1006 // Naming Styles.
#pragma warning disable CS0652  // Useless comparison for integer types. Stuff like `u8 < 0`. Not a priority.

namespace finlang;

public struct u32: IHasU32
{
    public const uint MAX = 4294967295;
    public const uint MIN = 0;

    /// <summary>
    /// Size of this type in bytes. Equivalent to `sizeof(uint32_t)` in C.
    /// </summary>
    public static readonly u8 SIZE = 4;

    /// <summary>
    /// C# backing value.
    /// </summary>
    internal uint _csValue;

    public u32()
    {
    }

    private u32(uint value)
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
    internal uint _csReadValue => _csValue;

    public u32 value
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
    /// <code>var c = u32.from(2) + my_u32;</code>
    /// See https://github.com/fin-language/fin/issues/13
    /// </summary>
    public static u32 from(u32 value) => value;

    /// <summary>
    /// Implicit conversion from C# numeric type to fin numeric type.
    /// </summary>
    public static implicit operator u32(uint num) { return new u32(num); }

    /// <summary>
    /// Implicit conversion from fin numeric type to C# numeric type.
    /// </summary>
    /// This is needed for technical reasons, but I don't remember them. Should be documented.
    public static implicit operator uint(u32 num) { return num._csReadValue; }

    //################################################################
    // widening conversions
    //################################################################
    
    
    /// <summary>
    /// Safe type self declaration. See https://github.com/fin-language/fin/issues/21
    /// </summary>
    public u32 u32_ => value;

    /// <summary>
    /// Safe explicit widening conversion.
    /// </summary>
    public u64 u64 => value;

    /// <summary>
    /// Safe explicit widening conversion.
    /// </summary>
    public i64 i64 => value;


    /// <summary>
    /// Safe implicit widening conversion.
    /// </summary>
    public static implicit operator u64(u32 num) { return num._csReadValue; }

    /// <summary>
    /// Safe implicit widening conversion.
    /// </summary>
    public static implicit operator i64(u32 num) { return num._csReadValue; }


    //################################################################
    // narrowing conversions
    //################################################################
    
    /// <summary>
    /// Narrowing conversion from u32 to i32 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public i32 narrow_to_i32()
    {
        ThrowIfMathModeNotSpecified();
        uint value = this._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i32.MIN) { throw new OverflowException($"Underflow! u32 value `{value}` cannot be converted to type i32."); }
                if (value > i32.MAX) { throw new OverflowException($"Overflow! u32 value `{value}` cannot be converted to type i32."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < i32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }
        
        return unchecked((int)value);
    }

    /// <summary>
    /// Narrowing conversion from u32 to i16 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public i16 narrow_to_i16()
    {
        ThrowIfMathModeNotSpecified();
        uint value = this._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i16.MIN) { throw new OverflowException($"Underflow! u32 value `{value}` cannot be converted to type i16."); }
                if (value > i16.MAX) { throw new OverflowException($"Overflow! u32 value `{value}` cannot be converted to type i16."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < i16.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i16.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }
        
        return unchecked((short)value);
    }

    /// <summary>
    /// Narrowing conversion from u32 to u16 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public u16 narrow_to_u16()
    {
        ThrowIfMathModeNotSpecified();
        uint value = this._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < u16.MIN) { throw new OverflowException($"Underflow! u32 value `{value}` cannot be converted to type u16."); }
                if (value > u16.MAX) { throw new OverflowException($"Overflow! u32 value `{value}` cannot be converted to type u16."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u16.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u16.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }
        
        return unchecked((ushort)value);
    }

    /// <summary>
    /// Narrowing conversion from u32 to i8 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public i8 narrow_to_i8()
    {
        ThrowIfMathModeNotSpecified();
        uint value = this._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i8.MIN) { throw new OverflowException($"Underflow! u32 value `{value}` cannot be converted to type i8."); }
                if (value > i8.MAX) { throw new OverflowException($"Overflow! u32 value `{value}` cannot be converted to type i8."); }
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
    /// Narrowing conversion from u32 to u8 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public u8 narrow_to_u8()
    {
        ThrowIfMathModeNotSpecified();
        uint value = this._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < u8.MIN) { throw new OverflowException($"Underflow! u32 value `{value}` cannot be converted to type u8."); }
                if (value > u8.MAX) { throw new OverflowException($"Overflow! u32 value `{value}` cannot be converted to type u8."); }
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
    /// Narrowing conversion from i8 to u32 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static u32 narrow_from(i8 v)
    {
        ThrowIfMathModeNotSpecified();
        sbyte value = v._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < u32.MIN) { throw new OverflowException($"Underflow! u32 value `{value}` cannot be converted to type u32."); }
                if (value > u32.MAX) { throw new OverflowException($"Overflow! u32 value `{value}` cannot be converted to type u32."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }
        
        return unchecked((uint)value);
    }

    /// <summary>
    /// Narrowing conversion from i16 to u32 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static u32 narrow_from(i16 v)
    {
        ThrowIfMathModeNotSpecified();
        short value = v._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < u32.MIN) { throw new OverflowException($"Underflow! u32 value `{value}` cannot be converted to type u32."); }
                if (value > u32.MAX) { throw new OverflowException($"Overflow! u32 value `{value}` cannot be converted to type u32."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }
        
        return unchecked((uint)value);
    }

    /// <summary>
    /// Narrowing conversion from i32 to u32 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static u32 narrow_from(i32 v)
    {
        ThrowIfMathModeNotSpecified();
        int value = v._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < u32.MIN) { throw new OverflowException($"Underflow! u32 value `{value}` cannot be converted to type u32."); }
                if (value > u32.MAX) { throw new OverflowException($"Overflow! u32 value `{value}` cannot be converted to type u32."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }
        
        return unchecked((uint)value);
    }

    /// <summary>
    /// Narrowing conversion from i64 to u32 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static u32 narrow_from(i64 v)
    {
        ThrowIfMathModeNotSpecified();
        decimal value = v._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < u32.MIN) { throw new OverflowException($"Underflow! u32 value `{value}` cannot be converted to type u32."); }
                if (value > u32.MAX) { throw new OverflowException($"Overflow! u32 value `{value}` cannot be converted to type u32."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }
        
        return unchecked((uint)value);
    }

    /// <summary>
    /// Narrowing conversion from u64 to u32 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static u32 narrow_from(u64 v)
    {
        ThrowIfMathModeNotSpecified();
        decimal value = v._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < u32.MIN) { throw new OverflowException($"Underflow! u32 value `{value}` cannot be converted to type u32."); }
                if (value > u32.MAX) { throw new OverflowException($"Overflow! u32 value `{value}` cannot be converted to type u32."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }
        
        return unchecked((uint)value);
    }

    /// <summary>
    /// Narrowing conversion from i8 to u32 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static explicit operator u32(i8 num) => u32.narrow_from(num);

    ///// <summary>
    ///// Narrowing conversion from sbyte to u32 when you don't expect data loss.
    ///// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    ///// or an exception will be thrown during simulation (if math mode is unsafe).
    ///// </summary>
    public static explicit operator u32(sbyte num) => u32.narrow_from(num);

    /// <summary>
    /// Narrowing conversion from i16 to u32 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static explicit operator u32(i16 num) => u32.narrow_from(num);

    ///// <summary>
    ///// Narrowing conversion from short to u32 when you don't expect data loss.
    ///// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    ///// or an exception will be thrown during simulation (if math mode is unsafe).
    ///// </summary>
    public static explicit operator u32(short num) => u32.narrow_from(num);

    /// <summary>
    /// Narrowing conversion from i32 to u32 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static explicit operator u32(i32 num) => u32.narrow_from(num);

    ///// <summary>
    ///// Narrowing conversion from int to u32 when you don't expect data loss.
    ///// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    ///// or an exception will be thrown during simulation (if math mode is unsafe).
    ///// </summary>
    public static explicit operator u32(int num) => u32.narrow_from(num);

    /// <summary>
    /// Narrowing conversion from i64 to u32 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static explicit operator u32(i64 num) => u32.narrow_from(num);

    ///// <summary>
    ///// Narrowing conversion from long to u32 when you don't expect data loss.
    ///// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    ///// or an exception will be thrown during simulation (if math mode is unsafe).
    ///// </summary>
    public static explicit operator u32(long num) => u32.narrow_from(num);

    /// <summary>
    /// Narrowing conversion from u64 to u32 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static explicit operator u32(u64 num) => u32.narrow_from(num);

    ///// <summary>
    ///// Narrowing conversion from ulong to u32 when you don't expect data loss.
    ///// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    ///// or an exception will be thrown during simulation (if math mode is unsafe).
    ///// </summary>
    public static explicit operator u32(ulong num) => u32.narrow_from(num);


    //################################################################
    // wrapping conversions (only for unsigned)
    //################################################################
    
    /// <summary>
    /// Safe explicit wrapping conversion. Truncates upper bits.
    /// </summary>
    public u16 wrap_u16 => unchecked((ushort)this._csReadValue);

    /// <summary>
    /// Safe explicit wrapping conversion. Truncates upper bits.
    /// </summary>
    public u8 wrap_u8 => unchecked((byte)this._csReadValue);

    //################################################################
    // comparisons
    //################################################################
    
    
    /// <summary>
    /// Both operands stay of type u32 during this operation (no implicit promotion to platform dependent int).<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator ==(u32 a, u32 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue == b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[==]]> operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator ==(u32 a, IHasI8 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue == b.value;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[==]]> operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator ==(u32 a, IHasI16 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue == b.value;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[==]]> operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator ==(u32 a, IHasI32 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue == b.value;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[==]]> operation, both operands are treated as u64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator ==(u32 a, u64 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue == b._csReadValue;
    }

    
    /// <summary>
    /// Both operands stay of type u32 during this operation (no implicit promotion to platform dependent int).<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator !=(u32 a, u32 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue != b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[!=]]> operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator !=(u32 a, IHasI8 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue != b.value;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[!=]]> operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator !=(u32 a, IHasI16 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue != b.value;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[!=]]> operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator !=(u32 a, IHasI32 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue != b.value;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[!=]]> operation, both operands are treated as u64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator !=(u32 a, u64 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue != b._csReadValue;
    }

    
    /// <summary>
    /// Both operands stay of type u32 during this operation (no implicit promotion to platform dependent int).<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator <(u32 a, u32 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue < b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[<]]> operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator <(u32 a, IHasI8 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue < b.value;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[<]]> operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator <(u32 a, IHasI16 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue < b.value;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[<]]> operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator <(u32 a, IHasI32 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue < b.value;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[<]]> operation, both operands are treated as u64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator <(u32 a, u64 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue < b._csReadValue;
    }

    
    /// <summary>
    /// Both operands stay of type u32 during this operation (no implicit promotion to platform dependent int).<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator <=(u32 a, u32 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue <= b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[<=]]> operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator <=(u32 a, IHasI8 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue <= b.value;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[<=]]> operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator <=(u32 a, IHasI16 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue <= b.value;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[<=]]> operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator <=(u32 a, IHasI32 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue <= b.value;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[<=]]> operation, both operands are treated as u64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator <=(u32 a, u64 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue <= b._csReadValue;
    }

    
    /// <summary>
    /// Both operands stay of type u32 during this operation (no implicit promotion to platform dependent int).<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator >(u32 a, u32 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue > b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[>]]> operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator >(u32 a, IHasI8 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue > b.value;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[>]]> operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator >(u32 a, IHasI16 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue > b.value;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[>]]> operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator >(u32 a, IHasI32 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue > b.value;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[>]]> operation, both operands are treated as u64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator >(u32 a, u64 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue > b._csReadValue;
    }

    
    /// <summary>
    /// Both operands stay of type u32 during this operation (no implicit promotion to platform dependent int).<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator >=(u32 a, u32 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue >= b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[>=]]> operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator >=(u32 a, IHasI8 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue >= b.value;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[>=]]> operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator >=(u32 a, IHasI16 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue >= b.value;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[>=]]> operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator >=(u32 a, IHasI32 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue >= b.value;
    }
    /// <summary>
    /// NOTE: before the <![CDATA[>=]]> operation, both operands are treated as u64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator >=(u32 a, u64 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue >= b._csReadValue;
    }


    
    /// <summary>
    /// Both operands stay of type u32 during this operation (no implicit promotion to platform dependent int).<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static u32 operator +(u32 a, u32 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (ulong)a._csReadValue + b._csReadValue; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < u32.MIN) { throw new OverflowException($"Underflow! `{a} (u32) + {b} (u32)` result `{value}` is beyond u32 type MIN limit of `{u32.MIN}`. Explicitly widen before `+` operation."); }
            if (value > u32.MAX) { throw new OverflowException($"Overflow! `{a} (u32) + {b} (u32)` result `{value}` is beyond u32 type MAX limit of `{u32.MAX}`. Explicitly widen before `+` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        u32 result = unchecked((uint)value);
        return result;
    }

    /// <summary>
    /// NOTE: before the <![CDATA[+]]> operation, both operands are treated as i64.<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i64 operator +(u32 a, IHasI8 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (long)a._csReadValue + b.value; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i64.MIN) { throw new OverflowException($"Underflow! `{a} (i64) + {b} (i64)` result `{value}` is beyond i64 type MIN limit of `{i64.MIN}`. Explicitly widen before `+` operation."); }
            if (value > i64.MAX) { throw new OverflowException($"Overflow! `{a} (i64) + {b} (i64)` result `{value}` is beyond i64 type MAX limit of `{i64.MAX}`. Explicitly widen before `+` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i64 result = unchecked((long)value);
        return result;
    }

    /// <summary>
    /// NOTE: before the <![CDATA[+]]> operation, both operands are treated as i64.<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i64 operator +(u32 a, IHasI16 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (long)a._csReadValue + b.value; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i64.MIN) { throw new OverflowException($"Underflow! `{a} (i64) + {b} (i64)` result `{value}` is beyond i64 type MIN limit of `{i64.MIN}`. Explicitly widen before `+` operation."); }
            if (value > i64.MAX) { throw new OverflowException($"Overflow! `{a} (i64) + {b} (i64)` result `{value}` is beyond i64 type MAX limit of `{i64.MAX}`. Explicitly widen before `+` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i64 result = unchecked((long)value);
        return result;
    }

    /// <summary>
    /// NOTE: before the <![CDATA[+]]> operation, both operands are treated as i64.<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i64 operator +(u32 a, IHasI32 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (long)a._csReadValue + b.value; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i64.MIN) { throw new OverflowException($"Underflow! `{a} (i64) + {b} (i64)` result `{value}` is beyond i64 type MIN limit of `{i64.MIN}`. Explicitly widen before `+` operation."); }
            if (value > i64.MAX) { throw new OverflowException($"Overflow! `{a} (i64) + {b} (i64)` result `{value}` is beyond i64 type MAX limit of `{i64.MAX}`. Explicitly widen before `+` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i64 result = unchecked((long)value);
        return result;
    }

    /// <summary>
    /// NOTE: before the <![CDATA[+]]> operation, both operands are treated as u64.<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static u64 operator +(u32 a, u64 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (decimal)a._csReadValue + b._csReadValue; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < u64.MIN) { throw new OverflowException($"Underflow! `{a} (u64) + {b} (u64)` result `{value}` is beyond u64 type MIN limit of `{u64.MIN}`. Explicitly widen before `+` operation."); }
            if (value > u64.MAX) { throw new OverflowException($"Overflow! `{a} (u64) + {b} (u64)` result `{value}` is beyond u64 type MAX limit of `{u64.MAX}`. Explicitly widen before `+` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        u64 result = unchecked((ulong)value);
        return result;
    }


    
    /// <summary>
    /// Both operands stay of type u32 during this operation (no implicit promotion to platform dependent int).<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static u32 operator -(u32 a, u32 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (ulong)a._csReadValue - b._csReadValue; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < u32.MIN) { throw new OverflowException($"Underflow! `{a} (u32) - {b} (u32)` result `{value}` is beyond u32 type MIN limit of `{u32.MIN}`. Explicitly widen before `-` operation."); }
            if (value > u32.MAX) { throw new OverflowException($"Overflow! `{a} (u32) - {b} (u32)` result `{value}` is beyond u32 type MAX limit of `{u32.MAX}`. Explicitly widen before `-` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        u32 result = unchecked((uint)value);
        return result;
    }

    /// <summary>
    /// NOTE: before the <![CDATA[-]]> operation, both operands are treated as i64.<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i64 operator -(u32 a, IHasI8 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (long)a._csReadValue - b.value; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i64.MIN) { throw new OverflowException($"Underflow! `{a} (i64) - {b} (i64)` result `{value}` is beyond i64 type MIN limit of `{i64.MIN}`. Explicitly widen before `-` operation."); }
            if (value > i64.MAX) { throw new OverflowException($"Overflow! `{a} (i64) - {b} (i64)` result `{value}` is beyond i64 type MAX limit of `{i64.MAX}`. Explicitly widen before `-` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i64 result = unchecked((long)value);
        return result;
    }

    /// <summary>
    /// NOTE: before the <![CDATA[-]]> operation, both operands are treated as i64.<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i64 operator -(u32 a, IHasI16 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (long)a._csReadValue - b.value; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i64.MIN) { throw new OverflowException($"Underflow! `{a} (i64) - {b} (i64)` result `{value}` is beyond i64 type MIN limit of `{i64.MIN}`. Explicitly widen before `-` operation."); }
            if (value > i64.MAX) { throw new OverflowException($"Overflow! `{a} (i64) - {b} (i64)` result `{value}` is beyond i64 type MAX limit of `{i64.MAX}`. Explicitly widen before `-` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i64 result = unchecked((long)value);
        return result;
    }

    /// <summary>
    /// NOTE: before the <![CDATA[-]]> operation, both operands are treated as i64.<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i64 operator -(u32 a, IHasI32 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (long)a._csReadValue - b.value; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i64.MIN) { throw new OverflowException($"Underflow! `{a} (i64) - {b} (i64)` result `{value}` is beyond i64 type MIN limit of `{i64.MIN}`. Explicitly widen before `-` operation."); }
            if (value > i64.MAX) { throw new OverflowException($"Overflow! `{a} (i64) - {b} (i64)` result `{value}` is beyond i64 type MAX limit of `{i64.MAX}`. Explicitly widen before `-` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i64 result = unchecked((long)value);
        return result;
    }

    /// <summary>
    /// NOTE: before the <![CDATA[-]]> operation, both operands are treated as u64.<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static u64 operator -(u32 a, u64 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (decimal)a._csReadValue - b._csReadValue; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < u64.MIN) { throw new OverflowException($"Underflow! `{a} (u64) - {b} (u64)` result `{value}` is beyond u64 type MIN limit of `{u64.MIN}`. Explicitly widen before `-` operation."); }
            if (value > u64.MAX) { throw new OverflowException($"Overflow! `{a} (u64) - {b} (u64)` result `{value}` is beyond u64 type MAX limit of `{u64.MAX}`. Explicitly widen before `-` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        u64 result = unchecked((ulong)value);
        return result;
    }


    
    /// <summary>
    /// Both operands stay of type u32 during this operation (no implicit promotion to platform dependent int).<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static u32 operator *(u32 a, u32 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (ulong)a._csReadValue * b._csReadValue; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < u32.MIN) { throw new OverflowException($"Underflow! `{a} (u32) * {b} (u32)` result `{value}` is beyond u32 type MIN limit of `{u32.MIN}`. Explicitly widen before `*` operation."); }
            if (value > u32.MAX) { throw new OverflowException($"Overflow! `{a} (u32) * {b} (u32)` result `{value}` is beyond u32 type MAX limit of `{u32.MAX}`. Explicitly widen before `*` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        u32 result = unchecked((uint)value);
        return result;
    }

    /// <summary>
    /// NOTE: before the <![CDATA[*]]> operation, both operands are treated as i64.<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i64 operator *(u32 a, IHasI8 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (long)a._csReadValue * b.value; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i64.MIN) { throw new OverflowException($"Underflow! `{a} (i64) * {b} (i64)` result `{value}` is beyond i64 type MIN limit of `{i64.MIN}`. Explicitly widen before `*` operation."); }
            if (value > i64.MAX) { throw new OverflowException($"Overflow! `{a} (i64) * {b} (i64)` result `{value}` is beyond i64 type MAX limit of `{i64.MAX}`. Explicitly widen before `*` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i64 result = unchecked((long)value);
        return result;
    }

    /// <summary>
    /// NOTE: before the <![CDATA[*]]> operation, both operands are treated as i64.<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i64 operator *(u32 a, IHasI16 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (long)a._csReadValue * b.value; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i64.MIN) { throw new OverflowException($"Underflow! `{a} (i64) * {b} (i64)` result `{value}` is beyond i64 type MIN limit of `{i64.MIN}`. Explicitly widen before `*` operation."); }
            if (value > i64.MAX) { throw new OverflowException($"Overflow! `{a} (i64) * {b} (i64)` result `{value}` is beyond i64 type MAX limit of `{i64.MAX}`. Explicitly widen before `*` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i64 result = unchecked((long)value);
        return result;
    }

    /// <summary>
    /// NOTE: before the <![CDATA[*]]> operation, both operands are treated as i64.<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i64 operator *(u32 a, IHasI32 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (long)a._csReadValue * b.value; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i64.MIN) { throw new OverflowException($"Underflow! `{a} (i64) * {b} (i64)` result `{value}` is beyond i64 type MIN limit of `{i64.MIN}`. Explicitly widen before `*` operation."); }
            if (value > i64.MAX) { throw new OverflowException($"Overflow! `{a} (i64) * {b} (i64)` result `{value}` is beyond i64 type MAX limit of `{i64.MAX}`. Explicitly widen before `*` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i64 result = unchecked((long)value);
        return result;
    }

    /// <summary>
    /// NOTE: before the <![CDATA[*]]> operation, both operands are treated as u64.<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static u64 operator *(u32 a, u64 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (decimal)a._csReadValue * b._csReadValue; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < u64.MIN) { throw new OverflowException($"Underflow! `{a} (u64) * {b} (u64)` result `{value}` is beyond u64 type MIN limit of `{u64.MIN}`. Explicitly widen before `*` operation."); }
            if (value > u64.MAX) { throw new OverflowException($"Overflow! `{a} (u64) * {b} (u64)` result `{value}` is beyond u64 type MAX limit of `{u64.MAX}`. Explicitly widen before `*` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        u64 result = unchecked((ulong)value);
        return result;
    }



    //################################################################
    // wrapping numeric methods (unsigned only for now)
    //################################################################
    
    
    /// <summary>
    /// Equivalent to `(uint32_t)(this value + number value)`.
    /// No error possible.
    /// </summary>
    public u32 wrap_add(u32 number)
    {
        var result = unchecked((uint)(this._csReadValue + number._csReadValue));
        return result;
    }


    
    /// <summary>
    /// Equivalent to `(uint32_t)(this value - number value)`.
    /// No error possible.
    /// </summary>
    public u32 wrap_sub(u32 number)
    {
        var result = unchecked((uint)(this._csReadValue - number._csReadValue));
        return result;
    }


    
    /// <summary>
    /// Equivalent to `(uint32_t)(this value * number value)`.
    /// No error possible.
    /// </summary>
    public u32 wrap_mul(u32 number)
    {
        var result = unchecked((uint)(this._csReadValue * number._csReadValue));
        return result;
    }


            
    //################################################################
    // shift methods (unsigned only for now)
    //################################################################
    
        
    /// <summary>
    /// Left shifts the bits discarding overflow bits without error.<br/>
    /// Shift does not change the value of this object (without an assignment).<br/>
    /// Transpiles to C99 code something like <code>(uint32_t)(my_num &lt;&lt; shift_amount)</code><br/>
    /// Sim exception or Error if shift by negative amount or amount larger than type (undefined C99 behaviors).
    /// Math mode is required to be specified to handle bad shift amounts.
    /// </summary>
    public u32 wrap_lshift(IHasI8 shift_amount)
    {
        ThrowIfMathModeNotSpecified();
        var shift_amount_value = shift_amount.value._csReadValue;
    
            if (shift_amount_value < 0)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Shift misuse! Shifting a value `{this._csReadValue}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        if (shift_amount_value >= 32)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Overshift! Shifting a u32 integer (value `{this._csReadValue}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        u32 result = unchecked((uint)(this._csReadValue << (byte)shift_amount_value));
        return result;
    }
    
    /// <summary>
    /// Left shifts the bits (error on overflow).<br/>
    /// Shift does not change the value of this object (without an assignment).<br/>
    /// Transpiles to C99 code something like <code>(uint32_t)(my_num &lt;&lt; shift_amount)</code><br/>
    /// Sim exception or Error if overflow or if shift by negative amount or amount larger than type (undefined C99 behaviors).
    /// Math mode is required to be specified to handle bad shift amounts.
    /// </summary>
    public static u32 operator <<(u32 a, IHasI8 shift_amount)
    {
        ThrowIfMathModeNotSpecified();
        var shift_amount_value = shift_amount.value._csReadValue;
    
            if (shift_amount_value < 0)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Shift misuse! Shifting a value `{a}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        if (shift_amount_value >= 32)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Overshift! Shifting a u32 integer (value `{a}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        var result = a._csValue << (byte)shift_amount_value; // shift_amount_value must fit in byte because of above checks
    
        if (result < a || result > u32.MAX)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Left shifting a u32 integer (value `{a._csReadValue}`) by `{shift_amount_value}` caused the value to overflow (promote first if needed).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.OverflowError());
                    return 0;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        return (uint)result;
    }
    
    /// <summary>
    /// Right shifts the bits (no overflow possible).<br/>
    /// Shift does not change the value of this object (without an assignment).<br/>
    /// Transpiles to C99 code something like <code>(uint32_t)(my_num &gt;&gt; shift_amount)</code><br/>
    /// Sim exception or if shift by negative amount or amount larger than type (undefined C99 behaviors).
    /// Math mode is required to be specified to handle bad shift amounts.
    /// </summary>
    public static u32 operator >>(u32 a, IHasI8 shift_amount)
    {
        ThrowIfMathModeNotSpecified();
        var shift_amount_value = shift_amount.value._csReadValue;
    
            if (shift_amount_value < 0)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Shift misuse! Shifting a value `{a}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        if (shift_amount_value >= 32)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Overshift! Shifting a u32 integer (value `{a}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        u32 result = unchecked((uint)(a._csReadValue >> (byte)shift_amount_value));
        return result;
    }
    
    /// <summary>
    /// Left shifts the bits discarding overflow bits without error.<br/>
    /// Shift does not change the value of this object (without an assignment).<br/>
    /// Transpiles to C99 code something like <code>(uint32_t)(my_num &lt;&lt; shift_amount)</code><br/>
    /// Sim exception or Error if shift by negative amount or amount larger than type (undefined C99 behaviors).
    /// Math mode is required to be specified to handle bad shift amounts.
    /// </summary>
    public u32 wrap_lshift(IHasI16 shift_amount)
    {
        ThrowIfMathModeNotSpecified();
        var shift_amount_value = shift_amount.value._csReadValue;
    
            if (shift_amount_value < 0)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Shift misuse! Shifting a value `{this._csReadValue}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        if (shift_amount_value >= 32)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Overshift! Shifting a u32 integer (value `{this._csReadValue}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        u32 result = unchecked((uint)(this._csReadValue << (byte)shift_amount_value));
        return result;
    }
    
    /// <summary>
    /// Left shifts the bits (error on overflow).<br/>
    /// Shift does not change the value of this object (without an assignment).<br/>
    /// Transpiles to C99 code something like <code>(uint32_t)(my_num &lt;&lt; shift_amount)</code><br/>
    /// Sim exception or Error if overflow or if shift by negative amount or amount larger than type (undefined C99 behaviors).
    /// Math mode is required to be specified to handle bad shift amounts.
    /// </summary>
    public static u32 operator <<(u32 a, IHasI16 shift_amount)
    {
        ThrowIfMathModeNotSpecified();
        var shift_amount_value = shift_amount.value._csReadValue;
    
            if (shift_amount_value < 0)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Shift misuse! Shifting a value `{a}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        if (shift_amount_value >= 32)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Overshift! Shifting a u32 integer (value `{a}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        var result = a._csValue << (byte)shift_amount_value; // shift_amount_value must fit in byte because of above checks
    
        if (result < a || result > u32.MAX)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Left shifting a u32 integer (value `{a._csReadValue}`) by `{shift_amount_value}` caused the value to overflow (promote first if needed).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.OverflowError());
                    return 0;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        return (uint)result;
    }
    
    /// <summary>
    /// Right shifts the bits (no overflow possible).<br/>
    /// Shift does not change the value of this object (without an assignment).<br/>
    /// Transpiles to C99 code something like <code>(uint32_t)(my_num &gt;&gt; shift_amount)</code><br/>
    /// Sim exception or if shift by negative amount or amount larger than type (undefined C99 behaviors).
    /// Math mode is required to be specified to handle bad shift amounts.
    /// </summary>
    public static u32 operator >>(u32 a, IHasI16 shift_amount)
    {
        ThrowIfMathModeNotSpecified();
        var shift_amount_value = shift_amount.value._csReadValue;
    
            if (shift_amount_value < 0)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Shift misuse! Shifting a value `{a}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        if (shift_amount_value >= 32)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Overshift! Shifting a u32 integer (value `{a}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        u32 result = unchecked((uint)(a._csReadValue >> (byte)shift_amount_value));
        return result;
    }
    
    /// <summary>
    /// Left shifts the bits discarding overflow bits without error.<br/>
    /// Shift does not change the value of this object (without an assignment).<br/>
    /// Transpiles to C99 code something like <code>(uint32_t)(my_num &lt;&lt; shift_amount)</code><br/>
    /// Sim exception or Error if shift by negative amount or amount larger than type (undefined C99 behaviors).
    /// Math mode is required to be specified to handle bad shift amounts.
    /// </summary>
    public u32 wrap_lshift(IHasI32 shift_amount)
    {
        ThrowIfMathModeNotSpecified();
        var shift_amount_value = shift_amount.value._csReadValue;
    
            if (shift_amount_value < 0)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Shift misuse! Shifting a value `{this._csReadValue}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        if (shift_amount_value >= 32)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Overshift! Shifting a u32 integer (value `{this._csReadValue}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        u32 result = unchecked((uint)(this._csReadValue << (byte)shift_amount_value));
        return result;
    }
    
    /// <summary>
    /// Left shifts the bits (error on overflow).<br/>
    /// Shift does not change the value of this object (without an assignment).<br/>
    /// Transpiles to C99 code something like <code>(uint32_t)(my_num &lt;&lt; shift_amount)</code><br/>
    /// Sim exception or Error if overflow or if shift by negative amount or amount larger than type (undefined C99 behaviors).
    /// Math mode is required to be specified to handle bad shift amounts.
    /// </summary>
    public static u32 operator <<(u32 a, IHasI32 shift_amount)
    {
        ThrowIfMathModeNotSpecified();
        var shift_amount_value = shift_amount.value._csReadValue;
    
            if (shift_amount_value < 0)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Shift misuse! Shifting a value `{a}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        if (shift_amount_value >= 32)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Overshift! Shifting a u32 integer (value `{a}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        var result = a._csValue << (byte)shift_amount_value; // shift_amount_value must fit in byte because of above checks
    
        if (result < a || result > u32.MAX)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Left shifting a u32 integer (value `{a._csReadValue}`) by `{shift_amount_value}` caused the value to overflow (promote first if needed).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.OverflowError());
                    return 0;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        return (uint)result;
    }
    
    /// <summary>
    /// Right shifts the bits (no overflow possible).<br/>
    /// Shift does not change the value of this object (without an assignment).<br/>
    /// Transpiles to C99 code something like <code>(uint32_t)(my_num &gt;&gt; shift_amount)</code><br/>
    /// Sim exception or if shift by negative amount or amount larger than type (undefined C99 behaviors).
    /// Math mode is required to be specified to handle bad shift amounts.
    /// </summary>
    public static u32 operator >>(u32 a, IHasI32 shift_amount)
    {
        ThrowIfMathModeNotSpecified();
        var shift_amount_value = shift_amount.value._csReadValue;
    
            if (shift_amount_value < 0)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Shift misuse! Shifting a value `{a}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        if (shift_amount_value >= 32)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Overshift! Shifting a u32 integer (value `{a}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        u32 result = unchecked((uint)(a._csReadValue >> (byte)shift_amount_value));
        return result;
    }
    
    /// <summary>
    /// Left shifts the bits discarding overflow bits without error.<br/>
    /// Shift does not change the value of this object (without an assignment).<br/>
    /// Transpiles to C99 code something like <code>(uint32_t)(my_num &lt;&lt; shift_amount)</code><br/>
    /// Sim exception or Error if shift by negative amount or amount larger than type (undefined C99 behaviors).
    /// Math mode is required to be specified to handle bad shift amounts.
    /// </summary>
    public u32 wrap_lshift(IHasI64 shift_amount)
    {
        ThrowIfMathModeNotSpecified();
        var shift_amount_value = shift_amount.value._csReadValue;
    
            if (shift_amount_value < 0)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Shift misuse! Shifting a value `{this._csReadValue}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        if (shift_amount_value >= 32)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Overshift! Shifting a u32 integer (value `{this._csReadValue}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        u32 result = unchecked((uint)(this._csReadValue << (byte)shift_amount_value));
        return result;
    }
    
    /// <summary>
    /// Left shifts the bits (error on overflow).<br/>
    /// Shift does not change the value of this object (without an assignment).<br/>
    /// Transpiles to C99 code something like <code>(uint32_t)(my_num &lt;&lt; shift_amount)</code><br/>
    /// Sim exception or Error if overflow or if shift by negative amount or amount larger than type (undefined C99 behaviors).
    /// Math mode is required to be specified to handle bad shift amounts.
    /// </summary>
    public static u32 operator <<(u32 a, IHasI64 shift_amount)
    {
        ThrowIfMathModeNotSpecified();
        var shift_amount_value = shift_amount.value._csReadValue;
    
            if (shift_amount_value < 0)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Shift misuse! Shifting a value `{a}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        if (shift_amount_value >= 32)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Overshift! Shifting a u32 integer (value `{a}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        var result = a._csValue << (byte)shift_amount_value; // shift_amount_value must fit in byte because of above checks
    
        if (result < a || result > u32.MAX)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Left shifting a u32 integer (value `{a._csReadValue}`) by `{shift_amount_value}` caused the value to overflow (promote first if needed).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.OverflowError());
                    return 0;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        return (uint)result;
    }
    
    /// <summary>
    /// Right shifts the bits (no overflow possible).<br/>
    /// Shift does not change the value of this object (without an assignment).<br/>
    /// Transpiles to C99 code something like <code>(uint32_t)(my_num &gt;&gt; shift_amount)</code><br/>
    /// Sim exception or if shift by negative amount or amount larger than type (undefined C99 behaviors).
    /// Math mode is required to be specified to handle bad shift amounts.
    /// </summary>
    public static u32 operator >>(u32 a, IHasI64 shift_amount)
    {
        ThrowIfMathModeNotSpecified();
        var shift_amount_value = shift_amount.value._csReadValue;
    
            if (shift_amount_value < 0)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Shift misuse! Shifting a value `{a}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        if (shift_amount_value >= 32)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Overshift! Shifting a u32 integer (value `{a}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        u32 result = unchecked((uint)(a._csReadValue >> (byte)shift_amount_value));
        return result;
    }
    
    /// <summary>
    /// Left shifts the bits discarding overflow bits without error.<br/>
    /// Shift does not change the value of this object (without an assignment).<br/>
    /// Transpiles to C99 code something like <code>(uint32_t)(my_num &lt;&lt; shift_amount)</code><br/>
    /// Sim exception or Error if shift by negative amount or amount larger than type (undefined C99 behaviors).
    /// Math mode is required to be specified to handle bad shift amounts.
    /// </summary>
    public u32 wrap_lshift(u8 shift_amount)
    {
        ThrowIfMathModeNotSpecified();
        var shift_amount_value = shift_amount._csReadValue;
    
            if (shift_amount_value < 0)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Shift misuse! Shifting a value `{this._csReadValue}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        if (shift_amount_value >= 32)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Overshift! Shifting a u32 integer (value `{this._csReadValue}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        u32 result = unchecked((uint)(this._csReadValue << (byte)shift_amount_value));
        return result;
    }
    
    /// <summary>
    /// Left shifts the bits (error on overflow).<br/>
    /// Shift does not change the value of this object (without an assignment).<br/>
    /// Transpiles to C99 code something like <code>(uint32_t)(my_num &lt;&lt; shift_amount)</code><br/>
    /// Sim exception or Error if overflow or if shift by negative amount or amount larger than type (undefined C99 behaviors).
    /// Math mode is required to be specified to handle bad shift amounts.
    /// </summary>
    public static u32 operator <<(u32 a, u8 shift_amount)
    {
        ThrowIfMathModeNotSpecified();
        var shift_amount_value = shift_amount._csReadValue;
    
            if (shift_amount_value < 0)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Shift misuse! Shifting a value `{a}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        if (shift_amount_value >= 32)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Overshift! Shifting a u32 integer (value `{a}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        var result = a._csValue << (byte)shift_amount_value; // shift_amount_value must fit in byte because of above checks
    
        if (result < a || result > u32.MAX)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Left shifting a u32 integer (value `{a._csReadValue}`) by `{shift_amount_value}` caused the value to overflow (promote first if needed).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.OverflowError());
                    return 0;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        return (uint)result;
    }
    
    /// <summary>
    /// Right shifts the bits (no overflow possible).<br/>
    /// Shift does not change the value of this object (without an assignment).<br/>
    /// Transpiles to C99 code something like <code>(uint32_t)(my_num &gt;&gt; shift_amount)</code><br/>
    /// Sim exception or if shift by negative amount or amount larger than type (undefined C99 behaviors).
    /// Math mode is required to be specified to handle bad shift amounts.
    /// </summary>
    public static u32 operator >>(u32 a, u8 shift_amount)
    {
        ThrowIfMathModeNotSpecified();
        var shift_amount_value = shift_amount._csReadValue;
    
            if (shift_amount_value < 0)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Shift misuse! Shifting a value `{a}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        if (shift_amount_value >= 32)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Overshift! Shifting a u32 integer (value `{a}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        u32 result = unchecked((uint)(a._csReadValue >> (byte)shift_amount_value));
        return result;
    }
    
    /// <summary>
    /// Left shifts the bits discarding overflow bits without error.<br/>
    /// Shift does not change the value of this object (without an assignment).<br/>
    /// Transpiles to C99 code something like <code>(uint32_t)(my_num &lt;&lt; shift_amount)</code><br/>
    /// Sim exception or Error if shift by negative amount or amount larger than type (undefined C99 behaviors).
    /// Math mode is required to be specified to handle bad shift amounts.
    /// </summary>
    public u32 wrap_lshift(u16 shift_amount)
    {
        ThrowIfMathModeNotSpecified();
        var shift_amount_value = shift_amount._csReadValue;
    
            if (shift_amount_value < 0)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Shift misuse! Shifting a value `{this._csReadValue}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        if (shift_amount_value >= 32)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Overshift! Shifting a u32 integer (value `{this._csReadValue}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        u32 result = unchecked((uint)(this._csReadValue << (byte)shift_amount_value));
        return result;
    }
    
    /// <summary>
    /// Left shifts the bits (error on overflow).<br/>
    /// Shift does not change the value of this object (without an assignment).<br/>
    /// Transpiles to C99 code something like <code>(uint32_t)(my_num &lt;&lt; shift_amount)</code><br/>
    /// Sim exception or Error if overflow or if shift by negative amount or amount larger than type (undefined C99 behaviors).
    /// Math mode is required to be specified to handle bad shift amounts.
    /// </summary>
    public static u32 operator <<(u32 a, u16 shift_amount)
    {
        ThrowIfMathModeNotSpecified();
        var shift_amount_value = shift_amount._csReadValue;
    
            if (shift_amount_value < 0)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Shift misuse! Shifting a value `{a}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        if (shift_amount_value >= 32)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Overshift! Shifting a u32 integer (value `{a}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        var result = a._csValue << (byte)shift_amount_value; // shift_amount_value must fit in byte because of above checks
    
        if (result < a || result > u32.MAX)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Left shifting a u32 integer (value `{a._csReadValue}`) by `{shift_amount_value}` caused the value to overflow (promote first if needed).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.OverflowError());
                    return 0;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        return (uint)result;
    }
    
    /// <summary>
    /// Right shifts the bits (no overflow possible).<br/>
    /// Shift does not change the value of this object (without an assignment).<br/>
    /// Transpiles to C99 code something like <code>(uint32_t)(my_num &gt;&gt; shift_amount)</code><br/>
    /// Sim exception or if shift by negative amount or amount larger than type (undefined C99 behaviors).
    /// Math mode is required to be specified to handle bad shift amounts.
    /// </summary>
    public static u32 operator >>(u32 a, u16 shift_amount)
    {
        ThrowIfMathModeNotSpecified();
        var shift_amount_value = shift_amount._csReadValue;
    
            if (shift_amount_value < 0)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Shift misuse! Shifting a value `{a}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        if (shift_amount_value >= 32)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Overshift! Shifting a u32 integer (value `{a}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        u32 result = unchecked((uint)(a._csReadValue >> (byte)shift_amount_value));
        return result;
    }
    
    /// <summary>
    /// Left shifts the bits discarding overflow bits without error.<br/>
    /// Shift does not change the value of this object (without an assignment).<br/>
    /// Transpiles to C99 code something like <code>(uint32_t)(my_num &lt;&lt; shift_amount)</code><br/>
    /// Sim exception or Error if shift by negative amount or amount larger than type (undefined C99 behaviors).
    /// Math mode is required to be specified to handle bad shift amounts.
    /// </summary>
    public u32 wrap_lshift(u32 shift_amount)
    {
        ThrowIfMathModeNotSpecified();
        var shift_amount_value = shift_amount._csReadValue;
    
            if (shift_amount_value < 0)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Shift misuse! Shifting a value `{this._csReadValue}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        if (shift_amount_value >= 32)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Overshift! Shifting a u32 integer (value `{this._csReadValue}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        u32 result = unchecked((uint)(this._csReadValue << (byte)shift_amount_value));
        return result;
    }
    
    /// <summary>
    /// Left shifts the bits (error on overflow).<br/>
    /// Shift does not change the value of this object (without an assignment).<br/>
    /// Transpiles to C99 code something like <code>(uint32_t)(my_num &lt;&lt; shift_amount)</code><br/>
    /// Sim exception or Error if overflow or if shift by negative amount or amount larger than type (undefined C99 behaviors).
    /// Math mode is required to be specified to handle bad shift amounts.
    /// </summary>
    public static u32 operator <<(u32 a, u32 shift_amount)
    {
        ThrowIfMathModeNotSpecified();
        var shift_amount_value = shift_amount._csReadValue;
    
            if (shift_amount_value < 0)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Shift misuse! Shifting a value `{a}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        if (shift_amount_value >= 32)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Overshift! Shifting a u32 integer (value `{a}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        var result = a._csValue << (byte)shift_amount_value; // shift_amount_value must fit in byte because of above checks
    
        if (result < a || result > u32.MAX)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Left shifting a u32 integer (value `{a._csReadValue}`) by `{shift_amount_value}` caused the value to overflow (promote first if needed).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.OverflowError());
                    return 0;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        return (uint)result;
    }
    
    /// <summary>
    /// Right shifts the bits (no overflow possible).<br/>
    /// Shift does not change the value of this object (without an assignment).<br/>
    /// Transpiles to C99 code something like <code>(uint32_t)(my_num &gt;&gt; shift_amount)</code><br/>
    /// Sim exception or if shift by negative amount or amount larger than type (undefined C99 behaviors).
    /// Math mode is required to be specified to handle bad shift amounts.
    /// </summary>
    public static u32 operator >>(u32 a, u32 shift_amount)
    {
        ThrowIfMathModeNotSpecified();
        var shift_amount_value = shift_amount._csReadValue;
    
            if (shift_amount_value < 0)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Shift misuse! Shifting a value `{a}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        if (shift_amount_value >= 32)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Overshift! Shifting a u32 integer (value `{a}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        u32 result = unchecked((uint)(a._csReadValue >> (byte)shift_amount_value));
        return result;
    }
    
    /// <summary>
    /// Left shifts the bits discarding overflow bits without error.<br/>
    /// Shift does not change the value of this object (without an assignment).<br/>
    /// Transpiles to C99 code something like <code>(uint32_t)(my_num &lt;&lt; shift_amount)</code><br/>
    /// Sim exception or Error if shift by negative amount or amount larger than type (undefined C99 behaviors).
    /// Math mode is required to be specified to handle bad shift amounts.
    /// </summary>
    public u32 wrap_lshift(u64 shift_amount)
    {
        ThrowIfMathModeNotSpecified();
        var shift_amount_value = shift_amount._csReadValue;
    
            if (shift_amount_value < 0)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Shift misuse! Shifting a value `{this._csReadValue}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        if (shift_amount_value >= 32)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Overshift! Shifting a u32 integer (value `{this._csReadValue}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        u32 result = unchecked((uint)(this._csReadValue << (byte)shift_amount_value));
        return result;
    }
    
    /// <summary>
    /// Left shifts the bits (error on overflow).<br/>
    /// Shift does not change the value of this object (without an assignment).<br/>
    /// Transpiles to C99 code something like <code>(uint32_t)(my_num &lt;&lt; shift_amount)</code><br/>
    /// Sim exception or Error if overflow or if shift by negative amount or amount larger than type (undefined C99 behaviors).
    /// Math mode is required to be specified to handle bad shift amounts.
    /// </summary>
    public static u32 operator <<(u32 a, u64 shift_amount)
    {
        ThrowIfMathModeNotSpecified();
        var shift_amount_value = shift_amount._csReadValue;
    
            if (shift_amount_value < 0)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Shift misuse! Shifting a value `{a}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        if (shift_amount_value >= 32)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Overshift! Shifting a u32 integer (value `{a}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        var result = a._csValue << (byte)shift_amount_value; // shift_amount_value must fit in byte because of above checks
    
        if (result < a || result > u32.MAX)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Left shifting a u32 integer (value `{a._csReadValue}`) by `{shift_amount_value}` caused the value to overflow (promote first if needed).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.OverflowError());
                    return 0;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        return (uint)result;
    }
    
    /// <summary>
    /// Right shifts the bits (no overflow possible).<br/>
    /// Shift does not change the value of this object (without an assignment).<br/>
    /// Transpiles to C99 code something like <code>(uint32_t)(my_num &gt;&gt; shift_amount)</code><br/>
    /// Sim exception or if shift by negative amount or amount larger than type (undefined C99 behaviors).
    /// Math mode is required to be specified to handle bad shift amounts.
    /// </summary>
    public static u32 operator >>(u32 a, u64 shift_amount)
    {
        ThrowIfMathModeNotSpecified();
        var shift_amount_value = shift_amount._csReadValue;
    
            if (shift_amount_value < 0)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Shift misuse! Shifting a value `{a}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        if (shift_amount_value >= 32)
        {
            switch (math.CurrentMode)
            {
                case math.Mode.Unsafe:
                    throw new OverflowException($"Overshift! Shifting a u32 integer (value `{a}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                case math.Mode.UserProvidedErr:
                    math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
            }
        }
    
        u32 result = unchecked((uint)(a._csReadValue >> (byte)shift_amount_value));
        return result;
    }
    

    
    //################################################################
    // bit methods (unsigned only for now)
    //################################################################
    
    
    /// <summary>
    /// Error free operation.<br/>
    /// Transpiles to <code>(u32)(a &amp; b)</code>.
    /// </summary>
    public static u32 operator &(u32 a, u8 b)
    {
        // ThrowIfMathModeNotSpecified(); // we don't care for error free operations
        var result = (uint)(a._csReadValue & b._csReadValue);
        return result;
    }
    
    /// <summary>
    /// Error free operation.<br/>
    /// Transpiles to <code>(u32)(a &amp; b)</code>.
    /// </summary>
    public static u32 operator &(u32 a, u16 b)
    {
        // ThrowIfMathModeNotSpecified(); // we don't care for error free operations
        var result = (uint)(a._csReadValue & b._csReadValue);
        return result;
    }
    
    /// <summary>
    /// Error free operation.<br/>
    /// Transpiles to <code>(u32)(a &amp; b)</code>.
    /// </summary>
    public static u32 operator &(u32 a, u32 b)
    {
        // ThrowIfMathModeNotSpecified(); // we don't care for error free operations
        var result = (uint)(a._csReadValue & b._csReadValue);
        return result;
    }
    
    /// <summary>
    /// Error free operation.<br/>
    /// Transpiles to <code>(u64)(a &amp; b)</code>.
    /// </summary>
    public static u64 operator &(u32 a, u64 b)
    {
        // ThrowIfMathModeNotSpecified(); // we don't care for error free operations
        var result = (ulong)(a._csReadValue & b._csReadValue);
        return result;
    }
    

    
    /// <summary>
    /// Error free operation.<br/>
    /// Transpiles to <code>(u32)(a ^ b)</code>.
    /// </summary>
    public static u32 operator ^(u32 a, u8 b)
    {
        // ThrowIfMathModeNotSpecified(); // we don't care for error free operations
        var result = (uint)(a._csReadValue ^ b._csReadValue);
        return result;
    }
    
    /// <summary>
    /// Error free operation.<br/>
    /// Transpiles to <code>(u32)(a ^ b)</code>.
    /// </summary>
    public static u32 operator ^(u32 a, u16 b)
    {
        // ThrowIfMathModeNotSpecified(); // we don't care for error free operations
        var result = (uint)(a._csReadValue ^ b._csReadValue);
        return result;
    }
    
    /// <summary>
    /// Error free operation.<br/>
    /// Transpiles to <code>(u32)(a ^ b)</code>.
    /// </summary>
    public static u32 operator ^(u32 a, u32 b)
    {
        // ThrowIfMathModeNotSpecified(); // we don't care for error free operations
        var result = (uint)(a._csReadValue ^ b._csReadValue);
        return result;
    }
    
    /// <summary>
    /// Error free operation.<br/>
    /// Transpiles to <code>(u64)(a ^ b)</code>.
    /// </summary>
    public static u64 operator ^(u32 a, u64 b)
    {
        // ThrowIfMathModeNotSpecified(); // we don't care for error free operations
        var result = (ulong)(a._csReadValue ^ b._csReadValue);
        return result;
    }
    

    
    /// <summary>
    /// Error free operation.<br/>
    /// Transpiles to <code>(u32)(a | b)</code>.
    /// </summary>
    public static u32 operator |(u32 a, u8 b)
    {
        // ThrowIfMathModeNotSpecified(); // we don't care for error free operations
        var result = (uint)(a._csReadValue | b._csReadValue);
        return result;
    }
    
    /// <summary>
    /// Error free operation.<br/>
    /// Transpiles to <code>(u32)(a | b)</code>.
    /// </summary>
    public static u32 operator |(u32 a, u16 b)
    {
        // ThrowIfMathModeNotSpecified(); // we don't care for error free operations
        var result = (uint)(a._csReadValue | b._csReadValue);
        return result;
    }
    
    /// <summary>
    /// Error free operation.<br/>
    /// Transpiles to <code>(u32)(a | b)</code>.
    /// </summary>
    public static u32 operator |(u32 a, u32 b)
    {
        // ThrowIfMathModeNotSpecified(); // we don't care for error free operations
        var result = (uint)(a._csReadValue | b._csReadValue);
        return result;
    }
    
    /// <summary>
    /// Error free operation.<br/>
    /// Transpiles to <code>(u64)(a | b)</code>.
    /// </summary>
    public static u64 operator |(u32 a, u64 b)
    {
        // ThrowIfMathModeNotSpecified(); // we don't care for error free operations
        var result = (ulong)(a._csReadValue | b._csReadValue);
        return result;
    }
    

    
    /// <summary>
    /// Error free operation.<br/>
    /// Transpiles to <code>(u32)(~)</code>.
    /// </summary>
    public static u32 operator ~(u32 a)
    {
        // ThrowIfMathModeNotSpecified(); // we don't care for error free operations
        var result = (u32)(uint)(~a._csReadValue); // have to cast to C# type first
        return result;
    }
    


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
        return obj_value == (uint)value;
    }
}