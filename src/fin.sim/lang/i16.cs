
//NOTE! AUTO GENERATED FILE
using System;

#pragma warning disable IDE1006 // Naming Styles

namespace fin.sim.lang
{
    public struct i16: IHasI16
    {
        public const short MAX = 32767;
        public const short MIN = -32768;

        internal short _value;

        public i16()
        {
        }

        private i16(short value)
        {
            _value = value;
        }

        private short read_value => _value;

        internal static short GetBackingValue(i16 n) { return n.read_value; }

        public i16 value
        {
            get
            {
                // TODO: _ThrowIfDestructed();
                return this;
            }

            set
            {
                // TODO: _ThrowIfDestructed();
                this._value = value._value;
            }
        }

        public static implicit operator i16(short num) { return new i16(num); }
        public static implicit operator short(i16 num) { return num.read_value; }    //needed

        
        public i32 i32 => value;
        public i64 i64 => value;

        // widening conversions
        public static implicit operator i32(i16 num) { return num.read_value; }
        public static implicit operator i64(i16 num) { return num.read_value; }

        // narrowing conversions
        /// <summary>
        /// Throws during simulation if the value won't fit.
        /// </summary>
        public u16 unsafe_to_u16 {
            get {
                var vv = GetBackingValue(this);
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
                var vv = GetBackingValue(this);
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
                var vv = GetBackingValue(this);
                decimal v = vv; // will not use decimal in the future to speed up simulations
                if (v > u8.MAX || v < u8.MIN)
                {
                    throw new System.OverflowException("value " + vv + " too large for u8");
                }
                return (byte)vv;
            }
        }

        // wrapping conversions
        public u16 wrap_u16 => unchecked((ushort)GetBackingValue(this));
        public i8 wrap_i8 => unchecked((sbyte)GetBackingValue(this));
        public u8 wrap_u8 => unchecked((byte)GetBackingValue(this));

        public static bool operator ==(i16 a, i16 b)
        {
            var result = a.read_value == b.read_value;
            return result;
        }

        public static bool operator !=(i16 a, i16 b)
        {
            var result = a.read_value != b.read_value;
            return result;
        }

        public static bool operator <(i16 a, i16 b)
        {
            var result = a.read_value < b.read_value;
            return result;
        }

        public static bool operator <=(i16 a, i16 b)
        {
            var result = a.read_value <= b.read_value;
            return result;
        }

        public static bool operator >(i16 a, i16 b)
        {
            var result = a.read_value > b.read_value;
            return result;
        }

        public static bool operator >=(i16 a, i16 b)
        {
            var result = a.read_value >= b.read_value;
            return result;
        }


        public static i16 operator +(i16 a, i16 b)
        {
            var value = i16.GetBackingValue(a) + i16.GetBackingValue(b);
            if (value < i16.MIN) { throw new Exception("underflow!"); }
            if (value > i16.MAX) { throw new Exception("overflow!");  }
            i16 result = (short)value;
            return result;
        }public static i32 operator +(i16 a, i32 b)
        {
            var value = i16.GetBackingValue(a) + i32.GetBackingValue(b);
            if (value < i32.MIN) { throw new Exception("underflow!"); }
            if (value > i32.MAX) { throw new Exception("overflow!");  }
            i32 result = (int)value;
            return result;
        }public static i64 operator +(i16 a, i64 b)
        {
            var value = i16.GetBackingValue(a) + i64.GetBackingValue(b);
            
            i64 result = (long)value;
            return result;
        }



        public override string ToString()
        {
            return read_value.ToString();
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

                case i8  i: obj_value = i8.GetBackingValue(i);  break;
                case i16 i: obj_value = i16.GetBackingValue(i); break;
                case i32 i: obj_value = i32.GetBackingValue(i); break;
                case i64 i: obj_value = i64.GetBackingValue(i); break;
                case u8  i: obj_value = u8.GetBackingValue(i);  break;
                case u16 i: obj_value = u16.GetBackingValue(i); break;
                case u32 i: obj_value = u32.GetBackingValue(i); break;
                case u64 i: obj_value = u64.GetBackingValue(i); break;

                default: return false;
            }

            if (obj_value < MIN || obj_value > MAX) { return false; }
            return obj_value == (short)value;
        }
    }
}
