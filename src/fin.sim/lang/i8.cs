
//NOTE! AUTO GENERATED FILE
using System;

#pragma warning disable IDE1006 // Naming Styles

namespace fin.sim.lang
{
    public struct i8: IHasI8
    {
        public const sbyte MAX = 127;
        public const sbyte MIN = -128;

        internal sbyte _value;

        public i8()
        {
        }

        private i8(sbyte value)
        {
            _value = value;
        }

        private sbyte read_value => _value;

        internal static sbyte GetBackingValue(i8 n) { return n.read_value; }

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
                this._value = value._value;
            }
        }

        public static implicit operator i8(sbyte num) { return new i8(num); }
        public static implicit operator sbyte(i8 num) { return num.read_value; }    //needed

        
        public i16 as_i16 => value;
        public i32 as_i32 => value;
        public i64 as_i64 => value;

        public static implicit operator i16(i8 num) { return num.read_value; }
        public static implicit operator i32(i8 num) { return num.read_value; }
        public static implicit operator i64(i8 num) { return num.read_value; }

        /// <summary>
        /// Throws if the value won't fit.
        /// </summary>
        public u8 as_u8 {
            get {
                var vv = GetBackingValue(this);
                decimal v = vv;
                if (v > u8.MAX || v < u8.MIN)
                {
                    throw new System.OverflowException("value " + vv + " too large for u8");
                }
                return (byte)vv;
            }
        }

        public u8 wrap_to_u8 => unchecked((byte)GetBackingValue(this));

        public static bool operator ==(i8 a, i8 b)
        {
            var result = a.read_value == b.read_value;
            return result;
        }

        public static bool operator !=(i8 a, i8 b)
        {
            var result = a.read_value != b.read_value;
            return result;
        }

        public static bool operator <(i8 a, i8 b)
        {
            var result = a.read_value < b.read_value;
            return result;
        }

        public static bool operator <=(i8 a, i8 b)
        {
            var result = a.read_value <= b.read_value;
            return result;
        }

        public static bool operator >(i8 a, i8 b)
        {
            var result = a.read_value > b.read_value;
            return result;
        }

        public static bool operator >=(i8 a, i8 b)
        {
            var result = a.read_value >= b.read_value;
            return result;
        }


        public static i8 operator +(i8 a, i8 b)
        {
            var value = i8.GetBackingValue(a) + i8.GetBackingValue(b);
            if (value < i8.MIN) { throw new Exception("underflow!"); }
            if (value > i8.MAX) { throw new Exception("overflow!");  }
            i8 result = (sbyte)value;
            return result;
        }public static i16 operator +(i8 a, i16 b)
        {
            var value = i8.GetBackingValue(a) + i16.GetBackingValue(b);
            if (value < i16.MIN) { throw new Exception("underflow!"); }
            if (value > i16.MAX) { throw new Exception("overflow!");  }
            i16 result = (short)value;
            return result;
        }public static i32 operator +(i8 a, i32 b)
        {
            var value = i8.GetBackingValue(a) + i32.GetBackingValue(b);
            if (value < i32.MIN) { throw new Exception("underflow!"); }
            if (value > i32.MAX) { throw new Exception("overflow!");  }
            i32 result = (int)value;
            return result;
        }public static i64 operator +(i8 a, i64 b)
        {
            var value = i8.GetBackingValue(a) + i64.GetBackingValue(b);
            
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
            return obj_value == (sbyte)value;
        }
    }
}
