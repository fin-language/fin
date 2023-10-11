//NOTE! AUTO GENERATED FILE
using System;

#pragma warning disable IDE1006 // Naming Styles.
#pragma warning disable CS0652  // Useless comparison for integer types. Stuff like `u8 < 0`. Not a priority.

namespace fin.sim.lang;

public struct u64: IHasU64
{
    public const ulong MAX = 18446744073709551615;
    public const ulong MIN = 0;

    /// <summary>
    /// C# backing value.
    /// </summary>
    internal ulong _csValue;

    public u64()
    {
    }

    private u64(ulong value)
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
    internal ulong _csReadValue => _csValue;

    public u64 value
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
    public static implicit operator u64(ulong num) { return new u64(num); }

    /// <summary>
    /// Implicit conversion from fin numeric type to C# numeric type.
    /// </summary>
    /// This is needed for technical reasons, but I don't remember them. Should be documented.
    public static implicit operator ulong(u64 num) { return num._csReadValue; }

    //################################################################
    // widening conversions
    //################################################################
    
    

    


    //################################################################
    // narrowing conversions
    //################################################################
    
    /// <summary>
    /// Potentially unsafe conversion from u64 to i64.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public i64 unsafe_to_i64()
    {
        ThrowIfMathModeNotSpecified();
        ulong csValue = this._csReadValue;
        decimal dv = csValue; // use decimal type when C# primitives are too small
        if (dv > i64.MAX || dv < i64.MIN)
        {
            throw new OverflowException($"u64 value `{csValue}` cannot be converted to type i64.");
        }
        return (long)csValue;
    }

    /// <summary>
    /// Potentially unsafe conversion from u64 to i32.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public i32 unsafe_to_i32()
    {
        ThrowIfMathModeNotSpecified();
        ulong csValue = this._csReadValue;
        decimal dv = csValue; // use decimal type when C# primitives are too small
        if (dv > i32.MAX || dv < i32.MIN)
        {
            throw new OverflowException($"u64 value `{csValue}` cannot be converted to type i32.");
        }
        return (int)csValue;
    }

    /// <summary>
    /// Potentially unsafe conversion from u64 to u32.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public u32 unsafe_to_u32()
    {
        ThrowIfMathModeNotSpecified();
        ulong csValue = this._csReadValue;
        decimal dv = csValue; // use decimal type when C# primitives are too small
        if (dv > u32.MAX || dv < u32.MIN)
        {
            throw new OverflowException($"u64 value `{csValue}` cannot be converted to type u32.");
        }
        return (uint)csValue;
    }

    /// <summary>
    /// Potentially unsafe conversion from u64 to i16.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public i16 unsafe_to_i16()
    {
        ThrowIfMathModeNotSpecified();
        ulong csValue = this._csReadValue;
        decimal dv = csValue; // use decimal type when C# primitives are too small
        if (dv > i16.MAX || dv < i16.MIN)
        {
            throw new OverflowException($"u64 value `{csValue}` cannot be converted to type i16.");
        }
        return (short)csValue;
    }

    /// <summary>
    /// Potentially unsafe conversion from u64 to u16.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public u16 unsafe_to_u16()
    {
        ThrowIfMathModeNotSpecified();
        ulong csValue = this._csReadValue;
        decimal dv = csValue; // use decimal type when C# primitives are too small
        if (dv > u16.MAX || dv < u16.MIN)
        {
            throw new OverflowException($"u64 value `{csValue}` cannot be converted to type u16.");
        }
        return (ushort)csValue;
    }

    /// <summary>
    /// Potentially unsafe conversion from u64 to i8.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public i8 unsafe_to_i8()
    {
        ThrowIfMathModeNotSpecified();
        ulong csValue = this._csReadValue;
        decimal dv = csValue; // use decimal type when C# primitives are too small
        if (dv > i8.MAX || dv < i8.MIN)
        {
            throw new OverflowException($"u64 value `{csValue}` cannot be converted to type i8.");
        }
        return (sbyte)csValue;
    }

    /// <summary>
    /// Potentially unsafe conversion from u64 to u8.
    /// This operation will throw during simulation if the value won't fit.
    /// </summary>
    public u8 unsafe_to_u8()
    {
        ThrowIfMathModeNotSpecified();
        ulong csValue = this._csReadValue;
        decimal dv = csValue; // use decimal type when C# primitives are too small
        if (dv > u8.MAX || dv < u8.MIN)
        {
            throw new OverflowException($"u64 value `{csValue}` cannot be converted to type u8.");
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
    
    public static bool operator ==(u64 a, u64 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue == b._csReadValue;
        return result;
    }

    public static bool operator !=(u64 a, u64 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue != b._csReadValue;
        return result;
    }

    public static bool operator <(u64 a, u64 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue < b._csReadValue;
        return result;
    }

    public static bool operator <=(u64 a, u64 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue <= b._csReadValue;
        return result;
    }

    public static bool operator >(u64 a, u64 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue > b._csReadValue;
        return result;
    }

    public static bool operator >=(u64 a, u64 b)
    {
        ThrowIfMathModeNotSpecified();
        var result = a._csReadValue >= b._csReadValue;
        return result;
    }


    
    public static u64 operator +(u64 a, u64 b)
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
        return obj_value == (ulong)value;
    }
}