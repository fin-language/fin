//NOTE! AUTO GENERATED FILE
using System;

#pragma warning disable IDE1006 // Naming Styles.
#pragma warning disable CS0652  // Useless comparison for integer types. Stuff like `u8 < 0`. Not a priority.

namespace fin.sim.lang;

public struct i64: IHasI64
{
    public const long MAX = 9223372036854775807;
    public const long MIN = -9223372036854775808;

    /// <summary>
    /// C# backing value.
    /// </summary>
    internal long _csValue;

    public i64()
    {
    }

    private i64(long value)
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
    internal long _csReadValue => _csValue;

    public i64 value
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
    public static implicit operator i64(long num) { return new i64(num); }

    /// <summary>
    /// Implicit conversion from fin numeric type to C# numeric type.
    /// </summary>
    /// This is needed for technical reasons, but I don't remember them. Should be documented.
    public static implicit operator long(i64 num) { return num._csReadValue; }

    //################################################################
    // widening conversions
    //################################################################
    
    

    


    //################################################################
    // narrowing conversions
    //################################################################
    
    /// <summary>
    /// Potentially unsafe conversion from i64 to u64.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public u64 unsafe_to_u64()
    {
        ThrowIfMathModeNotSpecified();
        long csValue = this._csReadValue;
        decimal dv = csValue; // use decimal type when C# primitives are too small
        if (dv > u64.MAX || dv < u64.MIN)
        {
            throw new OverflowException($"i64 value `{csValue}` cannot be converted to type u64.");
        }
        return (ulong)csValue;
    }

    /// <summary>
    /// Potentially unsafe conversion from i64 to i32.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public i32 unsafe_to_i32()
    {
        ThrowIfMathModeNotSpecified();
        long csValue = this._csReadValue;
        decimal dv = csValue; // use decimal type when C# primitives are too small
        if (dv > i32.MAX || dv < i32.MIN)
        {
            throw new OverflowException($"i64 value `{csValue}` cannot be converted to type i32.");
        }
        return (int)csValue;
    }

    /// <summary>
    /// Potentially unsafe conversion from i64 to u32.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public u32 unsafe_to_u32()
    {
        ThrowIfMathModeNotSpecified();
        long csValue = this._csReadValue;
        decimal dv = csValue; // use decimal type when C# primitives are too small
        if (dv > u32.MAX || dv < u32.MIN)
        {
            throw new OverflowException($"i64 value `{csValue}` cannot be converted to type u32.");
        }
        return (uint)csValue;
    }

    /// <summary>
    /// Potentially unsafe conversion from i64 to i16.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public i16 unsafe_to_i16()
    {
        ThrowIfMathModeNotSpecified();
        long csValue = this._csReadValue;
        decimal dv = csValue; // use decimal type when C# primitives are too small
        if (dv > i16.MAX || dv < i16.MIN)
        {
            throw new OverflowException($"i64 value `{csValue}` cannot be converted to type i16.");
        }
        return (short)csValue;
    }

    /// <summary>
    /// Potentially unsafe conversion from i64 to u16.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public u16 unsafe_to_u16()
    {
        ThrowIfMathModeNotSpecified();
        long csValue = this._csReadValue;
        decimal dv = csValue; // use decimal type when C# primitives are too small
        if (dv > u16.MAX || dv < u16.MIN)
        {
            throw new OverflowException($"i64 value `{csValue}` cannot be converted to type u16.");
        }
        return (ushort)csValue;
    }

    /// <summary>
    /// Potentially unsafe conversion from i64 to i8.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public i8 unsafe_to_i8()
    {
        ThrowIfMathModeNotSpecified();
        long csValue = this._csReadValue;
        decimal dv = csValue; // use decimal type when C# primitives are too small
        if (dv > i8.MAX || dv < i8.MIN)
        {
            throw new OverflowException($"i64 value `{csValue}` cannot be converted to type i8.");
        }
        return (sbyte)csValue;
    }

    /// <summary>
    /// Potentially unsafe conversion from i64 to u8.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public u8 unsafe_to_u8()
    {
        ThrowIfMathModeNotSpecified();
        long csValue = this._csReadValue;
        decimal dv = csValue; // use decimal type when C# primitives are too small
        if (dv > u8.MAX || dv < u8.MIN)
        {
            throw new OverflowException($"i64 value `{csValue}` cannot be converted to type u8.");
        }
        return (byte)csValue;
    }


    //################################################################
    // wrapping conversions (only for unsigned)
    //################################################################
    
    /// <summary>
    /// Safe explicit wrapping conversion. Truncates upper bits.
    /// </summary>
    public u64 wrap_u64 => unchecked((ulong)this._csReadValue);

    /// <summary>
    /// Safe explicit wrapping conversion. Truncates upper bits.
    /// </summary>
    public u32 wrap_u32 => unchecked((uint)this._csReadValue);

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
    
    public static bool operator ==(i64 a, i64 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue == b._csReadValue;
        return result;
    }

    public static bool operator !=(i64 a, i64 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue != b._csReadValue;
        return result;
    }

    public static bool operator <(i64 a, i64 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue < b._csReadValue;
        return result;
    }

    public static bool operator <=(i64 a, i64 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue <= b._csReadValue;
        return result;
    }

    public static bool operator >(i64 a, i64 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue > b._csReadValue;
        return result;
    }

    public static bool operator >=(i64 a, i64 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue >= b._csReadValue;
        return result;
    }


    
    public static i64 operator +(i64 a, i64 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = (decimal)a._csReadValue + b._csReadValue; // use `var` as convenience. it will be int when operands are smaller than int.
        if (value < i64.MIN) { throw new OverflowException($"Underflow! `{a} (i64) + {b} (i64)` result `{value}` is beyond i64 type MIN limit of `{i64.MIN}`. Explicitly widen before `+` operation."); }
        if (value > i64.MAX) { throw new OverflowException($"Overflow! `{a} (i64) + {b} (i64)` result `{value}` is beyond i64 type MAX limit of `{i64.MAX}`. Explicitly widen before `+` operation."); }
        i64 result = (long)value;
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
        return obj_value == (long)value;
    }
}