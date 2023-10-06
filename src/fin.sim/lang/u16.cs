
//NOTE! AUTO GENERATED FILE
using System;

#pragma warning disable IDE1006 // Naming Styles

namespace fin.sim.lang
{
    public struct u16: IHasU16
    {
        public const ushort MAX = 65535;
        public const ushort MIN = 0;

        internal ushort _value;

        public u16()
        {
        }

        private u16(ushort value)
        {
            _value = value;
        }

        private ushort read_value => _value;

        internal static ushort GetBackingValue(u16 n) { return n.read_value; }

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
                this._value = value._value;
            }
        }

        public static implicit operator u16(ushort num) { return new u16(num); }
        public static implicit operator ushort(u16 num) { return num.read_value; }    //needed

        
        public u32 as_u32 => value;
        public u64 as_u64 => value;
        public i32 as_i32 => value;
        public i64 as_i64 => value;

        public static implicit operator u32(u16 num) { return num.read_value; }
        public static implicit operator u64(u16 num) { return num.read_value; }
        public static implicit operator i32(u16 num) { return num.read_value; }
        public static implicit operator i64(u16 num) { return num.read_value; }

        /// <summary>
        /// Throws if the value won't fit.
        /// </summary>
        public i16 as_i16 {
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

        /// <summary>
        /// Throws if the value won't fit.
        /// </summary>
        public i8 as_i8 {
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

        public static bool operator <(u16 a, u16 b)
        {
            var result = a.read_value < b.read_value;
            return result;
        }

        public static bool operator <=(u16 a, u16 b)
        {
            var result = a.read_value <= b.read_value;
            return result;
        }

        public static bool operator >(u16 a, u16 b)
        {
            var result = a.read_value > b.read_value;
            return result;
        }

        public static bool operator >=(u16 a, u16 b)
        {
            var result = a.read_value >= b.read_value;
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
            return obj_value == (ushort)value;
        }
    }
}
