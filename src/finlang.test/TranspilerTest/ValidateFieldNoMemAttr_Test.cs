using finlang.test.TranspilerTest;
using finlang.Transpiler;

namespace finlang.TranspilerTest;

public class ValidateFieldNoMemAttr_Test
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
}
