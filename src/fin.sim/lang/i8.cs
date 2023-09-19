
//NOTE! AUTO GENERATED FILE
using System;

#pragma warning disable IDE1006 // Naming Styles

namespace fin.sim.lang
{
    public class i8 : FinObj , IHasI8
    {
        public const sbyte MAX = 127;
        public const sbyte MIN = -128;

        protected sbyte _value;

        private i8()
        {
        }

        private i8(sbyte value)
        {
            _value = value;
        }

        private sbyte read_value => _value;

        internal static sbyte GetBackingValue(i8 n) { return n.read_value; }

        public i8 v
        {
            get
            {
                _ThrowIfDestructed();
                return this.cp;
            }

            set
            {
                _ThrowIfDestructed();
                this._value = value._value;
            }
        }

        /// <summary>
        /// creates a copy of i8 memory. Useful for when passing to functions.
        /// </summary>
        public i8 cp => new i8(read_value);

        public static implicit operator i8(sbyte num) { return new i8(num); }
        public static implicit operator sbyte(i8 num) { return num.read_value; }    //needed

        
        public i16 as_i16 => v;
        public i32 as_i32 => v;
        public i64 as_i64 => v;

        public static implicit operator i16(i8 num) { return num.read_value; }
        public static implicit operator i32(i8 num) { return num.read_value; }
        public static implicit operator i64(i8 num) { return num.read_value; }

        public u8 as_u8_ort {
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
            return v.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) { return false; }
            if (ReferenceEquals(this, obj)) { return true; }

            decimal value;

            switch (obj)
            {
                case sbyte  i: value = i; break;
                case short  i: value = i; break;
                case int    i: value = i; break;
                case long   i: value = i; break;
                case byte   i: value = i; break;
                case ushort i: value = i; break;
                case uint   i: value = i; break;
                case ulong  i: value = i; break;

                case i8  i: value = i8.GetBackingValue(i);  break;
                case i16 i: value = i16.GetBackingValue(i); break;
                case i32 i: value = i32.GetBackingValue(i); break;
                case i64 i: value = i64.GetBackingValue(i); break;
                case u8  i: value = u8.GetBackingValue(i);  break;
                case u16 i: value = u16.GetBackingValue(i); break;
                case u32 i: value = u32.GetBackingValue(i); break;
                case u64 i: value = u64.GetBackingValue(i); break;

                default: return false;
            }

            if (value < MIN || value > MAX) { return false; }
            return value == (sbyte)value;
        }
    }
}
