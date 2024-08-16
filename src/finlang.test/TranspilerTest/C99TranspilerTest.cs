using finlang.test.TranspilerTest;

namespace finlang.TranspilerTest;

public class C99TranspilerTest
{
    // Disabled in favor of default constructors https://github.com/fin-language/fin/issues/32
    ///// <summary>
    ///// https://github.com/fin-language/fin/issues/75
    ///// </summary>
    //[Fact]
    //public void NoCFileForPlainOldDataTypes_75()
    //{
    //    const string code = """
    //        using finlang;

    //        public class MyClass: FinObj
    //        {
    //            public u8 id;
    //            public u16 count;
    //        }

    //        // to satisfy the compiler
    //        class DummyMain
    //        {
    //            static void Main(string[] args){}
    //        }
    //        """;

    //    var files = TranspilerTestHelper.TranspileFinToCFiles(code);

    //    files.HasFileName("MyClass.h").Should().BeTrue();
    //    files.HasFileName("MyClass.c").Should().BeFalse();
    //}

    /// <summary>
    /// https://github.com/fin-language/fin/issues/30
    /// </summary>
    [Fact]
    public void wrap_add_sub_mul_30()
    {
        const string finCode = """
            using finlang;

            public class MyClass: FinObj
            {
                static void calc()
                {
                    u8 a = 255;
                    u8 b = 2;
                    a = a.wrap_add(b);
                    a = a.wrap_add(3*6);

                    u16 c = 5;
                    c = c.wrap_sub(10/2);

                    u32 d = 256;
                    d = d.wrap_mul(256*256);
                }

                static u32 djb2_hash_2(c_array<u8> data, u8 length)
                {
                    u32 hash = 5381;
                    for (u8 i = 0; i < length; i++)
                    {
                        hash = hash.wrap_mul(33).wrap_add(data.unsafe_get(i));
                    }
                    return hash;
                }
            }
            """;

        var files = TranspilerTestHelper.TranspileFinToCFilesWithDummyMain(finCode);
        string cCode = files.GetSingleWriterTextByFileName("MyClass.c");
        // addition
        cCode.Should().Contain("a = (uint8_t)(a + (b))");
        cCode.Should().Contain("a = (uint8_t)(a + (3*6))");
        // subtraction
        cCode.Should().Contain("c = (uint16_t)(c - (10/2))");
        // multiplication
        cCode.Should().Contain("d = (uint32_t)(d * (256*256))");
        // djb2_hash_2
        cCode.Should().Contain("hash = (uint32_t)((uint32_t)(hash * (33)) + (data[i]))");
    }
}
