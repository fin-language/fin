using finlang.Output;

namespace finlang.test.TranspilerTest;

public class MethodTests : IClassFixture<MethodTests.CompilationFixture>
{
    CompilationFixture compilationFixture;

    public MethodTests(CompilationFixture compilationFixture)
    {
        this.compilationFixture = compilationFixture;
    }

    /// <summary>
    /// https://github.com/fin-language/fin/issues/72
    /// </summary>
    [Fact]
    public void PrivateMethodsNotInHeader()
    {
        // fin: private static void my_priv1() { }
        string hCode = compilationFixture.GetFileCode("MyClass.h");
        hCode.Should().NotContain("my_priv1");

    }

    /// <summary>
    /// https://github.com/fin-language/fin/issues/72
    /// </summary>
    [Fact]
    public void PrivateMethodNameWithoutPrefix()
    {
        string cCode = compilationFixture.GetFileCode("MyClass.c");
        // fin: private static void my_priv1() { }
        cCode.Should().Contain("void my_priv1(");
    }

    /// <summary>
    /// https://github.com/fin-language/fin/issues/72
    /// </summary>
    [Fact]
    public void PrivateMethodDefinedStatic()
    {
        // fin: private static void my_priv1() { }
        string cCode = compilationFixture.GetFileCode("MyClass.c");
        cCode.Should().Contain("static void my_priv1(");
    }

    /// <summary>
    /// https://github.com/fin-language/fin/issues/72
    /// </summary>
    [Fact]
    public void PrivateMethodDeclaredStatic()
    {
        // fin: private static void my_priv1() { }
        string cCode = compilationFixture.GetFileCode("MyClass.c");
        cCode.Should().Contain("static void my_priv1(bool a);");
    }

    //-----------------------------------------------------------------------------------

    public class CompilationFixture : IDisposable
    {
        CapturingTextWriterFactory files;

        public string GetFileCode(string fileName)
        {
            return files.GetSingleWriterTextByFileName(fileName);
        }

        public CompilationFixture()
        {
            const string code = """
                using finlang;

                public class MyClass: FinObj
                {
                    private static void my_priv1(bool a) { }
                    public static void _my_priv2(bool b) { }
                }
                
                // to satisfy the compiler
                class DummyMain
                {
                    static void Main(string[] args){}
                }
                """;
            this.files = TranspilerTestHelper.TranspileFinToCFiles(code);
        }

        public void Dispose()
        {
        }
    }
}
