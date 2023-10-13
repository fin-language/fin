//NOTE! AUTO GENERATED FILE
using System;

#pragma warning disable IDE1006 // Naming Styles.
#pragma warning disable CS0652  // Useless comparison for integer types. Stuff like `u8 < 0`. Not a priority.

namespace fin.sim.lang;

public struct u32: IHasU32
{
    public const uint MAX = 4294967295;
    public const uint MIN = 0;

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
    /// Potentially unsafe conversion from u32 to i32.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public i32 unsafe_to_i32()
    {
        ThrowIfMathModeNotSpecified();
        uint csValue = this._csReadValue;
        if (csValue > i32.MAX || csValue < i32.MIN)
        {
            throw new OverflowException($"u32 value `{csValue}` cannot be converted to type i32.");
        }
        return (int)csValue;
    }

    /// <summary>
    /// Potentially unsafe conversion from u32 to i16.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public i16 unsafe_to_i16()
    {
        ThrowIfMathModeNotSpecified();
        uint csValue = this._csReadValue;
        if (csValue > i16.MAX || csValue < i16.MIN)
        {
            throw new OverflowException($"u32 value `{csValue}` cannot be converted to type i16.");
        }
        return (short)csValue;
    }

    /// <summary>
    /// Potentially unsafe conversion from u32 to u16.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public u16 unsafe_to_u16()
    {
        ThrowIfMathModeNotSpecified();
        uint csValue = this._csReadValue;
        if (csValue > u16.MAX || csValue < u16.MIN)
        {
            throw new OverflowException($"u32 value `{csValue}` cannot be converted to type u16.");
        }
        return (ushort)csValue;
    }

    /// <summary>
    /// Potentially unsafe conversion from u32 to i8.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public i8 unsafe_to_i8()
    {
        ThrowIfMathModeNotSpecified();
        uint csValue = this._csReadValue;
        if (csValue > i8.MAX || csValue < i8.MIN)
        {
            throw new OverflowException($"u32 value `{csValue}` cannot be converted to type i8.");
        }
        return (sbyte)csValue;
    }

    /// <summary>
    /// Potentially unsafe conversion from u32 to u8.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public u8 unsafe_to_u8()
    {
        ThrowIfMathModeNotSpecified();
        uint csValue = this._csReadValue;
        if (csValue > u8.MAX || csValue < u8.MIN)
        {
            throw new OverflowException($"u32 value `{csValue}` cannot be converted to type u8.");
        }
        return (byte)csValue;
    }


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
    
    public static bool operator ==(u32 a, u32 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue == b._csReadValue;
        return result;
    }

    public static bool operator !=(u32 a, u32 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue != b._csReadValue;
        return result;
    }

    public static bool operator <(u32 a, u32 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue < b._csReadValue;
        return result;
    }

    public static bool operator <=(u32 a, u32 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue <= b._csReadValue;
        return result;
    }

    public static bool operator >(u32 a, u32 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue > b._csReadValue;
        return result;
    }

    public static bool operator >=(u32 a, u32 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue >= b._csReadValue;
        return result;
    }


    
    public static u32 operator +(u32 a, u32 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (ulong)a._csReadValue + b._csReadValue; // use `var` as convenience. it will be int when operands are smaller than int.
        if (value < u32.MIN) { throw new OverflowException($"Underflow! `{a} (u32) + {b} (u32)` result `{value}` is beyond u32 type MIN limit of `{u32.MIN}`. Explicitly widen before `+` operation."); }
        if (value > u32.MAX) { throw new OverflowException($"Overflow! `{a} (u32) + {b} (u32)` result `{value}` is beyond u32 type MAX limit of `{u32.MAX}`. Explicitly widen before `+` operation."); }
        u32 result = (uint)value;
        return result;
    }
    public static i64 operator +(u32 a, IHasI8 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (long)a._csReadValue + b.value; // use `var` as convenience. it will be int when operands are smaller than int.
        if (value < i64.MIN) { throw new OverflowException($"Underflow! `{a} (i64) + {b} (i64)` result `{value}` is beyond i64 type MIN limit of `{i64.MIN}`. Explicitly widen before `+` operation."); }
        if (value > i64.MAX) { throw new OverflowException($"Overflow! `{a} (i64) + {b} (i64)` result `{value}` is beyond i64 type MAX limit of `{i64.MAX}`. Explicitly widen before `+` operation."); }
        i64 result = (long)value;
        return result;
    }
    public static i64 operator +(u32 a, IHasI16 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (long)a._csReadValue + b.value; // use `var` as convenience. it will be int when operands are smaller than int.
        if (value < i64.MIN) { throw new OverflowException($"Underflow! `{a} (i64) + {b} (i64)` result `{value}` is beyond i64 type MIN limit of `{i64.MIN}`. Explicitly widen before `+` operation."); }
        if (value > i64.MAX) { throw new OverflowException($"Overflow! `{a} (i64) + {b} (i64)` result `{value}` is beyond i64 type MAX limit of `{i64.MAX}`. Explicitly widen before `+` operation."); }
        i64 result = (long)value;
        return result;
    }
    public static i64 operator +(u32 a, IHasI32 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (long)a._csReadValue + b.value; // use `var` as convenience. it will be int when operands are smaller than int.
        if (value < i64.MIN) { throw new OverflowException($"Underflow! `{a} (i64) + {b} (i64)` result `{value}` is beyond i64 type MIN limit of `{i64.MIN}`. Explicitly widen before `+` operation."); }
        if (value > i64.MAX) { throw new OverflowException($"Overflow! `{a} (i64) + {b} (i64)` result `{value}` is beyond i64 type MAX limit of `{i64.MAX}`. Explicitly widen before `+` operation."); }
        i64 result = (long)value;
        return result;
    }
    public static u64 operator +(u32 a, u64 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (decimal)a._csReadValue + b._csReadValue; // use `var` as convenience. it will be int when operands are smaller than int.
        if (value < u64.MIN) { throw new OverflowException($"Underflow! `{a} (u64) + {b} (u64)` result `{value}` is beyond u64 type MIN limit of `{u64.MIN}`. Explicitly widen before `+` operation."); }
        if (value > u64.MAX) { throw new OverflowException($"Overflow! `{a} (u64) + {b} (u64)` result `{value}` is beyond u64 type MAX limit of `{u64.MAX}`. Explicitly widen before `+` operation."); }
        u64 result = (ulong)value;
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
        return obj_value == (uint)value;
    }
}