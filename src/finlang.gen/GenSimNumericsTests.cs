namespace finlang.gen;

public class GenSimNumericsTests
{
    internal static TypeInfo[] types => GenSimNumerics.types;

    public void GenTests()
    {
        const string dir_path = @"..\..\..\";

        var output = $@"
//NOTE! AUTO GENERATED
// TODO comparison operator tests for all types

using Xunit;
using FluentAssertions;

namespace finlang.tests
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
                inner += tab + $"{{ {type.fin_name} n = {value}; Assert.Equal<{type.fin_name}>({value}, n); }}\n";

                var binary = type.ToBinary(value);
                binary = String.Join("_", ChunksUpto(binary, 4));
                binary = $"0b{binary}";
                if (value < 0)
                {
                    binary = $"unchecked(({type.GetBackingTypeName()}){binary})";
                }
                inner += tab + $"{{ {type.fin_name} n = {value}; Assert.Equal<{type.fin_name}>({binary}, n.v); }}\n";
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
            var widerTypes = GenSimNumerics.GetWideningConversions(type);

            foreach (var widerType in widerTypes)
            {
                inner += tab + $"{{ {type.fin_name} n = {type.GetMaxValue()}; {type.fin_name}r r = n.r; {widerType.fin_name} wider = n; Assert.Equal<{widerType.fin_name}>({type.GetMaxValue()}, wider); wider = r; Assert.Equal<{widerType.fin_name}>({type.GetMaxValue()}, wider);}}\n";
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
            topDecl += tab + $"{type.fin_name} {type.fin_name} = 1;\n";

            foreach (var otherType in types)
            {
                var resultType = type.GetResultType(otherType);
                if (resultType.width > 64) continue;

                //{ i32 result = u16 + i8; Assert.Equal<int>(2, result); }
                inner += tab + $"{{ {resultType.fin_name} result = {type.fin_name} + {otherType.fin_name}; Assert.Equal<{resultType.fin_name}>(2, result); }}\n";
                pickyInner += tab + $"{{ var result = {type.fin_name} + {otherType.fin_name}; Assert.IsType<{resultType.fin_name}>(result); Assert.Equal<{resultType.fin_name}>(2, result); }}\n";

                resultType = type.GetResultTypeFromLiteral(otherType.GetMaxValue() - 1);

                inner += tab + $"{{ {resultType.fin_name} result = {type.fin_name} + {otherType.GetMaxValue() - 1}; Assert.Equal<{resultType.fin_name}>({otherType.GetMaxValue()}, result); }}\n";

                //{ var result = u16 + i8; Assert.IsType<i32>(result); Assert.Equal<int>(2, result); }
                pickyInner += tab + $"{{ var result = {type.fin_name} + {otherType.GetMaxValue() - 1}; Assert.IsType<{resultType.fin_name}>(result); Assert.Equal<{resultType.fin_name}>({otherType.GetMaxValue()}, result); }}\n";
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
}
