//NOTE! AUTO GENERATED FILE
using System;

#pragma warning disable IDE1006 // Naming Styles.
#pragma warning disable CS0652  // Useless comparison for integer types. Stuff like `u8 < 0`. Not a priority.

namespace fin.sim.lang;

public struct u8: IHasU8
{
    public const byte MAX = 255;
    public const byte MIN = 0;

    /// <summary>
    /// C# backing value.
    /// </summary>
    internal byte _csValue;

    public u8()
    {
    }

    private u8(byte value)
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
    internal byte _csReadValue => _csValue;

    public u8 value
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
    public static implicit operator u8(byte num) { return new u8(num); }

    /// <summary>
    /// Implicit conversion from fin numeric type to C# numeric type.
    /// </summary>
    /// This is needed for technical reasons, but I don't remember them. Should be documented.
    public static implicit operator byte(u8 num) { return num._csReadValue; }

    //################################################################
    // widening conversions
    //################################################################
    
    
    /// <summary>
    /// Safe explicit widening conversion.
    /// </summary>
    public u16 u16 => value;

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
    public static implicit operator u16(u8 num) { return num._csReadValue; }

    /// <summary>
    /// Safe implicit widening conversion.
    /// </summary>
    public static implicit operator u32(u8 num) { return num._csReadValue; }

    /// <summary>
    /// Safe implicit widening conversion.
    /// </summary>
    public static implicit operator u64(u8 num) { return num._csReadValue; }

    /// <summary>
    /// Safe implicit widening conversion.
    /// </summary>
    public static implicit operator i16(u8 num) { return num._csReadValue; }

    /// <summary>
    /// Safe implicit widening conversion.
    /// </summary>
    public static implicit operator i32(u8 num) { return num._csReadValue; }

    /// <summary>
    /// Safe implicit widening conversion.
    /// </summary>
    public static implicit operator i64(u8 num) { return num._csReadValue; }


    //################################################################
    // narrowing conversions
    //################################################################
    
    /// <summary>
    /// Potentially unsafe conversion from u8 to i8.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public i8 unsafe_to_i8()
    {
        ThrowIfMathModeNotSpecified();
        byte csValue = this._csReadValue;
        if (csValue > i8.MAX || csValue < i8.MIN)
        {
            throw new OverflowException($"u8 value `{csValue}` cannot be converted to type i8.");
        }
        return (sbyte)csValue;
    }


    //################################################################
    // wrapping conversions (only for unsigned)
    //################################################################
    
    

    //################################################################
    // comparisons
    //################################################################
    
    public static bool operator ==(u8 a, u8 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue == b._csReadValue;
        return result;
    }

    public static bool operator !=(u8 a, u8 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue != b._csReadValue;
        return result;
    }

    public static bool operator <(u8 a, u8 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue < b._csReadValue;
        return result;
    }

    public static bool operator <=(u8 a, u8 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue <= b._csReadValue;
        return result;
    }

    public static bool operator >(u8 a, u8 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue > b._csReadValue;
        return result;
    }

    public static bool operator >=(u8 a, u8 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue >= b._csReadValue;
        return result;
    }


    
    public static u8 operator +(u8 a, u8 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = a._csReadValue + b._csReadValue;
        if (value < u8.MIN) { throw new OverflowException($"Underflow! `{a} (u8) + {b} (u8)` result `{value}` is beyond u8 type MIN limit of `{u8.MIN}`. Explicitly widen before `+` operation."); }
        if (value > u8.MAX) { throw new OverflowException($"Overflow! `{a} (u8) + {b} (u8)` result `{value}` is beyond u8 type MAX limit of `{u8.MAX}`. Explicitly widen before `+` operation."); }
        u8 result = (byte)value;
        return result;
    }
    public static i16 operator +(u8 a, IHasI8 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = a._csReadValue + b.value;
        if (value < i16.MIN) { throw new OverflowException($"Underflow! `{a} (i16) + {b} (i16)` result `{value}` is beyond i16 type MIN limit of `{i16.MIN}`. Explicitly widen before `+` operation."); }
        if (value > i16.MAX) { throw new OverflowException($"Overflow! `{a} (i16) + {b} (i16)` result `{value}` is beyond i16 type MAX limit of `{i16.MAX}`. Explicitly widen before `+` operation."); }
        i16 result = (short)value;
        return result;
    }
    public static u16 operator +(u8 a, u16 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = a._csReadValue + b._csReadValue;
        if (value < u16.MIN) { throw new OverflowException($"Underflow! `{a} (u16) + {b} (u16)` result `{value}` is beyond u16 type MIN limit of `{u16.MIN}`. Explicitly widen before `+` operation."); }
        if (value > u16.MAX) { throw new OverflowException($"Overflow! `{a} (u16) + {b} (u16)` result `{value}` is beyond u16 type MAX limit of `{u16.MAX}`. Explicitly widen before `+` operation."); }
        u16 result = (ushort)value;
        return result;
    }
    public static u32 operator +(u8 a, u32 b)
    {
        ThrowIfMathModeNotSpecified();
        var value = a._csReadValue + b._csReadValue;
        if (value < u32.MIN) { throw new OverflowException($"Underflow! `{a} (u32) + {b} (u32)` result `{value}` is beyond u32 type MIN limit of `{u32.MIN}`. Explicitly widen before `+` operation."); }
        if (value > u32.MAX) { throw new OverflowException($"Overflow! `{a} (u32) + {b} (u32)` result `{value}` is beyond u32 type MAX limit of `{u32.MAX}`. Explicitly widen before `+` operation."); }
        u32 result = (uint)value;
        return result;
    }
    public static u64 operator +(u8 a, u64 b)
    {
        ThrowIfMathModeNotSpecified();
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
        return obj_value == (byte)value;
    }
}