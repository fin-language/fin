using System;
using Xunit;

namespace fin.sim.gen;

public class GenSimNumericsTestsSimple
{
    private const string Indent = "    ";

    [Fact]
    public void MakeAll()
    {
        var path = TestHelper.GetThisDir() + "../fin.sim.test/IntegerCombinationTest.cs";

        var types = GenSimNumerics.types;

        var fileTemplate = $$"""
            // NOTE!!! Auto generated
            using System;
            
            namespace fin.sim.test;

            public class IntegerCombinationTest
            {
                {{GenTest1Code(types).IndentNewLines(Indent)}}

                {{GenPositiveLiteralTestCode(types).IndentNewLines(Indent)}}

                {{GenPositive1LiteralTestCode(types).IndentNewLines(Indent)}}

                {{GenAddNegLiteralTestCode(types).IndentNewLines(Indent)}}

                {{GenAddNeg1LiteralTestCode(types).IndentNewLines(Indent)}}

                {{GenLeftShiftLiteralTest(types).IndentNewLines(Indent)}}
            }
            """;

        File.WriteAllText(path: path, fileTemplate);
    }

    private static string GenLeftShiftLiteralTest(TypeInfo[] types)
    {
        string code = "";

        foreach (var type in types)
        {
            if (type.is_signed)
                continue; // only for unsigned right now
            var var_name = type.fin_name;

            code += $"{{ var c = {var_name}.wrap_lshift(1); c.Should().BeOfType<{type.fin_name}>(); c.Should().Be(1 * 2); }}\n";
            code += $"{{ var c = {var_name}.wrap_lshift({type.width} - 1); c = c.wrap_lshift(1); c.Should().BeOfType<{type.fin_name}>(); c.Should().Be(0, \"should have overflowed\"); }}\n";
            code += $"{{ var c = {var_name}.wrap_lshift({type.width - 1}); c = c.wrap_lshift(1); c.Should().BeOfType<{type.fin_name}>(); c.Should().Be(0, \"should have overflowed\"); }}\n";
            code += $$"""{ Action a = () => { {{var_name}}.wrap_lshift({{type.width}}); }; a.Should().Throw<OverflowException>("shifting by size of integer type"); }{{"\n"}}""";
            code += $$"""{ Action a = () => { {{var_name}}.wrap_lshift({{type.width+1}}); }; a.Should().Throw<OverflowException>("shifting by more than size of integer type"); }{{"\n"}}""";

            foreach (var type2 in types)
            {
                decimal value = type2.GetMaxValue() - 1;
                code += $"{{ var c = {var_name}.wrap_lshift({type2.fin_name}); c.Should().BeOfType<{type.fin_name}>(); c.Should().Be(1 * 2); }}\n";
                code += $"{{ var c = {var_name}.wrap_lshift(({type2.fin_name})1); c.Should().BeOfType<{type.fin_name}>(); c.Should().Be(1 * 2); }}\n";
            }
        }

        return $$"""
            [Fact]
            public void wrap_lshift_LiteralTest()
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

                {{code.IndentNewLines(Indent)}}
            }
            """;
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

                {{code.IndentNewLines(Indent)}}
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
        string finPlusLiteral = "";
        string literalPlusFin = "";

        foreach (var type in types)
        {
            foreach (var type2 in types)
            {
                decimal value = type2.GetMaxValue() - 1;
                var literalType = type.GetResultTypeFromLiteral(value);
                if (literalType == null)
                    continue;

                TypeInfo resultType = type.GetResultType(literalType);

                finPlusLiteral += $"{{ var c = {type.fin_name} + {value}; c.Should().BeOfType<{resultType.fin_name}>(); c.Should().Be({value + 1}); }}\n";

                // https://github.com/fin-language/fin/issues/12
                if (literalType.Equals(type) || literalType.CanPromoteTo(type))
                {
                    literalPlusFin += $"{{ var c = {value} + {type.fin_name}; c.Should().BeOfType<{resultType.fin_name}>(); c.Should().Be({value + 1}); }}\n";
                }
                else
                {
                    literalPlusFin += $"{{ var c = {literalType.fin_name}.from({value}) + {type.fin_name}; c.Should().BeOfType<{resultType.fin_name}>(); c.Should().Be({value + 1}); }}\n";
                    literalPlusFin += $"{{ var c = ({literalType.fin_name})({value}) + {type.fin_name}; c.Should().BeOfType<{resultType.fin_name}>(); c.Should().Be({value + 1}); }}\n";
                    literalPlusFin += $"//        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12\n";
                }
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

                // fin + literal tests
                {{finPlusLiteral.IndentNewLines(Indent)}}
            
                // literal + fin tests
                {{literalPlusFin.IndentNewLines(Indent)}}
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
        string finPlusLiteral = "";
        string literalPlusFin = "";

        foreach (var type in types)
        {
            finPlusLiteral += $"{{ var c = {type.fin_name} + 1; c.Should().BeOfType<{type.fin_name}>(); c.Should().Be(2); }}\n";
            literalPlusFin += $"{{ var c = 1 + {type.fin_name}; c.Should().BeOfType<{type.fin_name}>(); c.Should().Be(2); }}\n";
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

                // fin + literal tests
                {{finPlusLiteral.IndentNewLines(Indent)}}
            
                // literal + fin tests
                {{literalPlusFin.IndentNewLines(Indent)}}
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
        string finPlusLiteral = "";
        string literalPlusFin = "";

        foreach (var type in types)
        {
            foreach (var type2 in types)
            {
                var resultType = type.GetResultType(type2);

                if (resultType.width > 64 || !type2.is_signed)
                    continue;

                string value = type2.GetMinValue() + "";
                if (type.is_signed)
                {
                    finPlusLiteral += $"{{ var c = {type.fin_name} + {value}; c.Should().BeOfType<{resultType.fin_name}>(); c.Should().Be({type2.GetMinValue() + 1}); }}\n";
                }
                else
                {
                    // https://github.com/fin-language/fin/issues/11
                    finPlusLiteral += $"{{ var c = {type.fin_name} + {type2.fin_name}.from({value}); c.Should().BeOfType<{resultType.fin_name}>(); c.Should().Be({type2.GetMinValue() + 1}); }}\n";
                    finPlusLiteral += $"//             conversion above required for https://github.com/fin-language/fin/issues/11\n";
                }

                // https://github.com/fin-language/fin/issues/11
                // https://github.com/fin-language/fin/issues/12
                if (type2.Equals(type) || type2.CanPromoteTo(type))
                {
                    literalPlusFin += $"{{ var c = {value} + {type.fin_name}; c.Should().BeOfType<{resultType.fin_name}>(); c.Should().Be({type2.GetMinValue() + 1}); }}\n";
                }
                else
                {
                    if (type.is_signed)
                    {
                        literalPlusFin += $"//↓↓ Specifying literal type required for https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12\n";
                        literalPlusFin += "{\n";
                        literalPlusFin += $"    {{ var c = {type2.fin_name}.from({value}) + {type.fin_name}; c.Should().BeOfType<{resultType.fin_name}>(); c.Should().Be({type2.GetMinValue() + 1}); }}  // .from() is preferred\n";
                        literalPlusFin += $"    {{ var c = ({type2.fin_name})({value}) + {type.fin_name}; c.Should().BeOfType<{resultType.fin_name}>(); c.Should().Be({type2.GetMinValue() + 1}); }}\n";
                        literalPlusFin += "}\n";
                    }
                    else
                    {
                        literalPlusFin += $"//↓↓ Unsigned case always requires specifying literal type for negatives https://github.com/fin-language/fin/issues/11, https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12, \n";
                        literalPlusFin += "{\n";
                        literalPlusFin += $"    {{ var c = {type2.fin_name}.from({value}) + {type.fin_name}; c.Should().BeOfType<{resultType.fin_name}>(); c.Should().Be({type2.GetMinValue() + 1}); }}  // .from() is preferred\n";
                        literalPlusFin += $"    {{ var c = ({type2.fin_name})({value}) + {type.fin_name}; c.Should().BeOfType<{resultType.fin_name}>(); c.Should().Be({type2.GetMinValue() + 1}); }}\n";
                        literalPlusFin += "}\n";
                    }
                }
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

                // fin + literal tests
                {{finPlusLiteral.IndentNewLines(Indent)}}
            
                // literal + fin tests
                {{literalPlusFin.IndentNewLines(Indent)}}
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
        string finPlusLiteral = "";
        string literalPlusFin = "";

        var neg1Type = new TypeInfo("i8");

        foreach (var type in types)
        {
            var resultType = type.GetResultType(neg1Type);
            if (resultType.width > 64)
                continue;

            string value = "-1";
            string preLineInfo = "";
            if (type.is_unsigned)
            {
                //value = $"(i8)({value})";
                value = $"i8.from({value})";
                preLineInfo = $"//↓↓ Unsigned case always requires specifying literal type for negatives https://github.com/fin-language/fin/issues/11, https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12, \n";
            }

            finPlusLiteral += $"{preLineInfo}{{ var c = {type.fin_name} + {value}; c.Should().BeOfType<{resultType.fin_name}>(); c.Should().Be(0); }}\n";

            literalPlusFin += $"{preLineInfo}{{ var c = {value} + {type.fin_name}; c.Should().BeOfType<{resultType.fin_name}>(); c.Should().Be(0); }}\n";
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

                // fin + literal tests
                {{finPlusLiteral.IndentNewLines(Indent)}}

                // literal + fin tests
                {{literalPlusFin.IndentNewLines(Indent)}}
            }
            """;
        return testCode;
    }
}
