//NOTE! AUTO GENERATED FILE
using System;

#pragma warning disable IDE1006 // Naming Styles.
#pragma warning disable CS0652  // Useless comparison for integer types. Stuff like `u8 < 0`. Not a priority.

namespace fin.sim.lang;

public struct u16: IHasU16
{
    public const ushort MAX = 65535;
    public const ushort MIN = 0;

    /// <summary>
    /// C# backing value.
    /// </summary>
    internal ushort _csValue;

    public u16()
    {
    }

    private u16(ushort value)
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
    internal ushort _csReadValue => _csValue;

    public u16 value
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
    /// Implicit conversion from C# numeric type to fin numeric type.
    /// </summary>
    public static implicit operator u16(ushort num) { return new u16(num); }

    /// <summary>
    /// Implicit conversion from fin numeric type to C# numeric type.
    /// </summary>
    /// This is needed for technical reasons, but I don't remember them. Should be documented.
    public static implicit operator ushort(u16 num) { return num._csReadValue; }

    //################################################################
    // widening conversions
    //################################################################
    
    
    /// <summary>
    /// Safe explicit widening conversion.
    /// </summary>
    public u32 u32 => value;

    /// <summary>
    /// Safe explicit widening conversion.
    /// </summary>
    public u64 u64 => value;

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
    public static implicit operator u32(u16 num) { return num._csReadValue; }

    /// <summary>
    /// Safe implicit widening conversion.
    /// </summary>
    public static implicit operator u64(u16 num) { return num._csReadValue; }

    /// <summary>
    /// Safe implicit widening conversion.
    /// </summary>
    public static implicit operator i32(u16 num) { return num._csReadValue; }

    /// <summary>
    /// Safe implicit widening conversion.
    /// </summary>
    public static implicit operator i64(u16 num) { return num._csReadValue; }


    //################################################################
    // narrowing conversions
    //################################################################
    
    /// <summary>
    /// Potentially unsafe conversion from u16 to i16.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public i16 unsafe_to_i16()
    {
        ThrowIfMathModeNotSpecified();
        ushort value = this._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i16.MIN) { throw new OverflowException($"Underflow! u16 value `{value}` cannot be converted to type i16."); }
                if (value > i16.MAX) { throw new OverflowException($"Overflow! u16 value `{value}` cannot be converted to type i16."); }
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
    /// Potentially unsafe conversion from u16 to i8.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public i8 unsafe_to_i8()
    {
        ThrowIfMathModeNotSpecified();
        ushort value = this._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i8.MIN) { throw new OverflowException($"Underflow! u16 value `{value}` cannot be converted to type i8."); }
                if (value > i8.MAX) { throw new OverflowException($"Overflow! u16 value `{value}` cannot be converted to type i8."); }
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
    /// Potentially unsafe conversion from u16 to u8.
    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
    /// or an exception will be thrown during simulation (if math mode is unsafe).
    /// </summary>
    public u8 unsafe_to_u8()
    {
        ThrowIfMathModeNotSpecified();
        ushort value = this._csReadValue;

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < u8.MIN) { throw new OverflowException($"Underflow! u16 value `{value}` cannot be converted to type u8."); }
                if (value > u8.MAX) { throw new OverflowException($"Overflow! u16 value `{value}` cannot be converted to type u8."); }
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


    //################################################################
    // wrapping conversions (only for unsigned)
    //################################################################
    
    /// <summary>
    /// Safe explicit wrapping conversion. Truncates upper bits.
    /// </summary>
    public u8 wrap_u8 => unchecked((byte)this._csReadValue);

    //################################################################
    // comparisons
    //################################################################
    
    public static bool operator ==(u16 a, u16 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue == b._csReadValue;
        return result;
    }

    public static bool operator !=(u16 a, u16 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue != b._csReadValue;
        return result;
    }

    public static bool operator <(u16 a, u16 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue < b._csReadValue;
        return result;
    }

    public static bool operator <=(u16 a, u16 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue <= b._csReadValue;
        return result;
    }

    public static bool operator >(u16 a, u16 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue > b._csReadValue;
        return result;
    }

    public static bool operator >=(u16 a, u16 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue >= b._csReadValue;
        return result;
    }


    
    /// <summary>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.
    /// </summary>
    public static u16 operator +(u16 a, u16 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (uint)a._csReadValue + b._csReadValue; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < u16.MIN) { throw new OverflowException($"Underflow! `{a} (u16) + {b} (u16)` result `{value}` is beyond u16 type MIN limit of `{u16.MIN}`. Explicitly widen before `+` operation."); }
                if (value > u16.MAX) { throw new OverflowException($"Overflow! `{a} (u16) + {b} (u16)` result `{value}` is beyond u16 type MAX limit of `{u16.MAX}`. Explicitly widen before `+` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u16.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u16.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        u16 result = unchecked((ushort)value);
        return result;
    }

    /// <summary>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.
    /// </summary>
    public static i32 operator +(u16 a, IHasI8 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (int)a._csReadValue + b.value; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i32.MIN) { throw new OverflowException($"Underflow! `{a} (i32) + {b} (i32)` result `{value}` is beyond i32 type MIN limit of `{i32.MIN}`. Explicitly widen before `+` operation."); }
                if (value > i32.MAX) { throw new OverflowException($"Overflow! `{a} (i32) + {b} (i32)` result `{value}` is beyond i32 type MAX limit of `{i32.MAX}`. Explicitly widen before `+` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u16.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u16.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i32 result = unchecked((int)value);
        return result;
    }

    /// <summary>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.
    /// </summary>
    public static i32 operator +(u16 a, IHasI16 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (int)a._csReadValue + b.value; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i32.MIN) { throw new OverflowException($"Underflow! `{a} (i32) + {b} (i32)` result `{value}` is beyond i32 type MIN limit of `{i32.MIN}`. Explicitly widen before `+` operation."); }
                if (value > i32.MAX) { throw new OverflowException($"Overflow! `{a} (i32) + {b} (i32)` result `{value}` is beyond i32 type MAX limit of `{i32.MAX}`. Explicitly widen before `+` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u16.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u16.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i32 result = unchecked((int)value);
        return result;
    }

    /// <summary>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.
    /// </summary>
    public static u32 operator +(u16 a, u32 b)
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
                if (value < u16.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u16.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        u32 result = unchecked((uint)value);
        return result;
    }

    /// <summary>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.
    /// </summary>
    public static u64 operator +(u16 a, u64 b)
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
                if (value < u16.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u16.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        u64 result = unchecked((ulong)value);
        return result;
    }


    
    /// <summary>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.
    /// </summary>
    public static u16 operator -(u16 a, u16 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (uint)a._csReadValue - b._csReadValue; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < u16.MIN) { throw new OverflowException($"Underflow! `{a} (u16) - {b} (u16)` result `{value}` is beyond u16 type MIN limit of `{u16.MIN}`. Explicitly widen before `-` operation."); }
                if (value > u16.MAX) { throw new OverflowException($"Overflow! `{a} (u16) - {b} (u16)` result `{value}` is beyond u16 type MAX limit of `{u16.MAX}`. Explicitly widen before `-` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u16.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u16.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        u16 result = unchecked((ushort)value);
        return result;
    }

    /// <summary>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.
    /// </summary>
    public static i32 operator -(u16 a, IHasI8 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (int)a._csReadValue - b.value; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i32.MIN) { throw new OverflowException($"Underflow! `{a} (i32) - {b} (i32)` result `{value}` is beyond i32 type MIN limit of `{i32.MIN}`. Explicitly widen before `-` operation."); }
                if (value > i32.MAX) { throw new OverflowException($"Overflow! `{a} (i32) - {b} (i32)` result `{value}` is beyond i32 type MAX limit of `{i32.MAX}`. Explicitly widen before `-` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u16.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u16.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i32 result = unchecked((int)value);
        return result;
    }

    /// <summary>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.
    /// </summary>
    public static i32 operator -(u16 a, IHasI16 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (int)a._csReadValue - b.value; // use `var` as convenience. it will be int when operands are smaller than int.

        switch (math.CurrentMode)
        {
            case math.Mode.Unsafe:
                if (value < i32.MIN) { throw new OverflowException($"Underflow! `{a} (i32) - {b} (i32)` result `{value}` is beyond i32 type MIN limit of `{i32.MIN}`. Explicitly widen before `-` operation."); }
                if (value > i32.MAX) { throw new OverflowException($"Overflow! `{a} (i32) - {b} (i32)` result `{value}` is beyond i32 type MAX limit of `{i32.MAX}`. Explicitly widen before `-` operation."); }
                break;
            case math.Mode.UserProvidedErr:
                if (value < u16.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u16.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        i32 result = unchecked((int)value);
        return result;
    }

    /// <summary>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.
    /// </summary>
    public static u32 operator -(u16 a, u32 b)
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
                if (value < u16.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u16.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        u32 result = unchecked((uint)value);
        return result;
    }

    /// <summary>
    /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.
    /// When math mode is `user provided err`, this operation will add an error if the value won't fit.
    /// </summary>
    public static u64 operator -(u16 a, u64 b)
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
                if (value < u16.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                if (value > u16.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                break;
            default:
                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
        }

        u64 result = unchecked((ulong)value);
        return result;
    }




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
        return obj_value == (ushort)value;
    }
}