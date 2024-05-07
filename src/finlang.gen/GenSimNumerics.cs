using System;
using System.Reflection;
using Xunit;
using System.Linq;

namespace finlang.gen;

public class GenSimNumerics
{
    internal const string Indent = "    ";

    internal static readonly TypeInfo[] types = { 
        new TypeInfo("i8"), new TypeInfo("i16"), new TypeInfo("i32"), new TypeInfo("i64"), new TypeInfo("u8"), new TypeInfo("u16"), new TypeInfo("u32"), new TypeInfo("u64"),
    };

    [Fact]
    public void MakeAll()
    {
        string dir_path = TestHelper.GetThisDir() + "/../finlang/ints/";

        foreach (var type in types)
        {
            var output = CreateCodeForType(type);
            File.WriteAllText(dir_path + type.fin_name + ".cs", output);
        }
    }

    public string CreateCodeForType(TypeInfo typeInfo)
    {
        string backing_type = typeInfo.GetBackingTypeName();

        List<TypeInfo> smallerTypes = GetSmallerTypes(typeInfo);
        List<TypeInfo> widenToTypes = GetWideningConversions(typeInfo);

        GetWideningConversions(typeInfo, out string implicitWidening, out string explicitWidening, widenToTypes);
        GetNarrowConversions(typeInfo, smallerTypes, out string narrowingConversions, out string wrappingConversions);

        string template = RenderTemplate(typeInfo: typeInfo, backing_type: backing_type, implicitWidening: implicitWidening, explicitWidening: explicitWidening, narrowingConversions: narrowingConversions, wrappingConversions: wrappingConversions);

        return template;
    }

    //-------------------------------------------------------------------------------------

    private static string GenOverflowChecks(string op, TypeInfo typeInfo)
    {
        string finType = typeInfo.fin_name;

        var overflowChecks = $$"""
            if (value < {{typeInfo.fin_name}}.MIN) { throw new OverflowException($"Underflow! `{a} ({{finType}}) {{op}} {b} ({{finType}})` result `{value}` is beyond {{finType}} type MIN limit of `{{{typeInfo.fin_name}}.MIN}`. Explicitly widen before `{{op}}` operation."); }
            if (value > {{typeInfo.fin_name}}.MAX) { throw new OverflowException($"Overflow! `{a} ({{finType}}) {{op}} {b} ({{finType}})` result `{value}` is beyond {{finType}} type MAX limit of `{{{typeInfo.fin_name}}.MAX}`. Explicitly widen before `{{op}}` operation."); }
            """;

        return overflowChecks;
    }


    private static void GetNarrowConversions(TypeInfo typeInfo, List<TypeInfo> smallerTypes, out string narrowingConversions, out string wrappingConversions)
    {
        narrowingConversions = "";
        wrappingConversions = "";
        foreach (var narrowTypeInfo in smallerTypes)
        {
            var narrowTypeName = narrowTypeInfo.fin_name;

            narrowingConversions += GenNarrowToConversion(typeInfo, narrowTypeInfo);

            if (typeInfo.is_unsigned && narrowTypeInfo.is_unsigned)
            {
                wrappingConversions += $"""

                    /// <summary>
                    /// Safe explicit wrapping conversion. Truncates upper bits.
                    /// </summary>
                    public {narrowTypeName} wrap_{narrowTypeName} => unchecked(({narrowTypeInfo.GetBackingTypeName()})this._csReadValue);

                """;
            }
        }

        // generate narrow_from() methods
        foreach (var fromType in types)
        {
            var destType = typeInfo;

            // if the destination type is already compatible, skip it
            if (fromType.CanPromoteTo(destType) || fromType == destType)
            {
                continue;
            }

            string valueType;

            if (destType.width == 64 || fromType.width == 64)
            {
                valueType = "decimal";
            }
            else
            {
                valueType = $"{fromType.GetBackingTypeName()}";
            }

            var code = $$"""

                    /// <summary>
                    /// Narrowing conversion from {{fromType}} to {{destType}} when you don't expect data loss.
                    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
                    /// or an exception will be thrown during simulation (if math mode is unsafe).
                    /// </summary>
                    public static {{destType}} narrow_from({{fromType}} v)
                    {
                        ThrowIfMathModeNotSpecified();
                        {{valueType}} value = v._csReadValue;

                        switch (math.CurrentMode)
                        {
                            case math.Mode.Unsafe:
                                if (value < {{destType}}.MIN) { throw new OverflowException($"Underflow! {{typeInfo}} value `{value}` cannot be converted to type {{destType}}."); }
                                if (value > {{destType}}.MAX) { throw new OverflowException($"Overflow! {{typeInfo}} value `{value}` cannot be converted to type {{destType}}."); }
                                break;
                            case math.Mode.UserProvidedErr:
                                if (value < {{destType}}.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                                if (value > {{destType}}.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                                break;
                            default:
                                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
                        }
                        
                        return unchecked(({{destType.GetBackingTypeName()}})value);
                    }

                """;

            narrowingConversions += code;
        }

        // generate casts for larger/incompatible types
        foreach (var fromType in types)
        {
            var destType = typeInfo;

            // if the type is already compatible, skip it
            if (fromType.CanPromoteTo(destType) || fromType == destType)
            {
                continue;
            }

            // from type is either larger or incompatible (signed/unsigned)

            //var narrowingCode = $"return num.narrow_to_{destType}();";

            if (destType.width > fromType.width)
            {
                // this only happens when destType is unsigned and fromType is signed
                // In this case, we only need to check that the value is not negative
                //narrowingCode = $"return ({destType.fin_name})num._csReadValue;";
            }

            narrowingConversions += $$"""

                    /// <summary>
                    /// Narrowing conversion from {{fromType}} to {{destType}} when you don't expect data loss.
                    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
                    /// or an exception will be thrown during simulation (if math mode is unsafe).
                    /// </summary>
                    public static explicit operator {{destType}}({{fromType}} num) => {{destType}}.narrow_from(num);

                    ///// <summary>
                    ///// Narrowing conversion from {{fromType.GetBackingTypeName()}} to {{destType}} when you don't expect data loss.
                    ///// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
                    ///// or an exception will be thrown during simulation (if math mode is unsafe).
                    ///// </summary>
                    public static explicit operator {{destType}}({{fromType.GetBackingTypeName()}} num) => {{destType}}.narrow_from(num);

                """;
        }
    }

    private static void GetWideningConversions(TypeInfo typeInfo, out string implicitWidening, out string explicitWidening, List<TypeInfo> widenToTypes)
    {
        implicitWidening = "";
        explicitWidening = "";

        explicitWidening += $"""

                    /// <summary>
                    /// Safe type self declaration. See https://github.com/fin-language/fin/issues/21
                    /// </summary>
                    public {typeInfo.fin_name} {typeInfo.fin_name}_ => value;

                """;

        foreach (var widerType in widenToTypes)
        {
            implicitWidening += $$"""

                    /// <summary>
                    /// Safe implicit widening conversion.
                    /// </summary>
                    public static implicit operator {{widerType.fin_name}}({{typeInfo.fin_name}} num) { return num._csReadValue; }

                """;

            explicitWidening += $"""

                    /// <summary>
                    /// Safe explicit widening conversion.
                    /// </summary>
                    public {widerType.fin_name} {widerType.fin_name} => value;

                """;
        }
    }

    private static string GenNarrowToConversion(TypeInfo typeInfo, TypeInfo narrowTypeInfo)
    {
        string narrowTypeName = narrowTypeInfo.fin_name;
        string valueType;

        if (typeInfo.width == 64 || narrowTypeInfo.width == 64)
        {
            valueType = "decimal";
        }
        else
        {
            valueType = $"{typeInfo.GetBackingTypeName()}";
        }
        string code = "";

        code += $$"""

                    /// <summary>
                    /// Narrowing conversion from {{typeInfo.fin_name}} to {{narrowTypeName}} when you don't expect data loss.
                    /// If the value won't fit in the destination type, either an error will be set (if math mode is `user provided err`)
                    /// or an exception will be thrown during simulation (if math mode is unsafe).
                    /// </summary>
                    public {{narrowTypeName}} narrow_to_{{narrowTypeName}}()
                    {
                        ThrowIfMathModeNotSpecified();
                        {{valueType}} value = this._csReadValue;

                        switch (math.CurrentMode)
                        {
                            case math.Mode.Unsafe:
                                if (value < {{narrowTypeName}}.MIN) { throw new OverflowException($"Underflow! {{typeInfo.fin_name}} value `{value}` cannot be converted to type {{narrowTypeName}}."); }
                                if (value > {{narrowTypeName}}.MAX) { throw new OverflowException($"Overflow! {{typeInfo.fin_name}} value `{value}` cannot be converted to type {{narrowTypeName}}."); }
                                break;
                            case math.Mode.UserProvidedErr:
                                if (value < {{narrowTypeName}}.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                                if (value > {{narrowTypeName}}.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                                break;
                            default:
                                throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
                        }
                        
                        return unchecked(({{narrowTypeInfo.GetBackingTypeName()}})value);
                    }

                """;


        return code;
    }

    private static string GenOverflowingOperators(TypeInfo classType, string op)
    {
        var result = "";
        var operations = OperationFigurer.DetermineTypes(classType, "b");

        foreach (var opData in operations)
        {
            result += GenOverflowingOperator(opData.classType, opData.otherType, opData.otherTypeArgName, opData.resultType, op, otherValueGetter: opData.otherValueGetter);
        }

        return result;
    }

    public static string GenIHasTypeName(TypeInfo otherType)
    {
        return "IHas" + otherType.fin_name.ToUpper();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="classFinType"></param>
    /// <param name="otherType"></param>
    /// <param name="otherTypeArgName">Sometimes it has to be something like IHasU8.</param>
    /// <param name="resultType"></param>
    /// <param name="op"></param>
    /// <param name="otherValueGetter"></param>
    /// <returns></returns>
    private static string GenOverflowingOperator(TypeInfo classFinType, TypeInfo otherType, string otherTypeArgName, TypeInfo resultType, string op, string otherValueGetter = "b._csReadValue")
    {
        // large type allows us to detect overflow and give nice error messages
        var csLargerType = classFinType.LargeEnoughToDetectOverflow(otherType)?.GetBackingTypeName() ?? "decimal";

        string noteAboutResult;

        if (classFinType != otherType)
            noteAboutResult = $"NOTE: before the `{op}` operation, both operands are treated as {resultType.fin_name}.";
        else
            noteAboutResult = $"Both operands stay of type {classFinType.fin_name} during this operation (no implicit promotion to platform dependent int).";

        var template = $$"""

            /// <summary>
            /// {{noteAboutResult}}<br/>
            /// When math mode is unsafe, this operation will throw during simulation if the value won't fit.<br/>
            /// When math mode is `user provided err`, this operation will add an error if the value won't fit.<br/>
            /// </summary>
            public static {{resultType.fin_name}} operator {{op}}({{classFinType.fin_name}} a, {{otherTypeArgName}} b)
            {
                ThrowIfMathModeNotSpecified();
                var value = ({{csLargerType}})a._csReadValue {{op}} {{otherValueGetter}}; // use `var` as convenience. it will be int when operands are smaller than int.

                switch (math.CurrentMode)
                {
                    case math.Mode.Unsafe:
                        {{GenOverflowChecks(op, resultType).IndentNewLines(Indent, 3)}}
                        break;
                    case math.Mode.UserProvidedErr:
                        if (value < {{classFinType.fin_name}}.MIN) { math.userProvidedErr!.add_without_context(new err.UnderflowError()); }
                        if (value > {{classFinType.fin_name}}.MAX) { math.userProvidedErr!.add_without_context(new err.OverflowError()); }
                        break;
                    default:
                        throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
                }

                {{resultType.fin_name}} result = unchecked(({{resultType.GetBackingTypeName()}})value);
                return result;
            }

        """;

        return template;
    }

    private static string GenComparisonOperators(TypeInfo classType, string op)
    {
        var result = "";
        var operations = OperationFigurer.DetermineTypes(classType, "b");

        foreach (var data in operations)
        {
            result += GenComparisonOperator(data.classType, data.otherType, data.otherTypeArgName, data.resultType, op, otherValueGetter: data.otherValueGetter);
        }

        return result.IndentNewLines(Indent);
    }

    private static string GenComparisonOperator(TypeInfo classFinType, TypeInfo otherType, string otherTypeArgName, TypeInfo resultType, string op, string otherValueGetter = "b._csReadValue")
    {
        string noteAboutResult;

        if (classFinType != otherType)
            noteAboutResult = $"NOTE: before the `{op}` operation, both operands are treated as {resultType.fin_name}.";
        else
            noteAboutResult = $"Both operands stay of type {classFinType.fin_name} during this operation (no implicit promotion to platform dependent int).";

        var template = $$"""

            /// <summary>
            /// {{noteAboutResult}}<br/>
            /// Error free operation.
            /// </summary>
            public static bool operator {{op}}({{classFinType.fin_name}} a, {{otherTypeArgName}} b)
            {
                //ThrowIfMathModeNotSpecified(); // not required as this is error free
                return a._csReadValue {{op}} {{otherValueGetter}};
            }
            """;

        return template;
    }

    private static string GenWrapShiftMethods(TypeInfo classType)
    {
        var result = "";

        if (classType.is_signed)
            return ""; // unsigned only for now

        foreach (var shiftType in types)
        {
            result += GenSimNumericsShifting.AddWrapShiftCode(classType, shiftType, is_left: true);
            //result += GenSimNumericsShifting.AddWrapShiftCode(classType, shiftType, is_left: false);  // not needed for right shifts
            result += GenSimNumericsShifting.AddShiftLeftOperator(classType, shiftType);
            result += GenSimNumericsShifting.AddShiftRightOperator(classType, shiftType);
        }
        return result.Indent(Indent);
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

                """.IndentNewLines(Indent);
        }

        return $$"""
            //NOTE! AUTO GENERATED FILE
            using System;
            
            #pragma warning disable IDE1006 // Naming Styles.
            #pragma warning disable CS0652  // Useless comparison for integer types. Stuff like `u8 < 0`. Not a priority.

            namespace finlang;

            public struct {{typeInfo.fin_name}}: {{GenIHasTypeName(typeInfo)}}
            {
                public const {{backing_type}} MAX = {{typeInfo.GetMaxValue()}};
                public const {{backing_type}} MIN = {{typeInfo.GetMinValue()}};

                /// <summary>
                /// Size of this type in bytes. Equivalent to `sizeof({{typeInfo.GetC99BackingTypeName()}})` in C.
                /// </summary>
                public static readonly u8 SIZE = {{typeInfo.width/8}};

                /// <summary>
                /// C# backing value.
                /// </summary>
                internal {{backing_type}} _csValue;
            
                public {{typeInfo.fin_name}}()
                {
                }
            
                private {{typeInfo.fin_name}}({{backing_type}} value)
                {
                    _csValue = value;
                }
            
                private static void ThrowIfMathModeNotSpecified()
                {
                    math.ThrowIfModeNotSpecified();
                }

                /// <summary>
                /// C# read only backing value.
                /// </summary>
                internal {{backing_type}} _csReadValue => _csValue;
            
                public {{typeInfo.fin_name}} value
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
                /// Useful for when you need to specify an integer literal's type that could be many types.<br/>
                /// For example, in the code below, the `2` integer literal could be many different int types, but we want an i16.
                /// <code>var c = {{typeInfo.fin_name}}.from(2) + my_{{typeInfo.fin_name}};</code>
                /// See https://github.com/fin-language/fin/issues/13
                /// </summary>
                public static {{typeInfo.fin_name}} from({{typeInfo.fin_name}} value) => value;

                /// <summary>
                /// Implicit conversion from C# numeric type to fin numeric type.
                /// </summary>
                public static implicit operator {{typeInfo.fin_name}}({{backing_type}} num) { return new {{typeInfo.fin_name}}(num); }

                /// <summary>
                /// Implicit conversion from fin numeric type to C# numeric type.
                /// </summary>
                /// This is needed for technical reasons, but I don't remember them. Should be documented.
                public static implicit operator {{backing_type}}({{typeInfo.fin_name}} num) { return num._csReadValue; }
            
                {{header("widening conversions")}}
                {{explicitWidening}}
            
                {{implicitWidening.Trim()}}
            

                {{header("narrowing conversions")}}
                {{narrowingConversions.Trim()}}
            

                {{header("wrapping conversions (only for unsigned)")}}
                {{wrappingConversions.Trim()}}
            
                {{header("comparisons")}}
                {{GenComparisonOperators(typeInfo, "==") + "\n"}}
                {{GenComparisonOperators(typeInfo, "!=") + "\n"}}
                {{GenComparisonOperators(typeInfo, "<") + "\n"}}
                {{GenComparisonOperators(typeInfo, "<=") + "\n"}}
                {{GenComparisonOperators(typeInfo, ">") + "\n"}}
                {{GenComparisonOperators(typeInfo, ">=") + "\n"}}
            
                {{GenOverflowingOperators(typeInfo, "+") + "\n"}}
                {{GenOverflowingOperators(typeInfo, "-") + "\n"}}
                {{GenOverflowingOperators(typeInfo, "*") + "\n"}}
            
                {{header("shift methods (unsigned only for now)")}}
                {{GenWrapShiftMethods(typeInfo) + "\n"}}
                
                {{header("bit methods (unsigned only for now)")}}
                {{BitOperationGen.Gen(typeInfo, "&") + "\n"}}
                {{BitOperationGen.Gen(typeInfo, "^") + "\n"}}
                {{BitOperationGen.Gen(typeInfo, "|") + "\n"}}
                {{BitOperationGen.GenUnary(typeInfo, "~") + "\n"}}

                {{header("misc")}}

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
