using System.Text.RegularExpressions;

namespace fin.sim.gen;

public class TypeInfo
{
    public readonly bool is_signed;
    public readonly char sign_char;
    public readonly int width;
    public readonly string fin_name;

    public bool is_unsigned => !is_signed;

    public TypeInfo(string fin_type_name)
    {
        var regex = new Regex(@"([iu])(\d+)(r?)");

        Match match = regex.Match(fin_type_name);

        sign_char = match.Groups[1].Value[0];
        width = int.Parse(match.Groups[2].Value);

        is_signed = sign_char == 'i';
        fin_name = "" + sign_char + width;
    }

    public string ToBinary(decimal value)
    {
        string v = fin_name switch
        {
            "i8" => Convert.ToString(unchecked((byte)(SByte)value), 2),
            "i16" => Convert.ToString((Int16)value, 2),
            "i32" => Convert.ToString((Int32)value, 2),
            "i64" => Convert.ToString((Int64)value, 2),
            "u8"  => Convert.ToString((Byte)value, 2),
            "u16" => Convert.ToString(unchecked((Int16)(UInt16)value), 2),
            "u32" => Convert.ToString(unchecked((Int32)(UInt32)value), 2),
            "u64" => Convert.ToString(unchecked((Int64)(ulong)value), 2),
            _ => throw new Exception(),
        };
        return v;
    }

    public decimal GetMinValue()
    {
        return fin_name switch
        {
            "i8" => SByte.MinValue,
            "i16" => Int16.MinValue,
            "i32" => Int32.MinValue,
            "i64" => Int64.MinValue,
            "u8" => Byte.MinValue,
            "u16" => UInt16.MinValue,
            "u32" => UInt32.MinValue,
            "u64" => UInt64.MinValue,
            _ => throw new Exception(),
        };
    }

    public decimal GetMaxValue()
    {
        return fin_name switch
        {
            "i8" => SByte.MaxValue,
            "i16" => Int16.MaxValue,
            "i32" => Int32.MaxValue,
            "i64" => Int64.MaxValue,
            "u8" => Byte.MaxValue,
            "u16" => UInt16.MaxValue,
            "u32" => UInt32.MaxValue,
            "u64" => UInt64.MaxValue,
            _ => throw new Exception(),
        };
    }

    public string GetBackingTypeName()
    {
        string backing_type = fin_name switch
        {
            "i8" => "sbyte",
            "i16" => "short",
            "i32" => "int",
            "i64" => "long",
            "u8" => "byte",
            "u16" => "ushort",
            "u32" => "uint",
            "u64" => "ulong",
            _ => throw new Exception(),
        };
        return backing_type;
    }

    public TypeInfo GetResultType(TypeInfo other)
    {
        int width = Math.Max(this.width, other.width);
        char sign_char;

        if (is_signed ^ other.is_signed)
        {
            sign_char = 'i';

            if (this.CanPromoteToOrViceVersa(other))
            {
                //nothing to do
            }
            else
            {
                width *= 2; //needs to promote to next largest
            }
        }
        else if (is_signed)
        {
            //both signed
            sign_char = 'i';
        }
        else
        {
            //both unsigned
            sign_char = 'u';
        }

        var result = new TypeInfo("" + sign_char + width);
        return result;
    }

    public TypeInfo GetResultTypeFromLiteral(decimal literal_value)
    {
        TypeInfo result;
        int new_width = width;

        if (literal_value >= 0)
        {
            //want literal value to stick with sign of this type

            while (true)
            {

                result = new TypeInfo("" + sign_char + new_width);

                decimal max = result.GetMaxValue();
                decimal min = result.GetMinValue();

                if (literal_value <= max && literal_value >= min)
                {
                    break;
                }
                else
                {
                    new_width *= 2;
                }
            }
        }
        else
        {
            //literal value is negative. result type must be signed
            while (true)
            {
                //want literal value to stick with sign of this type
                result = new TypeInfo("i" + new_width);

                decimal max = result.GetMaxValue();
                decimal min = result.GetMinValue();

                if (literal_value <= max && literal_value >= min)
                {
                    break;
                }
                else
                {
                    new_width *= 2;
                }
            }

        }

        return result;
    }

    public bool CanPromoteTo(TypeInfo other)
    {
        bool canPromote;

        if (is_signed && !other.is_signed)
        {
            canPromote = false;
        }
        else
        {
            canPromote = width < other.width;
        }

        return canPromote;
    }

    public bool CanPromoteToOrViceVersa(TypeInfo other)
    {
        return this.CanPromoteTo(other) || other.CanPromoteTo(this);
    }

    public TypeInfo? GetSmallerSameSign()
    {
        int newWidth = this.width/2;

        if (newWidth >= 8)
        {
            return new TypeInfo("" + sign_char + width);
        }
        return null;
    }

    public TypeInfo? LargeEnoughToDetectOverflow(TypeInfo other)
    {
        int maxExistingWidth = Math.Max(this.width, other.width);
        int newWidth = maxExistingWidth * 2;

        if (newWidth > 64)
            return null;

        bool atleastOneSigned = this.is_signed || other.is_signed;
        var signChar = atleastOneSigned ? 'i' : 'u';

        return new TypeInfo("" + signChar + newWidth);
    }

    public bool Equals(TypeInfo other)
    {
        return fin_name.Equals(other.fin_name);
    }
}
