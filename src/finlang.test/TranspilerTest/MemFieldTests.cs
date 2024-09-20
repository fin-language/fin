using finlang.test.TranspilerTest;
using finlang.Transpiler;

namespace finlang.TranspilerTest;

public class MemFieldTests
{
    [Fact]
    public void c_array()
    {
        const string finCode = """
            using finlang;

            public class MyClass: FinObj
            {
                [mem] c_array<u8> data; // this should not compile

                public MyClass(c_array<u8> data)
                {
                    this.data = data;
                }
            }
            """;

        Action a = () => TranspilerTestHelper.TranspileFinToCFilesWithDummyMain(finCode);
        a.Should().Throw<TranspilerException>().WithMessage("*Don't declare fields of type `c_array<T>` with the `[mem]` attribute. They don't need it.*");
    }

    [Fact]
    public void c_array_sized()
    {
        const string finCode = """
            using finlang;

            public class MyClass: FinObj
            {
                [mem] c_array_sized<u8> data = mem.init(new c_array_sized<u8>(5));
            }
            """;

        Action a = () => TranspilerTestHelper.TranspileFinToCFilesWithDummyMain(finCode);
        a.Should().Throw<TranspilerException>().WithMessage("*Don't declare fields of type `c_array_sized<T>` with the `[mem]` attribute. They don't need it.*");
    }

    [Fact]
    public void primitive_u8()
    {
        const string finCode = """
            using finlang;

            public class MyClass: FinObj
            {
                [mem] u8 count;
            }
            """;

        Action a = () => TranspilerTestHelper.TranspileFinToCFilesWithDummyMain(finCode);
        a.Should().Throw<TranspilerException>().WithMessage("*Primitives like type `finlang.u8` cannot have the `[mem]` attribute*");
    }

    [Fact]
    public void primitive_int()
    {
        const string finCode = """
            using finlang;

            public class MyClass: FinObj
            {
                [mem] int count;
            }
            """;

        Action a = () => TranspilerTestHelper.TranspileFinToCFilesWithDummyMain(finCode);
        a.Should().Throw<TranspilerException>().WithMessage("*Primitives like type `System.Int32` cannot have the `[mem]` attribute*");
    }
}
