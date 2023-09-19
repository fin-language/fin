
//NOTE! AUTO GENERATED FILE
using System;

#pragma warning disable IDE1006 // Naming Styles

namespace fin.sim.lang
{
    public class u8 : FinObj , IHasU8
    {
        public const byte MAX = 255;
        public const byte MIN = 0;

        protected byte _value;

        private u8()
        {
        }

        private u8(byte value)
        {
            _value = value;
        }

        private byte read_value => _value;

        internal static byte GetBackingValue(u8 n) { return n.read_value; }

        public u8 v
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
        /// creates a copy of u8 memory. Useful for when passing to functions.
        /// </summary>
        public u8 cp => new u8(read_value);

        public static implicit operator u8(byte num) { return new u8(num); }
        public static implicit operator byte(u8 num) { return num.read_value; }    //needed

        
        public u16 as_u16 => v;
        public u32 as_u32 => v;
        public u64 as_u64 => v;
        public i16 as_i16 => v;
        public i32 as_i32 => v;
        public i64 as_i64 => v;

        public static implicit operator u16(u8 num) { return num.read_value; }
        public static implicit operator u32(u8 num) { return num.read_value; }
        public static implicit operator u64(u8 num) { return num.read_value; }
        public static implicit operator i16(u8 num) { return num.read_value; }
        public static implicit operator i32(u8 num) { return num.read_value; }
        public static implicit operator i64(u8 num) { return num.read_value; }

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

        public i8 wrap_to_i8 => unchecked((sbyte)GetBackingValue(this));

        public static bool operator ==(u8 a, u8 b)
        {
            var result = a.read_value == b.read_value;
            return result;
        }

        public static bool operator !=(u8 a, u8 b)
        {
            var result = a.read_value != b.read_value;
            return result;
        }


        public static u8 operator +(u8 a, u8 b)
        {
            var value = u8.GetBackingValue(a) + u8.GetBackingValue(b);
            if (value < u8.MIN) { throw new Exception("underflow!"); }
            if (value > u8.MAX) { throw new Exception("overflow!");  }
            u8 result = (byte)value;
            return result;
        }public static i16 operator +(u8 a, IHasI8 b)
        {
            var value = u8.GetBackingValue(a) + i8.GetBackingValue((i8)b);
            if (value < i16.MIN) { throw new Exception("underflow!"); }
            if (value > i16.MAX) { throw new Exception("overflow!");  }
            i16 result = (short)value;
            return result;
        }public static u16 operator +(u8 a, u16 b)
        {
            var value = u8.GetBackingValue(a) + u16.GetBackingValue(b);
            if (value < u16.MIN) { throw new Exception("underflow!"); }
            if (value > u16.MAX) { throw new Exception("overflow!");  }
            u16 result = (ushort)value;
            return result;
        }public static u32 operator +(u8 a, u32 b)
        {
            var value = u8.GetBackingValue(a) + u32.GetBackingValue(b);
            if (value < u32.MIN) { throw new Exception("underflow!"); }
            if (value > u32.MAX) { throw new Exception("overflow!");  }
            u32 result = (uint)value;
            return result;
        }public static u64 operator +(u8 a, u64 b)
        {
            var value = u8.GetBackingValue(a) + u64.GetBackingValue(b);
            
            u64 result = (ulong)value;
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
            return value == (byte)value;
        }
    }
}
