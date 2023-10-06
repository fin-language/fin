//NOTE! AUTO GENERATED FILE
using System;

#pragma warning disable IDE1006 // Naming Styles

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
    /// Throws during simulation if the value won't fit.
    /// </summary>
    public i32 unsafe_to_i32 {
        get {
            var vv = this._csReadValue;
            decimal v = vv; // will not use decimal in the future to speed up simulations
            if (v > i32.MAX || v < i32.MIN)
            {
                throw new System.OverflowException("value " + vv + " too large for i32");
            }
            return (int)vv;
        }
    }

    /// <summary>
    /// Throws during simulation if the value won't fit.
    /// </summary>
    public i16 unsafe_to_i16 {
        get {
            var vv = this._csReadValue;
            decimal v = vv; // will not use decimal in the future to speed up simulations
            if (v > i16.MAX || v < i16.MIN)
            {
                throw new System.OverflowException("value " + vv + " too large for i16");
            }
            return (short)vv;
        }
    }

    /// <summary>
    /// Throws during simulation if the value won't fit.
    /// </summary>
    public u16 unsafe_to_u16 {
        get {
            var vv = this._csReadValue;
            decimal v = vv; // will not use decimal in the future to speed up simulations
            if (v > u16.MAX || v < u16.MIN)
            {
                throw new System.OverflowException("value " + vv + " too large for u16");
            }
            return (ushort)vv;
        }
    }

    /// <summary>
    /// Throws during simulation if the value won't fit.
    /// </summary>
    public i8 unsafe_to_i8 {
        get {
            var vv = this._csReadValue;
            decimal v = vv; // will not use decimal in the future to speed up simulations
            if (v > i8.MAX || v < i8.MIN)
            {
                throw new System.OverflowException("value " + vv + " too large for i8");
            }
            return (sbyte)vv;
        }
    }

    /// <summary>
    /// Throws during simulation if the value won't fit.
    /// </summary>
    public u8 unsafe_to_u8 {
        get {
            var vv = this._csReadValue;
            decimal v = vv; // will not use decimal in the future to speed up simulations
            if (v > u8.MAX || v < u8.MIN)
            {
                throw new System.OverflowException("value " + vv + " too large for u8");
            }
            return (byte)vv;
        }
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
        var result = a._csReadValue == b._csReadValue;
        return result;
    }

    public static bool operator !=(u32 a, u32 b)
    {
        var result = a._csReadValue != b._csReadValue;
        return result;
    }

    public static bool operator <(u32 a, u32 b)
    {
        var result = a._csReadValue < b._csReadValue;
        return result;
    }

    public static bool operator <=(u32 a, u32 b)
    {
        var result = a._csReadValue <= b._csReadValue;
        return result;
    }

    public static bool operator >(u32 a, u32 b)
    {
        var result = a._csReadValue > b._csReadValue;
        return result;
    }

    public static bool operator >=(u32 a, u32 b)
    {
        var result = a._csReadValue >= b._csReadValue;
        return result;
    }


    
    public static u32 operator +(u32 a, u32 b)
    {
        var value = a._csReadValue + b._csReadValue;
        if (value < u32.MIN) { throw new Exception("underflow!"); }
        if (value > u32.MAX) { throw new Exception("overflow!");  }
        u32 result = (uint)value;
        return result;
    }
    public static i64 operator +(u32 a, IHasI8 b)
    {
        var value = a._csReadValue + b.value;
        
        i64 result = (long)value;
        return result;
    }
    public static i64 operator +(u32 a, IHasI16 b)
    {
        var value = a._csReadValue + b.value;
        
        i64 result = (long)value;
        return result;
    }
    public static i64 operator +(u32 a, IHasI32 b)
    {
        var value = a._csReadValue + b.value;
        
        i64 result = (long)value;
        return result;
    }
    public static u64 operator +(u32 a, u64 b)
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
        return obj_value == (uint)value;
    }
}