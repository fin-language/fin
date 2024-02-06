//NOTE! AUTO GENERATED FILE
using System;

#pragma warning disable IDE1006 // Naming Styles.
#pragma warning disable CS0652  // Useless comparison for integer types. Stuff like `u8 < 0`. Not a priority.

namespace finlang;

public struct i32: IHasI32
{
    public const int MAX = 2147483647;
    public const int MIN = -2147483648;

    /// <summary>
    /// C# backing value.
    /// </summary>
    internal int _csValue;

    public i32()
    {
    }

    private i32(int value)
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
    internal int _csReadValue => _csValue;

    public i32 value
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
    /// <code>var c = i32.from(2) + my_i32;</code>
    /// See https://github.com/fin-language/fin/issues/13
    /// </summary>
    public static i32 from(i32 value) => value;

    /// <summary>
    /// Implicit conversion from C# numeric type to fin numeric type.
    /// </summary>
    public static implicit operator i32(int num) { return new i32(num); }

    /// <summary>
    /// Implicit conversion from fin numeric type to C# numeric type.
    /// </summary>
    /// This is needed for technical reasons, but I don't remember them. Should be documented.
    public static implicit operator int(i32 num) { return num._csReadValue; }

    //################################################################
    // widening conversions
    //################################################################
    
    
    /// <summary>
    /// Safe type self declaration. See https://github.com/fin-language/fin/issues/21
    /// </summary>
    public i32 i32_ => value;

    /// <summary>
    /// Safe explicit widening conversion.
    /// </summary>
    public i64 i64 => value;


    /// <summary>
    /// Safe implicit widening conversion.
    /// </summary>
    public static implicit operator i64(i32 num) { return num._csReadValue; }


    //################################################################
    // narrowing conversions
    //################################################################
    
    /// <summary>
    /// Narrowing conversion from i32 to u32 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public u32 narrow_to_u32()
    {
        ThrowIfMathModeNotSpecified();
        int value = this._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < u32.MIN) { throw new OverflowException($"Underflow! i32 value `{value}` cannot be converted to type u32."); }
                if (value > u32.MAX) { throw new OverflowException($"Overflow! i32 value `{value}` cannot be converted to type u32."); }
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
    /// Narrowing conversion from i32 to i16 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public i16 narrow_to_i16()
    {
        ThrowIfMathModeNotSpecified();
        int value = this._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i16.MIN) { throw new OverflowException($"Underflow! i32 value `{value}` cannot be converted to type i16."); }
                if (value > i16.MAX) { throw new OverflowException($"Overflow! i32 value `{value}` cannot be converted to type i16."); }
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
    /// Narrowing conversion from i32 to u16 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public u16 narrow_to_u16()
    {
        ThrowIfMathModeNotSpecified();
        int value = this._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < u16.MIN) { throw new OverflowException($"Underflow! i32 value `{value}` cannot be converted to type u16."); }
                if (value > u16.MAX) { throw new OverflowException($"Overflow! i32 value `{value}` cannot be converted to type u16."); }
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
    /// Narrowing conversion from i32 to i8 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public i8 narrow_to_i8()
    {
        ThrowIfMathModeNotSpecified();
        int value = this._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i8.MIN) { throw new OverflowException($"Underflow! i32 value `{value}` cannot be converted to type i8."); }
                if (value > i8.MAX) { throw new OverflowException($"Overflow! i32 value `{value}` cannot be converted to type i8."); }
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
    /// Narrowing conversion from i32 to u8 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public u8 narrow_to_u8()
    {
        ThrowIfMathModeNotSpecified();
        int value = this._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < u8.MIN) { throw new OverflowException($"Underflow! i32 value `{value}` cannot be converted to type u8."); }
                if (value > u8.MAX) { throw new OverflowException($"Overflow! i32 value `{value}` cannot be converted to type u8."); }
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
    /// Narrowing conversion from i64 to i32 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static i32 narrow_from(i64 v)
    {
        ThrowIfMathModeNotSpecified();
        decimal value = v._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i32.MIN) { throw new OverflowException($"Underflow! i32 value `{value}` cannot be converted to type i32."); }
                if (value > i32.MAX) { throw new OverflowException($"Overflow! i32 value `{value}` cannot be converted to type i32."); }
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
    /// Narrowing conversion from u32 to i32 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static i32 narrow_from(u32 v)
    {
        ThrowIfMathModeNotSpecified();
        uint value = v._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i32.MIN) { throw new OverflowException($"Underflow! i32 value `{value}` cannot be converted to type i32."); }
                if (value > i32.MAX) { throw new OverflowException($"Overflow! i32 value `{value}` cannot be converted to type i32."); }
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
    /// Narrowing conversion from u64 to i32 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static i32 narrow_from(u64 v)
    {
        ThrowIfMathModeNotSpecified();
        decimal value = v._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i32.MIN) { throw new OverflowException($"Underflow! i32 value `{value}` cannot be converted to type i32."); }
                if (value > i32.MAX) { throw new OverflowException($"Overflow! i32 value `{value}` cannot be converted to type i32."); }
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
    /// Narrowing conversion from i64 to i32 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static explicit operator i32(i64 num) => i32.narrow_from(num);

    ///// <summary>
    ///// Narrowing conversion from long to i32 when you don't expect data loss.
    ///// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    ///// or an exception will be thrown during simulation (if math mode is unsafe).
    ///// </summary>
    public static explicit operator i32(long num) => i32.narrow_from(num);

    /// <summary>
    /// Narrowing conversion from u32 to i32 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static explicit operator i32(u32 num) => i32.narrow_from(num);

    ///// <summary>
    ///// Narrowing conversion from uint to i32 when you don't expect data loss.
    ///// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    ///// or an exception will be thrown during simulation (if math mode is unsafe).
    ///// </summary>
    public static explicit operator i32(uint num) => i32.narrow_from(num);

    /// <summary>
    /// Narrowing conversion from u64 to i32 when you don't expect data loss.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public static explicit operator i32(u64 num) => i32.narrow_from(num);

    ///// <summary>
    ///// Narrowing conversion from ulong to i32 when you don't expect data loss.
    ///// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    ///// or an exception will be thrown during simulation (if math mode is unsafe).
    ///// </summary>
    public static explicit operator i32(ulong num) => i32.narrow_from(num);


    //################################################################
    // wrapping conversions (only for unsigned)
    //################################################################
    
    

    //################################################################
    // comparisons
    //################################################################
    
    
    /// <summary>
    /// Both operands stay of type i32 during this operation (no implicit promotion to platform dependent int).<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator ==(i32 a, i32 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue == b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the `==` operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator ==(i32 a, i64 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue == b._csReadValue;
    }

    
    /// <summary>
    /// Both operands stay of type i32 during this operation (no implicit promotion to platform dependent int).<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator !=(i32 a, i32 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue != b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the `!=` operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator !=(i32 a, i64 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue != b._csReadValue;
    }

    
    /// <summary>
    /// Both operands stay of type i32 during this operation (no implicit promotion to platform dependent int).<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator <(i32 a, i32 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue < b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the `<` operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator <(i32 a, i64 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue < b._csReadValue;
    }

    
    /// <summary>
    /// Both operands stay of type i32 during this operation (no implicit promotion to platform dependent int).<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator <=(i32 a, i32 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue <= b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the `<=` operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator <=(i32 a, i64 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue <= b._csReadValue;
    }

    
    /// <summary>
    /// Both operands stay of type i32 during this operation (no implicit promotion to platform dependent int).<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator >(i32 a, i32 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue > b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the `>` operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator >(i32 a, i64 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue > b._csReadValue;
    }

    
    /// <summary>
    /// Both operands stay of type i32 during this operation (no implicit promotion to platform dependent int).<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator >=(i32 a, i32 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue >= b._csReadValue;
    }
    /// <summary>
    /// NOTE: before the `>=` operation, both operands are treated as i64.<br/>
    /// Error free operation.
    /// </summary>
    public static bool operator >=(i32 a, i64 b)
    {
        //ThrowIfMathModeNotSpecified(); // not required as this is error free
        return a._csReadValue >= b._csReadValue;
    }


    
    /// <summary>
    /// Both operands stay of type i32 during this operation (no implicit promotion to platform dependent int).<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i32 operator +(i32 a, i32 b)
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
                if (value < i32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i32 result = unchecked((int)value);
        return result;
    }

    /// <summary>
    /// NOTE: before the `+` operation, both operands are treated as i64.<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i64 operator +(i32 a, i64 b)
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
                if (value < i32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i64 result = unchecked((long)value);
        return result;
    }


    
    /// <summary>
    /// Both operands stay of type i32 during this operation (no implicit promotion to platform dependent int).<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i32 operator -(i32 a, i32 b)
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
                if (value < i32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i32 result = unchecked((int)value);
        return result;
    }

    /// <summary>
    /// NOTE: before the `-` operation, both operands are treated as i64.<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i64 operator -(i32 a, i64 b)
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
                if (value < i32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i64 result = unchecked((long)value);
        return result;
    }


    
    /// <summary>
    /// Both operands stay of type i32 during this operation (no implicit promotion to platform dependent int).<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i32 operator *(i32 a, i32 b)
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
                if (value < i32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i32 result = unchecked((int)value);
        return result;
    }

    /// <summary>
    /// NOTE: before the `*` operation, both operands are treated as i64.<br/>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
    /// </summary>
    public static i64 operator *(i32 a, i64 b)
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
                if (value < i32.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > i32.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i64 result = unchecked((long)value);
        return result;
    }



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
        return obj_value == (int)value;
    }
}