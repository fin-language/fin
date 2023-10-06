
//NOTE! AUTO GENERATED FILE
using System;

#pragma warning disable IDE1006 // Naming Styles

namespace fin.sim.lang
{
    public struct u64: IHasU64
    {
        public const ulong MAX = 18446744073709551615;
        public const ulong MIN = 0;

        internal ulong _value;

        public u64()
        {
        }

        private u64(ulong value)
        {
            _value = value;
        }

        private ulong read_value => _value;

        internal static ulong GetBackingValue(u64 n) { return n.read_value; }

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
                this._value = value._value;
            }
        }

        public static implicit operator u64(ulong num) { return new u64(num); }
        public static implicit operator ulong(u64 num) { return num.read_value; }    //needed

        

        // widening conversions
        

        // narrowing conversions
        /// <summary>
        /// Throws during simulation if the value won't fit.
        /// </summary>
        public i64 unsafe_to_i64 {
            get {
                var vv = GetBackingValue(this);
                decimal v = vv; // will not use decimal in the future to speed up simulations
                if (v > i64.MAX || v < i64.MIN)
                {
                    throw new System.OverflowException("value " + vv + " too large for i64");
                }
                return (long)vv;
            }
        }

        /// <summary>
        /// Throws during simulation if the value won't fit.
        /// </summary>
        public i32 unsafe_to_i32 {
            get {
                var vv = GetBackingValue(this);
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
        public u32 unsafe_to_u32 {
            get {
                var vv = GetBackingValue(this);
                decimal v = vv; // will not use decimal in the future to speed up simulations
                if (v > u32.MAX || v < u32.MIN)
                {
                    throw new System.OverflowException("value " + vv + " too large for u32");
                }
                return (uint)vv;
            }
        }

        /// <summary>
        /// Throws during simulation if the value won't fit.
        /// </summary>
        public i16 unsafe_to_i16 {
            get {
                var vv = GetBackingValue(this);
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
        public i64 wrap_i64 => unchecked((long)GetBackingValue(this));
        public i32 wrap_i32 => unchecked((int)GetBackingValue(this));
        public u32 wrap_u32 => unchecked((uint)GetBackingValue(this));
        public i16 wrap_i16 => unchecked((short)GetBackingValue(this));
        public u16 wrap_u16 => unchecked((ushort)GetBackingValue(this));
        public i8 wrap_i8 => unchecked((sbyte)GetBackingValue(this));
        public u8 wrap_u8 => unchecked((byte)GetBackingValue(this));

        public static bool operator ==(u64 a, u64 b)
        {
            var result = a.read_value == b.read_value;
            return result;
        }

        public static bool operator !=(u64 a, u64 b)
        {
            var result = a.read_value != b.read_value;
            return result;
        }

        public static bool operator <(u64 a, u64 b)
        {
            var result = a.read_value < b.read_value;
            return result;
        }

        public static bool operator <=(u64 a, u64 b)
        {
            var result = a.read_value <= b.read_value;
            return result;
        }

        public static bool operator >(u64 a, u64 b)
        {
            var result = a.read_value > b.read_value;
            return result;
        }

        public static bool operator >=(u64 a, u64 b)
        {
            var result = a.read_value >= b.read_value;
            return result;
        }


        public static u64 operator +(u64 a, u64 b)
        {
            var value = u64.GetBackingValue(a) + u64.GetBackingValue(b);
            
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
            return obj_value == (ulong)value;
        }
    }
}
