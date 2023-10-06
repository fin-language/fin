using Xunit;

namespace fin.sim.gen;

public class GenSimNumerics
{
    internal static readonly TypeInfo[] types = { 
        new TypeInfo("i8"), new TypeInfo("i16"), new TypeInfo("i32"), new TypeInfo("i64"), new TypeInfo("u8"), new TypeInfo("u16"), new TypeInfo("u32"), new TypeInfo("u64"),
    };

    [Fact]
    public void MakeAll()
    {
        string dir_path = TestHelper.GetThisDir() + "/../fin.sim/lang/";

        foreach (var type in types)
        {
            var output = CreateCodeForType(type);
            File.WriteAllText(dir_path + type.full_name + ".cs", output);
        }
    }

    public string CreateCodeForType(TypeInfo typeInfo)
    {
        string backing_type = typeInfo.GetBackingTypeName();

        List<TypeInfo> smallerTypes = GetSmallerTypes(typeInfo);
        List<TypeInfo> widenToTypes = GetWideningConversions(typeInfo);

        GetWideningConversions(typeInfo, out string implicitWidening, out string explicitWidening, widenToTypes);
        GetNarrowConversions(smallerTypes, out string narrowingConversions, out string wrappingConversions);

        string template = RenderTemplate(typeInfo: typeInfo, backing_type: backing_type, implicitWidening: implicitWidening, explicitWidening: explicitWidening, narrowingConversions: narrowingConversions, wrappingConversions: wrappingConversions);

        return template;
    }

    //-------------------------------------------------------------------------------------

    private static string GenOverflowChecks(TypeInfo typeInfo)
    {
        if (typeInfo.width >= 64)
        {
            return "";
        }

        var overflowChecks = $$"""
            if (value < {{typeInfo.memory_name}}.MIN) { throw new Exception("underflow!"); }
            if (value > {{typeInfo.memory_name}}.MAX) { throw new Exception("overflow!");  }
            """;

        return overflowChecks;
    }


    private static void GetNarrowConversions(List<TypeInfo> smallerTypes, out string narrowingConversions, out string wrappingConversions)
    {
        narrowingConversions = "";
        wrappingConversions = "";
        foreach (var narrowTypeInfo in smallerTypes)
        {
            var narrowTypeName = narrowTypeInfo.memory_name;

            narrowingConversions += GenUnsafeToConversion(narrowTypeInfo);

            if (narrowTypeInfo.is_unsigned)
            {
                wrappingConversions += $"""

                    /// <summary>
                    /// Safe explicit wrapping conversion. Truncates upper bits.
                    /// </summary>
                    public {narrowTypeName} wrap_{narrowTypeName} => unchecked(({narrowTypeInfo.GetBackingTypeName()})this._csReadValue);

                """;
            }
        }
    }

    private static void GetWideningConversions(TypeInfo typeInfo, out string implicitWidening, out string explicitWidening, List<TypeInfo> widenToTypes)
    {
        implicitWidening = "";
        explicitWidening = "";

        foreach (var widerType in widenToTypes)
        {
            implicitWidening += $$"""

                    /// <summary>
                    /// Safe implicit widening conversion.
                    /// </summary>
                    public static implicit operator {{widerType.memory_name}}({{typeInfo.memory_name}} num) { return num._csReadValue; }

                """;

            explicitWidening += $"""

                    /// <summary>
                    /// Safe explicit widening conversion.
                    /// </summary>
                    public {widerType.memory_name} {widerType.memory_name} => value;

                """;
        }
    }

    private static string GenUnsafeToConversion(TypeInfo narrowTypeInfo)
    {
        var narrowTypeName = narrowTypeInfo.memory_name;

        //TODOLOW don't use decimal types
        var nc = "";
        nc += "\n";
        nc += $$"""
                    /// <summary>
                    /// Throws during simulation if the value won't fit.
                    /// </summary>
                    public {{narrowTypeName}} unsafe_to_{{narrowTypeName}} {
                        get {
                            var vv = this._csReadValue;
                            decimal v = vv; // will not use decimal in the future to speed up simulations
                            if (v > {{narrowTypeName}}.MAX || v < {{narrowTypeName}}.MIN)
                            {
                                throw new System.OverflowException("value " + vv + " too large for {{narrowTypeName}}");
                            }
                            return ({{narrowTypeInfo.GetBackingTypeName()}})vv;
                        }
                    }
                """;
        nc += "\n";
        return nc;
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
                result += GenOverflowingOperator(classType, "IHas" + otherType.full_name.ToUpper(), resultType, op, otherValueGetter: $"b.value");
            }
        }

        //foreach (var otherType in types)
        //{
        //    TypeInfo resultType = classType.GetResultType(otherType);
        //    if (resultType.width > 64) continue;

        //    result += GenOverflowingOperator(classType, otherType.full_name, resultType, op);
        //}

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

    private static string GenOverflowingOperator(TypeInfo classType, string otherTypeName, TypeInfo resultType, string op, string? otherValueGetter = null)
    {
        otherValueGetter = otherValueGetter ?? $"b._csReadValue";

        var template = $$"""

            public static {{resultType.full_name}} operator {{op}}({{classType.full_name}} a, {{otherTypeName}} b)
            {
                var value = a._csReadValue {{op}} {{otherValueGetter}};
                {{GenOverflowChecks(resultType).IndentNewLines("        ")}}
                {{resultType.full_name}} result = ({{resultType.GetBackingTypeName()}})value;
                return result;
            }
        """;

        return template;
    }

    private static string GenOverflowingOperator(string fin_type, string backing_type, string op, string overflowChecks)
    {
        var template = $$"""
            public static {{fin_type}} operator {{op}}({{fin_type}} a, {{fin_type}} b)
            {
                var value = a._csReadValue {{op}} b._csReadValue;
                {{overflowChecks}}
                {{fin_type}} result = ({{backing_type}})value;
                return result;
            }
        """;
        return template;
    }

    private static string GenNonOverflowingOperator(string fin_type, string backing_type, string op)
    {
        var template = $$"""
            public static {{fin_type}} operator {{op}}({{fin_type}} a, {{fin_type}} b)
            {
                var value = a._csReadValue {{op}} b._csReadValue;
                {{fin_type}} result = ({{backing_type}})value;
                return result;
            }
        """;

        return template;
    }

    private static string GenComparisonOperator(TypeInfo classType, string op)
    {
        var result = "";

        result += GenComparisonOperator(classType.full_name, op, classType.full_name);

        return result;
    }

    private static string GenComparisonOperator(string classType, string op, string otherType)
    {
        var template = $$"""
            public static bool operator {{op}}({{classType}} a, {{otherType}} b)
            {
                var result = a._csReadValue {{op}} b._csReadValue;
                return result;
            }
        """;
        return template.Trim();
    }

    internal static List<TypeInfo> GetWideningConversions(TypeInfo typeInfo)
    {
        List<TypeInfo> wideningConversions = new();
        AddWideningForWidth(wideningConversions, typeInfo.sign_char, typeInfo.width);

        if (typeInfo.is_signed == false)
        {
            AddWideningForWidth(wideningConversions, 'i', typeInfo.width);
        }

        return wideningConversions;
    }

    private static List<TypeInfo> GetSmallerTypes(TypeInfo info)
    {
        List<TypeInfo> smallerTypes = new();
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

    private static string RenderTemplate(TypeInfo typeInfo, string backing_type, string implicitWidening, string explicitWidening, string narrowingConversions, string wrappingConversions)
    {
        static string header(string title)
        {
            var marker = "//################################################################";
            return $"""
                {marker}
                // {title}
                {marker}

                """.IndentNewLines("    ");
        }

        return $$"""
            //NOTE! AUTO GENERATED FILE
            using System;
            
            #pragma warning disable IDE1006 // Naming Styles
            
            namespace fin.sim.lang;

            public struct {{typeInfo.memory_name}}: IHas{{typeInfo.memory_name.ToUpper()}}
            {
                public const {{backing_type}} MAX = {{typeInfo.GetMaxValue()}};
                public const {{backing_type}} MIN = {{typeInfo.GetMinValue()}};
            
                /// <summary>
                /// C# backing value.
                /// </summary>
                internal {{backing_type}} _csValue;
            
                public {{typeInfo.memory_name}}()
                {
                }
            
                private {{typeInfo.memory_name}}({{backing_type}} value)
                {
                    _csValue = value;
                }
            
                /// <summary>
                /// C# read only backing value.
                /// </summary>
                internal {{backing_type}} _csReadValue => _csValue;
            
                public {{typeInfo.memory_name}} value
                {
                    get
                    {
                        // TODO: _ThrowIfDestructed();
                        return this;
                    }
            
                    set
                    {
                        // TODO: _ThrowIfDestructed();
                        this._csValue = value._csValue;
                    }
                }
            
                /// <summary>
                /// Implicit conversion from C# numeric type to fin numeric type.
                /// </summary>
                public static implicit operator {{typeInfo.memory_name}}({{backing_type}} num) { return new {{typeInfo.memory_name}}(num); }

                /// <summary>
                /// Implicit conversion from fin numeric type to C# numeric type.
                /// </summary>
                /// This is needed for technical reasons, but I don't remember them. Should be documented.
                public static implicit operator {{backing_type}}({{typeInfo.memory_name}} num) { return num._csReadValue; }
            
                {{header("widening conversions")}}
                {{explicitWidening}}
            
                {{implicitWidening.Trim()}}
            

                {{header("narrowing conversions")}}
                {{narrowingConversions.Trim()}}
            

                {{header("wrapping conversions (only for unsigned)")}}
                {{wrappingConversions.Trim()}}
            
                {{header("comparisons")}}
                {{GenComparisonOperator(typeInfo, "==") + "\n"}}
                {{GenComparisonOperator(typeInfo, "!=") + "\n"}}
                {{GenComparisonOperator(typeInfo, "<") + "\n"}}
                {{GenComparisonOperator(typeInfo, "<=") + "\n"}}
                {{GenComparisonOperator(typeInfo, ">") + "\n"}}
                {{GenComparisonOperator(typeInfo, ">=") + "\n"}}
            
                {{GenOverflowingOperator(typeInfo, "+") + "\n"}}
            
            
                public override string ToString()
                {
                    return _csReadValue.ToString();
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
            
                        case i8  i: obj_value = i._csReadValue; break;
                        case i16 i: obj_value = i._csReadValue; break;
                        case i32 i: obj_value = i._csReadValue; break;
                        case i64 i: obj_value = i._csReadValue; break;
                        case u8  i: obj_value = i._csReadValue; break;
                        case u16 i: obj_value = i._csReadValue; break;
                        case u32 i: obj_value = i._csReadValue; break;
                        case u64 i: obj_value = i._csReadValue; break;
            
                        default: return false;
                    }
            
                    if (obj_value < MIN || obj_value > MAX) { return false; }
                    return obj_value == ({{backing_type}})value;
                }
            }
            """;
    }
}
