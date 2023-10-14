using System;
using Xunit;

namespace fin.sim.gen;

public class GenSimNumericsTestsSimple
{
    [Fact]
    public void MakeAll()
    {
        var path = TestHelper.GetThisDir() + "../fin.sim.test/IntegerCombinationTest.cs";

        var types = GenSimNumerics.types;

        var fileTemplate = $$"""
            // NOTE!!! Auto generated
            namespace fin.sim.test;

            public class IntegerCombinationTest
            {
                {{GenTest1Code(types).IndentNewLines("    ")}}

                {{GenPositiveLiteralTestCode(types).IndentNewLines("    ")}}

                {{GenPositive1LiteralTestCode(types).IndentNewLines("    ")}}

                {{GenAddNegLiteralTestCode(types).IndentNewLines("    ")}}

                {{GenAddNeg1LiteralTestCode(types).IndentNewLines("    ")}}

            }
            """;

        File.WriteAllText(path: path, fileTemplate);
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    private static string GenTest1Code(TypeInfo[] types)
    {
        string code = "";

        foreach (var type in types)
        {
            foreach (var type2 in types)
            {
                var resultType = type.GetResultType(type2);

                if (resultType.width > 64)
                    code += "//";

                code += $"{{ var c = {type.fin_name} + {type2.fin_name}; c.Should().BeOfType<{resultType.fin_name}>(); }}";

                if (resultType.width > 64)
                    code += "  // not allowed for now (need 128 bit or extra logic)";

                code += "\n";
            }
        }

        var test1 = $$"""
            [Fact]
            public void Test1()
            {
                math.unsafe_mode();
                i8 i8 = 1;
                i16 i16 = 1;
                i32 i32 = 1;
                i64 i64 = 1;
                u8 u8 = 1;
                u16 u16 = 1;
                u32 u32 = 1;
                u64 u64 = 1;

                {{code.IndentNewLines("    ")}}
            }
            """;
        return test1;
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    private static string GenPositiveLiteralTestCode(TypeInfo[] types)
    {
        string code = "";

        foreach (var type in types)
        {
            foreach (var type2 in types)
            {
                decimal value = type2.GetMaxValue() - 1;
                var literalType = type.GetResultTypeFromLiteral(value);
                if (literalType == null)
                    continue;

                TypeInfo resultType = type.GetResultType(literalType);

                code += $"{{ var c = {type.fin_name} + {value}; c.Should().BeOfType<{resultType.fin_name}>(); c.Should().Be({value + 1}); }}\n";
            }
        }

        var testCode = $$"""
            [Fact]
            public void AddPositiveLiteralTest()
            {
                math.unsafe_mode();
                i8 i8 = 1;
                i16 i16 = 1;
                i32 i32 = 1;
                i64 i64 = 1;
                u8 u8 = 1;
                u16 u16 = 1;
                u32 u32 = 1;
                u64 u64 = 1;

                {{code.IndentNewLines("    ")}}
            }
            """;
        return testCode;
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    private static string GenPositive1LiteralTestCode(TypeInfo[] types)
    {
        string code = "";

        foreach (var type in types)
        {
            code += $"{{ var c = {type.fin_name} + 1; c.Should().BeOfType<{type.fin_name}>(); c.Should().Be(2); }}\n";
        }

        var testCode = $$"""
            [Fact]
            public void AddPositive1LiteralTest()
            {
                math.unsafe_mode();
                i8 i8 = 1;
                i16 i16 = 1;
                i32 i32 = 1;
                i64 i64 = 1;
                u8 u8 = 1;
                u16 u16 = 1;
                u32 u32 = 1;
                u64 u64 = 1;

                {{code.IndentNewLines("    ")}}
            }
            """;
        return testCode;
    }


    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    private static string GenAddNegLiteralTestCode(TypeInfo[] types)
    {
        string code = "";

        foreach (var type in types)
        {
            foreach (var type2 in types)
            {
                var resultType = type.GetResultType(type2);

                if (resultType.width > 64 || !type2.is_signed)
                    continue;

                string value = type2.GetMinValue() + "";
                if (type.is_unsigned)
                    value = $"/* required cast */ ({type2.fin_name})({value})";

                code += $"{{ var c = {type.fin_name} + {value}; c.Should().BeOfType<{resultType.fin_name}>(); c.Should().Be({type2.GetMinValue() + 1}); }}\n";
            }
        }

        var testCode = $$"""
            [Fact]
            public void NegLiteralTest()
            {
                math.unsafe_mode();
                i8 i8 = 1;
                i16 i16 = 1;
                i32 i32 = 1;
                i64 i64 = 1;
                u8 u8 = 1;
                u16 u16 = 1;
                u32 u32 = 1;
                u64 u64 = 1;

                {{code.IndentNewLines("    ")}}
            }
            """;
        return testCode;
    }



    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    private static string GenAddNeg1LiteralTestCode(TypeInfo[] types)
    {
        string code = "";

        var neg1Type = new TypeInfo("i8");

        foreach (var type in types)
        {
            var resultType = type.GetResultType(neg1Type);
            if (resultType.width > 64)
                continue;

            string value = "-1";
            if (type.is_unsigned)
                value = $"/* required cast */ (i8)({value})";

            code += $"{{ var c = {type.fin_name} + {value}; c.Should().BeOfType<{resultType.fin_name}>(); c.Should().Be(0); }}\n";
        }

        var testCode = $$"""
            [Fact]
            public void AddNeg1LiteralTest()
            {
                math.unsafe_mode();
                i8 i8 = 1;
                i16 i16 = 1;
                i32 i32 = 1;
                i64 i64 = 1;
                u8 u8 = 1;
                u16 u16 = 1;
                u32 u32 = 1;
                u64 u64 = 1;

                {{code.IndentNewLines("    ")}}
            }
            """;
        return testCode;
    }
}
