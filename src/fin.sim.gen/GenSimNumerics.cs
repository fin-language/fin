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

            wrappingConversions += $"        public {narrowTypeName} wrap_{narrowTypeName} => unchecked(({narrowTypeInfo.GetBackingTypeName()})GetBackingValue(this));\n";
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
                    public static implicit operator {{widerType.memory_name}}({{typeInfo.memory_name}} num) { return num.read_value; }

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
                            var vv = GetBackingValue(this);
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
                result += GenOverflowingOperator(classType, "IHas" + otherType.full_name.ToUpper(), resultType, op, otherValueGetter: $"{otherType.full_name}.GetBackingValue(({otherType.memory_name})b)");
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
        otherValueGetter = otherValueGetter ?? $"{otherTypeName}.GetBackingValue(b)";

        var template = $$"""

            public static {{resultType.full_name}} operator {{op}}({{classType.full_name}} a, {{otherTypeName}} b)
            {
                var value = {{classType.full_name}}.GetBackingValue(a) {{op}} {{otherValueGetter}};
                {{GenOverflowChecks(resultType).ReplaceLineEndings("\n        ")}}
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
                var value = a.read_value {{op}} b.read_value;
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
                var value = a.read_value {{op}} b.read_value;
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
                var result = a.read_value {{op}} b.read_value;
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
        return $$"""
            //NOTE! AUTO GENERATED FILE
            using System;
            
            #pragma warning disable IDE1006 // Naming Styles
            
            namespace fin.sim.lang;

            public struct {{typeInfo.memory_name}}: IHas{{typeInfo.memory_name.ToUpper()}}
            {
                public const {{backing_type}} MAX = {{typeInfo.GetMaxValue()}};
                public const {{backing_type}} MIN = {{typeInfo.GetMinValue()}};
            
                internal {{backing_type}} _value;
            
                public {{typeInfo.memory_name}}()
                {
                }
            
                private {{typeInfo.memory_name}}({{backing_type}} value)
                {
                    _value = value;
                }
            
                private {{backing_type}} read_value => _value;
            
                internal static {{backing_type}} GetBackingValue({{typeInfo.memory_name}} n) { return n.read_value; }
            
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
                        this._value = value._value;
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
                public static implicit operator {{backing_type}}({{typeInfo.memory_name}} num) { return num.read_value; }
            
                {{explicitWidening}}
            
                {{implicitWidening.Trim()}}
            
                // narrowing conversions
                {{narrowingConversions.Trim()}}
            
                // wrapping conversions
                {{wrappingConversions.Trim()}}
            
                {{GenComparisonOperator(typeInfo, "==") + "\n"}}
                {{GenComparisonOperator(typeInfo, "!=") + "\n"}}
                {{GenComparisonOperator(typeInfo, "<") + "\n"}}
                {{GenComparisonOperator(typeInfo, "<=") + "\n"}}
                {{GenComparisonOperator(typeInfo, ">") + "\n"}}
                {{GenComparisonOperator(typeInfo, ">=") + "\n"}}
            
                {{GenOverflowingOperator(typeInfo, "+") + "\n"}}
            
            
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
                    return obj_value == ({{backing_type}})value;
                }
            }
            """;
    }
}
