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
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public i16 unsafe_to_i16()
    {
        ushort csValue = this._csReadValue;
        if (csValue > i16.MAX || csValue < i16.MIN)
        {
            throw new OverflowException($"u16 value `{csValue}` cannot be converted to type i16.");
        }
        return (short)csValue;
    }

    /// <summary>
    /// Potentially unsafe conversion from u16 to i8.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public i8 unsafe_to_i8()
    {
        ushort csValue = this._csReadValue;
        if (csValue > i8.MAX || csValue < i8.MIN)
        {
            throw new OverflowException($"u16 value `{csValue}` cannot be converted to type i8.");
        }
        return (sbyte)csValue;
    }

    /// <summary>
    /// Potentially unsafe conversion from u16 to u8.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public u8 unsafe_to_u8()
    {
        ushort csValue = this._csReadValue;
        if (csValue > u8.MAX || csValue < u8.MIN)
        {
            throw new OverflowException($"u16 value `{csValue}` cannot be converted to type u8.");
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
    
    public static bool operator ==(u16 a, u16 b)
    {
        var result = a._csReadValue == b._csReadValue;
        return result;
    }

    public static bool operator !=(u16 a, u16 b)
    {
        var result = a._csReadValue != b._csReadValue;
        return result;
    }

    public static bool operator <(u16 a, u16 b)
    {
        var result = a._csReadValue < b._csReadValue;
        return result;
    }

    public static bool operator <=(u16 a, u16 b)
    {
        var result = a._csReadValue <= b._csReadValue;
        return result;
    }

    public static bool operator >(u16 a, u16 b)
    {
        var result = a._csReadValue > b._csReadValue;
        return result;
    }

    public static bool operator >=(u16 a, u16 b)
    {
        var result = a._csReadValue >= b._csReadValue;
        return result;
    }


    
    public static u16 operator +(u16 a, u16 b)
    {
        var value = a._csReadValue + b._csReadValue;
        if (value < u16.MIN) { throw new OverflowException($"Underflow! `{a} (u16) + {b} (u16)` result `{value}` is beyond u16 type MIN limit of `{u16.MIN}`. Explicitly widen before `+` operation."); }
        if (value > u16.MAX) { throw new OverflowException($"Overflow! `{a} (u16) + {b} (u16)` result `{value}` is beyond u16 type MAX limit of `{u16.MAX}`. Explicitly widen before `+` operation."); }
        u16 result = (ushort)value;
        return result;
    }
    public static i32 operator +(u16 a, IHasI8 b)
    {
        var value = a._csReadValue + b.value;
        if (value < i32.MIN) { throw new OverflowException($"Underflow! `{a} (i32) + {b} (i32)` result `{value}` is beyond i32 type MIN limit of `{i32.MIN}`. Explicitly widen before `+` operation."); }
        if (value > i32.MAX) { throw new OverflowException($"Overflow! `{a} (i32) + {b} (i32)` result `{value}` is beyond i32 type MAX limit of `{i32.MAX}`. Explicitly widen before `+` operation."); }
        i32 result = (int)value;
        return result;
    }
    public static i32 operator +(u16 a, IHasI16 b)
    {
        var value = a._csReadValue + b.value;
        if (value < i32.MIN) { throw new OverflowException($"Underflow! `{a} (i32) + {b} (i32)` result `{value}` is beyond i32 type MIN limit of `{i32.MIN}`. Explicitly widen before `+` operation."); }
        if (value > i32.MAX) { throw new OverflowException($"Overflow! `{a} (i32) + {b} (i32)` result `{value}` is beyond i32 type MAX limit of `{i32.MAX}`. Explicitly widen before `+` operation."); }
        i32 result = (int)value;
        return result;
    }
    public static u32 operator +(u16 a, u32 b)
    {
        var value = a._csReadValue + b._csReadValue;
        if (value < u32.MIN) { throw new OverflowException($"Underflow! `{a} (u32) + {b} (u32)` result `{value}` is beyond u32 type MIN limit of `{u32.MIN}`. Explicitly widen before `+` operation."); }
        if (value > u32.MAX) { throw new OverflowException($"Overflow! `{a} (u32) + {b} (u32)` result `{value}` is beyond u32 type MAX limit of `{u32.MAX}`. Explicitly widen before `+` operation."); }
        u32 result = (uint)value;
        return result;
    }
    public static u64 operator +(u16 a, u64 b)
    {
        var value = a._csReadValue + b._csReadValue;
        
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
        return obj_value == (ushort)value;
    }
}