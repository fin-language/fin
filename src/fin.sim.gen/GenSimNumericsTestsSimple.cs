using Xunit;

namespace fin.sim.gen;

public class GenSimNumericsTestsSimple
{
    [Fact]
    public void MakeAll()
    {
        var path = TestHelper.GetThisDir() + "../fin.sim.test/IntegerCombinationTest.cs";

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

                    { var c = i8 + i8; c.Should().BeOfType<i8>(); }
                    { var c = i8 + u8; c.Should().BeOfType<i16>(); }
                }
            }
            """;

        File.WriteAllText(path:path, template);
    }
}
