
//NOTE! AUTO GENERATED FILE
using System;

#pragma warning disable IDE1006 // Naming Styles

namespace fin.sim.lang
{
    public struct u8: IHasU8
    {
        public const byte MAX = 255;
        public const byte MIN = 0;

        internal byte _value;

        public u8()
        {
        }

        private u8(byte value)
        {
            _value = value;
        }

        private byte read_value => _value;

        internal static byte GetBackingValue(u8 n) { return n.read_value; }

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
                this._value = value._value;
            }
        }

        public static implicit operator u8(byte num) { return new u8(num); }
        public static implicit operator byte(u8 num) { return num.read_value; }    //needed

        
        public u16 as_u16 => value;
        public u32 as_u32 => value;
        public u64 as_u64 => value;
        public i16 as_i16 => value;
        public i32 as_i32 => value;
        public i64 as_i64 => value;

        public static implicit operator u16(u8 num) { return num.read_value; }
        public static implicit operator u32(u8 num) { return num.read_value; }
        public static implicit operator u64(u8 num) { return num.read_value; }
        public static implicit operator i16(u8 num) { return num.read_value; }
        public static implicit operator i32(u8 num) { return num.read_value; }
        public static implicit operator i64(u8 num) { return num.read_value; }

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

        public static bool operator <(u8 a, u8 b)
        {
            var result = a.read_value < b.read_value;
            return result;
        }

        public static bool operator <=(u8 a, u8 b)
        {
            var result = a.read_value <= b.read_value;
            return result;
        }

        public static bool operator >(u8 a, u8 b)
        {
            var result = a.read_value > b.read_value;
            return result;
        }

        public static bool operator >=(u8 a, u8 b)
        {
            var result = a.read_value >= b.read_value;
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
            return obj_value == (byte)value;
        }
    }
}
