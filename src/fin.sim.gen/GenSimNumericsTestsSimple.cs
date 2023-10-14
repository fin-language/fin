using Xunit;

namespace fin.sim.gen;

public class GenSimNumericsTestsSimple
{
    [Fact]
    public void MakeAll()
    {
        var path = TestHelper.GetThisDir() + "../fin.sim.test/IntegerCombinationTest.cs";

        var types = GenSimNumerics.types;
        string test1 = GenTest1Code(types);
        string sameSignLiteralTest = GenSameSignLiteralTestCode(types);
        string unsignedVarSignedLiteralTest = GenUnsignedVarSignedLiteralTestCode(types);

        var fileTemplate = $$"""
            // NOTE!!! Auto generated
            namespace fin.sim.test;

            public class IntegerCombinationTest
            {
                {{test1.IndentNewLines("    ")}}
                {{sameSignLiteralTest.IndentNewLines("    ")}}
                {{unsignedVarSignedLiteralTest.IndentNewLines("    ")}}
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
    private static string GenSameSignLiteralTestCode(TypeInfo[] types)
    {
        string code = "";

        foreach (var type in types)
        {
            foreach (var type2 in types)
            {
                if (type.is_signed != type2.is_signed)
                    continue;

                var resultType = type.GetResultType(type2);

                if (resultType.width > 64)
                    code += "//";

                code += $"{{ var c = {type.fin_name} + {type2.GetMaxValue() - 1}; c.Should().BeOfType<{resultType.fin_name}>(); c.Should().Be({type2.GetMaxValue()}); }}";

                if (resultType.width > 64)
                    code += "  // not allowed for now (need 128 bit or extra logic)";

                code += "\n";
            }
        }

        var testCode = $$"""
            [Fact]
            public void SameSignLiteralTest()
            {
                math.unsafe_mode();
                //i8 i8 = 1;
                //i16 i16 = 1;
                //i32 i32 = 1;
                //i64 i64 = 1;
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
    private static string GenUnsignedVarSignedLiteralTestCode(TypeInfo[] types)
    {
        string code = "";

        foreach (var type in types)
        {
            if (type.is_signed)
                continue;

            foreach (var type2 in types)
            {
                if (!type2.is_signed)
                    continue;

                var resultType = type.GetResultType(type2);

                if (resultType.width > 64)
                    code += "//";

                code += $"{{ var c = {type.fin_name} + {type2.GetMinValue()}; c.Should().BeOfType<{resultType.fin_name}>(); c.Should().Be({type2.GetMaxValue() + 1}); }}";

                if (resultType.width > 64)
                    code += "  // not allowed for now (need 128 bit or extra logic)";

                code += "\n";
            }
        }

        var testCode = $$"""
            [Fact]
            public void DiffSignLiteralTest()
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
