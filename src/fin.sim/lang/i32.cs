
//NOTE! AUTO GENERATED FILE
using System;

#pragma warning disable IDE1006 // Naming Styles

namespace fin.sim.lang
{
    public class i32 : FinObj , IHasI32
    {
        public const int MAX = 2147483647;
        public const int MIN = -2147483648;

        protected int _value;

        private i32()
        {
        }

        private i32(int value)
        {
            _value = value;
        }

        private int read_value => _value;

        internal static int GetBackingValue(i32 n) { return n.read_value; }

        public i32 v
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
        /// creates a copy of i32 memory. Useful for when passing to functions.
        /// </summary>
        public i32 cp => new i32(read_value);

        public static implicit operator i32(int num) { return new i32(num); }
        public static implicit operator int(i32 num) { return num.read_value; }    //needed

        
        public i64 as_i64 => v;

        public static implicit operator i64(i32 num) { return num.read_value; }

        public u32 as_u32_ort {
            get {
                var vv = GetBackingValue(this);
                decimal v = vv;
                if (v > u32.MAX || v < u32.MIN)
                {
                    throw new System.OverflowException("value " + vv + " too large for u32");
                }
                return (uint)vv;
            }
        }

        public i16 as_i16_ort {
            get {
                var vv = GetBackingValue(this);
                decimal v = vv;
                if (v > i16.MAX || v < i16.MIN)
                {
                    throw new System.OverflowException("value " + vv + " too large for i16");
                }
                return (short)vv;
            }
        }

        public u16 as_u16_ort {
            get {
                var vv = GetBackingValue(this);
                decimal v = vv;
                if (v > u16.MAX || v < u16.MIN)
                {
                    throw new System.OverflowException("value " + vv + " too large for u16");
                }
                return (ushort)vv;
            }
        }

        public i8 as_i8_ort {
            get {
                var vv = GetBackingValue(this);
                decimal v = vv;
                if (v > i8.MAX || v < i8.MIN)
                {
                    throw new System.OverflowException("value " + vv + " too large for i8");
                }
                return (sbyte)vv;
            }
        }

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

        public u32 wrap_to_u32 => unchecked((uint)GetBackingValue(this));
        public i16 wrap_to_i16 => unchecked((short)GetBackingValue(this));
        public u16 wrap_to_u16 => unchecked((ushort)GetBackingValue(this));
        public i8 wrap_to_i8 => unchecked((sbyte)GetBackingValue(this));
        public u8 wrap_to_u8 => unchecked((byte)GetBackingValue(this));

        public static bool operator ==(i32 a, i32 b)
        {
            var result = a.read_value == b.read_value;
            return result;
        }

        public static bool operator !=(i32 a, i32 b)
        {
            var result = a.read_value != b.read_value;
            return result;
        }


        public static i32 operator +(i32 a, i32 b)
        {
            var value = i32.GetBackingValue(a) + i32.GetBackingValue(b);
            if (value < i32.MIN) { throw new Exception("underflow!"); }
            if (value > i32.MAX) { throw new Exception("overflow!");  }
            i32 result = (int)value;
            return result;
        }public static i64 operator +(i32 a, i64 b)
        {
            var value = i32.GetBackingValue(a) + i64.GetBackingValue(b);
            
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
            return value == (int)value;
        }
    }
}
