//NOTE! AUTO GENERATED FILE
using System;

#pragma warning disable IDE1006 // Naming Styles.
#pragma warning disable CS0652  // Useless comparison for integer types. Stuff like `u8 < 0`. Not a priority.

namespace fin.sim.lang;

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
        Math.ThrowIfModeNotSpecified();
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
    /// Potentially unsafe conversion from i32 to u32.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public u32 unsafe_to_u32()
    {
        ThrowIfMathModeNotSpecified();
        int csValue = this._csReadValue;
        if (csValue > u32.MAX || csValue < u32.MIN)
        {
            throw new OverflowException($"i32 value `{csValue}` cannot be converted to type u32.");
        }
        return (uint)csValue;
    }

    /// <summary>
    /// Potentially unsafe conversion from i32 to i16.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public i16 unsafe_to_i16()
    {
        ThrowIfMathModeNotSpecified();
        int csValue = this._csReadValue;
        if (csValue > i16.MAX || csValue < i16.MIN)
        {
            throw new OverflowException($"i32 value `{csValue}` cannot be converted to type i16.");
        }
        return (short)csValue;
    }

    /// <summary>
    /// Potentially unsafe conversion from i32 to u16.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public u16 unsafe_to_u16()
    {
        ThrowIfMathModeNotSpecified();
        int csValue = this._csReadValue;
        if (csValue > u16.MAX || csValue < u16.MIN)
        {
            throw new OverflowException($"i32 value `{csValue}` cannot be converted to type u16.");
        }
        return (ushort)csValue;
    }

    /// <summary>
    /// Potentially unsafe conversion from i32 to i8.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public i8 unsafe_to_i8()
    {
        ThrowIfMathModeNotSpecified();
        int csValue = this._csReadValue;
        if (csValue > i8.MAX || csValue < i8.MIN)
        {
            throw new OverflowException($"i32 value `{csValue}` cannot be converted to type i8.");
        }
        return (sbyte)csValue;
    }

    /// <summary>
    /// Potentially unsafe conversion from i32 to u8.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public u8 unsafe_to_u8()
    {
        ThrowIfMathModeNotSpecified();
        int csValue = this._csReadValue;
        if (csValue > u8.MAX || csValue < u8.MIN)
        {
            throw new OverflowException($"i32 value `{csValue}` cannot be converted to type u8.");
        }
        return (byte)csValue;
    }


    //################################################################
    // wrapping conversions (only for unsigned)
    //################################################################
    
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
    
    public static bool operator ==(i32 a, i32 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue == b._csReadValue;
        return result;
    }

    public static bool operator !=(i32 a, i32 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue != b._csReadValue;
        return result;
    }

    public static bool operator <(i32 a, i32 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue < b._csReadValue;
        return result;
    }

    public static bool operator <=(i32 a, i32 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue <= b._csReadValue;
        return result;
    }

    public static bool operator >(i32 a, i32 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue > b._csReadValue;
        return result;
    }

    public static bool operator >=(i32 a, i32 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue >= b._csReadValue;
        return result;
    }


    
    public static i32 operator +(i32 a, i32 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = a._csReadValue + b._csReadValue;
        if (value < i32.MIN) { throw new OverflowException($"Underflow! `{a} (i32) + {b} (i32)` result `{value}` is beyond i32 type MIN limit of `{i32.MIN}`. Explicitly widen before `+` operation."); }
        if (value > i32.MAX) { throw new OverflowException($"Overflow! `{a} (i32) + {b} (i32)` result `{value}` is beyond i32 type MAX limit of `{i32.MAX}`. Explicitly widen before `+` operation."); }
        i32 result = (int)value;
        return result;
    }
    public static i64 operator +(i32 a, i64 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = a._csReadValue + b._csReadValue;
        
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
        return obj_value == (int)value;
    }
}