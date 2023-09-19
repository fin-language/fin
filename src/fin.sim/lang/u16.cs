
//NOTE! AUTO GENERATED FILE
using System;

#pragma warning disable IDE1006 // Naming Styles

namespace fin.sim.lang
{
    public class u16 : FinObj , IHasU16
    {
        public const ushort MAX = 65535;
        public const ushort MIN = 0;

        protected ushort _value;

        private u16()
        {
        }

        private u16(ushort value)
        {
            _value = value;
        }

        private ushort read_value => _value;

        internal static ushort GetBackingValue(u16 n) { return n.read_value; }

        public u16 v
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
        /// creates a copy of u16 memory. Useful for when passing to functions.
        /// </summary>
        public u16 cp => new u16(read_value);

        public static implicit operator u16(ushort num) { return new u16(num); }
        public static implicit operator ushort(u16 num) { return num.read_value; }    //needed

        
        public u32 as_u32 => v;
        public u64 as_u64 => v;
        public i32 as_i32 => v;
        public i64 as_i64 => v;

        public static implicit operator u32(u16 num) { return num.read_value; }
        public static implicit operator u64(u16 num) { return num.read_value; }
        public static implicit operator i32(u16 num) { return num.read_value; }
        public static implicit operator i64(u16 num) { return num.read_value; }

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

        public i16 wrap_to_i16 => unchecked((short)GetBackingValue(this));
        public i8 wrap_to_i8 => unchecked((sbyte)GetBackingValue(this));
        public u8 wrap_to_u8 => unchecked((byte)GetBackingValue(this));

        public static bool operator ==(u16 a, u16 b)
        {
            var result = a.read_value == b.read_value;
            return result;
        }

        public static bool operator !=(u16 a, u16 b)
        {
            var result = a.read_value != b.read_value;
            return result;
        }


        public static u16 operator +(u16 a, u16 b)
        {
            var value = u16.GetBackingValue(a) + u16.GetBackingValue(b);
            if (value < u16.MIN) { throw new Exception("underflow!"); }
            if (value > u16.MAX) { throw new Exception("overflow!");  }
            u16 result = (ushort)value;
            return result;
        }public static i32 operator +(u16 a, IHasI8 b)
        {
            var value = u16.GetBackingValue(a) + i8.GetBackingValue((i8)b);
            if (value < i32.MIN) { throw new Exception("underflow!"); }
            if (value > i32.MAX) { throw new Exception("overflow!");  }
            i32 result = (int)value;
            return result;
        }public static i32 operator +(u16 a, IHasI16 b)
        {
            var value = u16.GetBackingValue(a) + i16.GetBackingValue((i16)b);
            if (value < i32.MIN) { throw new Exception("underflow!"); }
            if (value > i32.MAX) { throw new Exception("overflow!");  }
            i32 result = (int)value;
            return result;
        }public static u32 operator +(u16 a, u32 b)
        {
            var value = u16.GetBackingValue(a) + u32.GetBackingValue(b);
            if (value < u32.MIN) { throw new Exception("underflow!"); }
            if (value > u32.MAX) { throw new Exception("overflow!");  }
            u32 result = (uint)value;
            return result;
        }public static u64 operator +(u16 a, u64 b)
        {
            var value = u16.GetBackingValue(a) + u64.GetBackingValue(b);
            
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
            return value == (ushort)value;
        }
    }
}
