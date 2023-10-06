//NOTE! AUTO GENERATED FILE
using System;

#pragma warning disable IDE1006 // Naming Styles.
#pragma warning disable CS0652  // Useless comparison for integer types. Stuff like `u8 < 0`. Not a priority.

namespace fin.sim.lang;

public struct i8: IHasI8
{
    public const sbyte MAX = 127;
    public const sbyte MIN = -128;

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
    /// Potentially unsafe conversion from i8 to u8.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public u8 unsafe_to_u8()
    {
        sbyte csValue = this._csReadValue;
        if (csValue > u8.MAX || csValue < u8.MIN)
        {
            throw new OverflowException($"i8 value `{csValue}` cannot be converted to type u8.");
        }
        return (byte)csValue;
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
    
    public static bool operator ==(i8 a, i8 b)
    {
        var result = a._csReadValue == b._csReadValue;
        return result;
    }

    public static bool operator !=(i8 a, i8 b)
    {
        var result = a._csReadValue != b._csReadValue;
        return result;
    }

    public static bool operator <(i8 a, i8 b)
    {
        var result = a._csReadValue < b._csReadValue;
        return result;
    }

    public static bool operator <=(i8 a, i8 b)
    {
        var result = a._csReadValue <= b._csReadValue;
        return result;
    }

    public static bool operator >(i8 a, i8 b)
    {
        var result = a._csReadValue > b._csReadValue;
        return result;
    }

    public static bool operator >=(i8 a, i8 b)
    {
        var result = a._csReadValue >= b._csReadValue;
        return result;
    }


    
    public static i8 operator +(i8 a, i8 b)
    {
        var value = a._csReadValue + b._csReadValue;
        if (value < i8.MIN) { throw new OverflowException($"Underflow! `{a} (i8) + {b} (i8)` result `{value}` is beyond i8 type MIN limit of `{i8.MIN}`. Explicitly widen before `+` operation."); }
        if (value > i8.MAX) { throw new OverflowException($"Overflow! `{a} (i8) + {b} (i8)` result `{value}` is beyond i8 type MAX limit of `{i8.MAX}`. Explicitly widen before `+` operation."); }
        i8 result = (sbyte)value;
        return result;
    }
    public static i16 operator +(i8 a, i16 b)
    {
        var value = a._csReadValue + b._csReadValue;
        if (value < i16.MIN) { throw new OverflowException($"Underflow! `{a} (i16) + {b} (i16)` result `{value}` is beyond i16 type MIN limit of `{i16.MIN}`. Explicitly widen before `+` operation."); }
        if (value > i16.MAX) { throw new OverflowException($"Overflow! `{a} (i16) + {b} (i16)` result `{value}` is beyond i16 type MAX limit of `{i16.MAX}`. Explicitly widen before `+` operation."); }
        i16 result = (short)value;
        return result;
    }
    public static i32 operator +(i8 a, i32 b)
    {
        var value = a._csReadValue + b._csReadValue;
        if (value < i32.MIN) { throw new OverflowException($"Underflow! `{a} (i32) + {b} (i32)` result `{value}` is beyond i32 type MIN limit of `{i32.MIN}`. Explicitly widen before `+` operation."); }
        if (value > i32.MAX) { throw new OverflowException($"Overflow! `{a} (i32) + {b} (i32)` result `{value}` is beyond i32 type MAX limit of `{i32.MAX}`. Explicitly widen before `+` operation."); }
        i32 result = (int)value;
        return result;
    }
    public static i64 operator +(i8 a, i64 b)
    {
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
        return obj_value == (sbyte)value;
    }
}