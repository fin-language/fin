namespace fin.lang.gen;

public class BitOperationGen
{
    internal static TypeInfo[] types => GenSimNumerics.types;

    public static string Gen(TypeInfo classType, string op)
    {
        var code = "";

        if (classType.is_signed)
            return "";

        foreach (TypeInfo otherType in types)
        {
            if (otherType.is_signed)
                continue;

            TypeInfo resultType = classType.GetResultType(otherType);

            code += $$"""

                /// <summary>
                /// Error free operation.<br/>
                /// Transpiles to {{DocHelper.Code($"({resultType.fin_name})(a {op} b)")}}.
                /// </summary>
                public static {{resultType.fin_name}} operator {{op}}({{classType.fin_name}} a, {{otherType.fin_name}} b)
                {
                    // ThrowIfMathModeNotSpecified(); // we don't care for error free operations
                    var result = ({{resultType.fin_name}})(a._csReadValue {{op}} b._csReadValue);
                    return result;
                }

                """;
        }

        return code.IndentNewLines(GenSimNumerics.Indent);
    }

    public static string GenUnary(TypeInfo classType, string op)
    {
        var code = "";

        if (classType.is_signed)
            return "";

        code += $$"""

            /// <summary>
            /// Error free operation.<br/>
            /// Transpiles to {{DocHelper.Code($"({classType.fin_name})({op})")}}.
            /// </summary>
            public static {{classType.fin_name}} operator {{op}}({{classType.fin_name}} a)
            {
                // ThrowIfMathModeNotSpecified(); // we don't care for error free operations
                var result = ({{classType.fin_name}})({{op}}a._csReadValue);
                return result;
            }

            """;

        return code.IndentNewLines(GenSimNumerics.Indent);
    }
}
