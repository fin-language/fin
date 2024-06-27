using finlang.test.TranspilerTest;

namespace finlang.TranspilerTest;

public class C99TranspilerTest
{
    /// <summary>
    /// https://github.com/fin-language/fin/issues/75
    /// </summary>
    [Fact]
    public void NoCFileForPlainOldDataTypes_75()
    {
        const string code = """
            using finlang;

            public class MyClass: FinObj
            {
                public u8 id;
                public u16 count;
            }
                
            // to satisfy the compiler
            class DummyMain
            {
                static void Main(string[] args){}
            }
            """;

        var files = TranspilerTestHelper.TranspileFinToCFiles(code);

        files.HasFileName("MyClass.h").Should().BeTrue();
        files.HasFileName("MyClass.c").Should().BeFalse();
    }
}
