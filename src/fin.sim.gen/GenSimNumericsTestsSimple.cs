using Xunit;

namespace fin.sim.gen;

public class GenSimNumericsTestsSimple
{
    [Fact]
    public void MakeAll()
    {
        var path = TestHelper.GetThisDir() + "../fin.sim.test/IntegerCombinationTest.cs";

        string code = "";

        var types = GenSimNumerics.types;

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

        var template = $$"""
            // NOTE!!! Auto generated
            using FluentAssertions;
            using Xunit;
            using fin.sim.lang;

            namespace fin.sim.test;

            public class IntegerCombinationTest
            {
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

                    {{code.IndentNewLines("        ")}}
                }
            }
            """;

        File.WriteAllText(path:path, template);
    }
}
