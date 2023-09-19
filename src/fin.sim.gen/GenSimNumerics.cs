namespace fin.sim.gen;

public class GenSimNumerics
{
    //[Fact]
    //public void MakeI8()
    //{
    //    var dir_path = @"..\..\..\";
    //    var output = Build("i8");
    //    File.WriteAllText(dir_path + "i8.cs", output);
    //}
    private static readonly TypeInfo[] types = { 
        new TypeInfo("i8"), new TypeInfo("i16"), new TypeInfo("i32"), new TypeInfo("i64"), new TypeInfo("u8"), new TypeInfo("u16"), new TypeInfo("u32"), new TypeInfo("u64"),
        new TypeInfo("i8r"), new TypeInfo("i16r"), new TypeInfo("i32r"), new TypeInfo("i64r"), new TypeInfo("u8r"), new TypeInfo("u16r"), new TypeInfo("u32r"), new TypeInfo("u64r"),
    };

    public void MakeAll()
    {
        const string dir_path = @"..\..\..\";

        foreach (var type in types)
        {
            var output = Build(type);
            File.WriteAllText(dir_path + type.full_name + ".cs", output);
        }
    }

    public void GenTests()
    {
        const string dir_path = @"..\..\..\";
        var output = $@"
//NOTE! AUTO GENERATED

using RefExtendsPublic;
using torc.lang;
using Xunit;
using FluentAssertions;

namespace torc
{{
    public class AllNumericTests 
    {{
        {GenLiteralConversionTest()}
        {GenWindeningConversionTest()}
        {GenArithmeticTest()}
    }}
}}
";

        File.WriteAllText(dir_path + "AllNumericTests.cs", output);
    }

    static IEnumerable<string> ChunksUpto(string str, int maxChunkSize)
    {
        for (int i = 0; i < str.Length; i += maxChunkSize)
            yield return str.Substring(i, Math.Min(maxChunkSize, str.Length - i));
    }

    public string GenLiteralConversionTest()
    {
        var inner = "";
        var tab = "            ";
        foreach (var type in types)
        {
            void genInner(decimal value)
            {
                inner += tab + $"{{ {type.full_name} n = {value}; Assert.Equal<{type.memory_name}>({value}, n); }}\n";

                var binary = type.ToBinary(value);
                binary = String.Join("_", ChunksUpto(binary, 4));
                binary = $"0b{binary}";
                if (value < 0)
                {
                    binary = $"unchecked(({type.GetBackingTypeName()}){binary})";
                }
                inner += tab + $"{{ {type.full_name} n = {value}; Assert.Equal<{type.memory_name}>({binary}, n.v); }}\n";
            }

            if (type.is_signed)
            {
                genInner(0);
                genInner(-1);
            }
            genInner(1);

            genInner(type.GetMinValue());
            genInner(type.GetMaxValue());
            inner += "\n";
        }
        inner = inner.Trim();

        var output = $@"
        //NOTE! AUTO GENERATED
        [Fact]
        public void LiteralToType()
        {{
            {inner}
        }}
";
        return output;
    }

    public string GenWindeningConversionTest()
    {
        var inner = "";
        var tab = "            ";
        foreach (var type in types)
        {
            var widerTypes = GetWideningConversions(type);

            foreach (var widerType in widerTypes)
            {
                inner += tab + $"{{ {type.full_name} n = {type.GetMaxValue()}; {type.full_name}r r = n.r; {widerType.full_name} wider = n; Assert.Equal<{widerType.memory_name}>({type.GetMaxValue()}, wider); wider = r; Assert.Equal<{widerType.memory_name}>({type.GetMaxValue()}, wider);}}\n";
            }

            inner += "\n";
        }
        inner = inner.Trim();

        var output = $@"
        //NOTE! AUTO GENERATED
        [Fact]
        public void WideningConversions()
        {{
            {inner}
        }}
";
        return output;
    }

    public string GenArithmeticTest()
    {
        var inner = "";
        var pickyInner = "";
        var tab = "            ";
        var topDecl = "";
        foreach (var type in types)
        {
            topDecl += tab + $"{type.full_name} {type.full_name} = 1;\n";

            foreach (var otherType in types)
            {
                var resultType = type.GetResultType(otherType);
                if (resultType.width > 64) continue;

                //{ i32 result = u16 + i8; Assert.Equal<int>(2, result); }
                inner += tab + $"{{ {resultType.full_name} result = {type.full_name} + {otherType.full_name}; Assert.Equal<{resultType.memory_name}>(2, result); }}\n";
                pickyInner += tab + $"{{ var result = {type.full_name} + {otherType.full_name}; Assert.IsType<{resultType.full_name}>(result); Assert.Equal<{resultType.memory_name}>(2, result); }}\n";

                resultType = type.GetResultTypeFromLiteral(otherType.GetMaxValue() - 1);
                
                inner += tab + $"{{ {resultType.full_name} result = {type.full_name} + {otherType.GetMaxValue() - 1}; Assert.Equal<{resultType.memory_name}>({otherType.GetMaxValue()}, result); }}\n";

                //{ var result = u16 + i8; Assert.IsType<i32>(result); Assert.Equal<int>(2, result); }
                pickyInner += tab + $"{{ var result = {type.full_name} + {otherType.GetMaxValue() - 1}; Assert.IsType<{resultType.full_name}>(result); Assert.Equal<{resultType.memory_name}>({otherType.GetMaxValue()}, result); }}\n";
            }

            inner += "\n";
        }
        topDecl = topDecl.Trim();
        inner = inner.Trim();
        pickyInner = pickyInner.Trim();

        var output = $@"
        //NOTE! AUTO GENERATED
        [Fact]
        public void Arithmetic()
        {{
            {topDecl}
            {inner}
        }}

        //NOTE! AUTO GENERATED
        [Fact]
        public void PickyTypeArithmetic()
        {{
            {topDecl}
            {pickyInner}
        }}
";
        return output;
    }

    private static string GenOverflowChecks(TypeInfo typeInfo)
    {
        if (typeInfo.width >= 64)
        {
            return "";
        }

            var overflowChecks = $@"
            if (value < {typeInfo.memory_name}.MIN) {{ throw new Exception(""underflow!""); }}
            if (value > {typeInfo.memory_name}.MAX) {{ throw new Exception(""overflow!"");  }}
                ".Trim();

        return overflowChecks;
    }

    public string Build(TypeInfo typeInfo)
    {
        string backing_type = typeInfo.GetBackingTypeName();

        List<TypeInfo> smallerTypes = GetSmallerTypes(typeInfo);

        var wideningConversions = "";
        var wideningConversionsRef = "";

        var asTypeConversions = ""; //needed for mixing signed and unsigned where the unsigned needs promoting

        var widenToTypes = GetWideningConversions(typeInfo);
        foreach (var widerType in widenToTypes)
        {
            wideningConversions    += $"        public static implicit operator {widerType.memory_name}({typeInfo.memory_name} num) {{ return num.read_value; }}\n";
            wideningConversionsRef += $"        public static implicit operator {widerType.memory_name}({typeInfo.memory_name}r num) {{ return num.cp; }}\n";
            asTypeConversions += $"\n        public {widerType.memory_name} as_{widerType.memory_name} => v;";
        }

        var narrowingConversions = "";
        var wrappingConversions = "";
        //var saturatingConversions = FINISH ME //TODO finish

        foreach (var narrowTypeInfo in smallerTypes)
        {
            var narrowTypeName = narrowTypeInfo.memory_name;

            //TODOLOW don't use decimal types
            narrowingConversions += $@"
        public {narrowTypeName} as_{narrowTypeName}_ort {{
            get {{
                var vv = GetBackingValue(this);
                decimal v = vv;
                if (v > {narrowTypeName}.MAX || v < {narrowTypeName}.MIN)
                {{
                    throw new System.OverflowException(""value "" + vv + "" too large for {narrowTypeName}"");
                }}
                return ({narrowTypeInfo.GetBackingTypeName()})vv;
            }}
        }}
";

            wrappingConversions += $"        public {narrowTypeName} wrap_to_{narrowTypeName} => unchecked(({narrowTypeInfo.GetBackingTypeName()})GetBackingValue(this));\n";
        }

        var template = $@"
//NOTE! AUTO GENERATED FILE
using System;
using RefExtendsPublic;

#pragma warning disable IDE1006 // Naming Styles

namespace torc.lang
{{
    public class {typeInfo.memory_name} : WiftObj, IHas{typeInfo.memory_name.ToUpper()}
    {{
        public const {backing_type} MAX = {typeInfo.GetMaxValue()};
        public const {backing_type} MIN = {typeInfo.GetMinValue()};

        protected {backing_type} _value;

        private {typeInfo.memory_name}()
        {{
        }}

        private {typeInfo.memory_name}({backing_type} value)
        {{
            _value = value;
        }}

        private {backing_type} read_value => _value;

        internal static {backing_type} GetBackingValue({typeInfo.memory_name} n) {{ return n.read_value; }}

        public {typeInfo.memory_name} v
        {{
            get
            {{
                ThrowIfDestructed();
                return this.cp;
            }}

            set
            {{
                ThrowIfDestructed();
                this._value = value._value;
            }}
        }}

        public {typeInfo.memory_name}r r {{ get {{ return new {typeInfo.memory_name}r(this); }} }}

        /// <summary>
        /// creates a copy of {typeInfo.memory_name} memory. Useful for when passing to functions.
        /// </summary>
        public {typeInfo.memory_name} cp => new {typeInfo.memory_name}(read_value);

        public static implicit operator {typeInfo.memory_name}({backing_type} num) {{ return new {typeInfo.memory_name}(num); }}
        public static implicit operator {backing_type}({typeInfo.memory_name} num) {{ return num.read_value; }}    //needed
        //public static implicit operator {typeInfo.memory_name}r({typeInfo.memory_name} num) {{ return new {typeInfo.memory_name}r(num); }}

        {asTypeConversions}

        { wideningConversions.Trim() }

        { narrowingConversions.Trim() }

        { wrappingConversions.Trim() }

        { GenComparisonOperator(typeInfo, "==") + "\n" }
        { GenComparisonOperator(typeInfo, "!=") + "\n" }

        { GenOverflowingOperator(typeInfo, "+") + "\n" }


        public override string ToString()
        {{
            return read_value.ToString();
        }}

        public override int GetHashCode()
        {{
            return v.GetHashCode();
        }}

        public override bool Equals(object obj)
        {{
            if (obj == null) {{ return false; }}
            if (ReferenceEquals(this, obj)) {{ return true; }}

            decimal value;

            switch (obj)
            {{
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
            }}

            if (value < MIN || value > MAX) {{ return false; }}
            return value == ({backing_type})value;
        }}
    }}

    ////////////////////////////////////////////////

    public struct {typeInfo.memory_name}r //: IOtherHas{typeInfo.memory_name.ToUpper()}
    {{
        public const {backing_type} MAX = {typeInfo.GetMaxValue()};
        public const {backing_type} MIN = {typeInfo.GetMinValue()};

        private {typeInfo.memory_name} refr_obj;

        public {typeInfo.memory_name}r({typeInfo.memory_name} refr_obj)
        {{
            this.refr_obj = refr_obj;
        }}

        public {typeInfo.memory_name} v
        {{
            get
            {{
                return refr_obj.cp; //TODOLOW think about
            }}

            set
            {{
                refr_obj.v = value;
            }}
        }}

        internal static {backing_type} GetBackingValue({typeInfo.memory_name} n) {{ return {typeInfo.memory_name}.GetBackingValue(n); }}

        /// <summary>
        /// creates a copy of {typeInfo.memory_name} memory. Useful for when passing to functions.
        /// </summary>
        public {typeInfo.memory_name} cp => v;

        /// <summary>
        /// returns a new reference to the same {typeInfo.memory_name} memory that this reference already points to.
        /// </summary>
        public {typeInfo.memory_name}r r => this;

        public void point_to({typeInfo.memory_name} refr_obj) {{ this.refr_obj = refr_obj; }}

        //public static implicit operator {backing_type}({typeInfo.memory_name}r num) {{ return num.read_value; }}
        public static implicit operator {typeInfo.memory_name}({typeInfo.memory_name}r num) {{ return num.cp; }}

        {asTypeConversions}

        { wideningConversionsRef.Trim() }

        { narrowingConversions.Trim() }

        { wrappingConversions.Trim() }

        { GenComparisonOperatorRef(typeInfo.memory_name, "==") + "\n" }
        { GenComparisonOperatorRef(typeInfo.memory_name, "!=") + "\n" }

        public override bool Equals(object obj)
        {{
            return refr_obj.Equals(obj);            
        }}

        public override string ToString()
        {{
            return v.ToString();
        }}

        public override int GetHashCode()
        {{
            return v.GetHashCode();
        }}
    }}
}}
";

        return template;
    }


    private static string GenOverflowingOperator(TypeInfo classType, string op)
    {
        var result = "";

        result += GenOverflowingOperator(classType, classType.full_name, classType, op);

        //for mixing signed and unsigned
        foreach (var otherType in types)
        {
            if (classType.is_signed == otherType.is_signed) continue;    //only care about mixing

            TypeInfo resultType = classType.GetResultType(otherType);
            if (resultType.width > 64) continue;

            if (classType.is_signed == false && classType.CanPromoteToOrViceVersa(otherType) == false)
            {
                result += GenOverflowingOperator(classType, "IHas" + otherType.full_name.ToUpper(), resultType, op, otherValueGetter: $"{otherType.full_name}.GetBackingValue(({otherType.memory_name})b)");
            }
        }

        foreach (var otherType in types)
        {
            TypeInfo resultType = classType.GetResultType(otherType);
            if (resultType.width > 64) continue;

            result += GenOverflowingOperator(classType, otherType.full_name, resultType, op);
        }

        //{ i32 result = i16 + 65534; Assert.Equal<int>(65535, result); }
        foreach (var otherType in types)
        {
            TypeInfo resultType = classType.GetResultType(otherType);
            if (resultType.width > 64) continue;
            if (resultType.width <= classType.width) continue;
            if (classType.CanPromoteTo(otherType) && classType.is_signed == otherType.is_signed)
            {
                result += GenOverflowingOperator(classType, otherType.memory_name, resultType, op);
            }
        }

        return result;
    }

    private static string GenOverflowingOperator(TypeInfo classType, string otherTypeName, TypeInfo resultType, string op, string otherValueGetter = null)
    {
        otherValueGetter = otherValueGetter ?? $"{otherTypeName}.GetBackingValue(b)";

        var template = $@"
        public static {resultType.full_name} operator {op}({classType.full_name} a, {otherTypeName} b)
        {{
            var value = {classType.full_name}.GetBackingValue(a) {op} {otherValueGetter};
            {GenOverflowChecks(resultType)}
            {resultType.full_name} result = ({resultType.GetBackingTypeName()})value;
            return result;
        }}";
        return template.Trim();
    }

    private static string GenOverflowingOperator(string torc_type, string backing_type, string op, string overflowChecks)
    {
        var template = $@"
        public static {torc_type} operator {op}({torc_type} a, {torc_type} b)
        {{
            var value = a.read_value {op} b.read_value;
            {overflowChecks}
            {torc_type} result = ({backing_type})value;
            return result;
        }}";
        return template.Trim();
    }

    private static string GenNonOverflowingOperator(string torc_type, string backing_type, string op)
    {
        var template = $@"
        public static {torc_type} operator {op}({torc_type} a, {torc_type} b)
        {{
            var value = a.read_value {op} b.read_value;
            {torc_type} result = ({backing_type})value;
            return result;
        }}";
        return template.Trim();
    }

    private static string GenNonOverflowingOperatorRef(string torc_type, string backing_type, string op)
    {
        var template = $@"
        public static {torc_type} operator {op}({torc_type}r a, {torc_type} b)
        {{
            var value = {torc_type}r.GetBackingValue(a.v) {op} {torc_type}r.GetBackingValue(b.v);
            {torc_type} result = ({backing_type})value;
            return result;
        }}";
        return template.Trim();
    }

    //List<TypeInfo> GetOtherTypes(TypeInfo type)
    //{
    //    List<TypeInfo> others = new List<TypeInfo>();
    //}

    private static string GenComparisonOperator(TypeInfo classType, string op)
    {
        var result = "";

        result += GenComparisonOperator(classType.full_name, op, classType.full_name);

        return result;
    }

    private static string GenComparisonOperator(string classType, string op, string otherType)
    {
        var template = $@"
        public static bool operator {op}({classType} a, {otherType} b)
        {{
            var result = a.read_value {op} b.read_value;
            return result;
        }}";
        return template.Trim();
    }

    private static string GenComparisonOperatorRef(string torc_type, string op)
    {
        var template = $@"
        public static bool operator {op}({torc_type}r a, {torc_type} b)
        {{
            var result = {torc_type}r.GetBackingValue(a.v) {op} {torc_type}r.GetBackingValue(b.v);
            return result;
        }}";
        return template.Trim();
    }

    private static List<TypeInfo> GetWideningConversions(TypeInfo typeInfo)
    {
        List<TypeInfo> wideningConversions = new List<TypeInfo>();
        AddWideningForWidth(wideningConversions, typeInfo.sign_char, typeInfo.width);

        if (typeInfo.is_signed == false)
        {
            AddWideningForWidth(wideningConversions, 'i', typeInfo.width);
        }

        return wideningConversions;
    }

    private static List<TypeInfo> GetSmallerTypes(TypeInfo info)
    {
        List<TypeInfo> smallerTypes = new List<TypeInfo>();
        int width = info.width;
        var other_sign = info.is_signed ? "u" : "i";

        smallerTypes.Add(new TypeInfo(other_sign + info.width));

        while (true)
        {
            width /= 2;
            if (width < 8)
            {
                break;
            }
            smallerTypes.Add(new TypeInfo("i" + width));
            smallerTypes.Add(new TypeInfo("u" + width));
        }

        return smallerTypes;
    }

    private static void AddWideningForWidth(List<TypeInfo> wideningConversions, char sign_char, int width)
    {
        while (true)
        {
            width *= 2;
            if (width > 64)
            {
                break;
            }
            wideningConversions.Add(new TypeInfo("" + sign_char + width));
        }
    }
}
