using finlang.test.TranspilerTest;
using finlang.Transpiler;

namespace finlang.TranspilerTest;

public class CArrayTests
{
    [Fact]
    public void c_array_sized()
    {
        const string finCode = """
            using finlang;

            public class MyClass: FinObj
            {
                public static void use_array(c_array_sized<u8> data) // this shouldn't compile
                {
                }
            }
            """;

        Action a = () => TranspilerTestHelper.TranspileFinToCFilesWithDummyMain(finCode);
        a.Should().Throw<TranspilerException>().WithMessage("*c_array_sized<T> cannot be used as a parameter because of how C works. Use a c_array<T> instead.*");
    }
}
