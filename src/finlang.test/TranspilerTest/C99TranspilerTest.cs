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
    /// https://github.com/fin-language/fin/issues/80
    /// </summary>
    [Fact]
    public void MinMaxConstants()
    {
        const string code = """
            using finlang;

            public class MyClass: FinObj
            {
                static u8 get_u8_max() {  return u8.MAX /*u8.MAX*/;  }
                static u16 get_u16_max() {  return u16.MAX /*u16.MAX*/;  }
                static u32 get_u32_max() {  return u32.MAX /*u32.MAX*/;  }
                static u64 get_u64_max() {  return u64.MAX /*u64.MAX*/;  }
            
                static i8 get_i8_max() {  return i8.MAX /*i8.MAX*/;  }
                static i16 get_i16_max() {  return i16.MAX /*i16.MAX*/;  }
                static i32 get_i32_max() {  return i32.MAX /*i32.MAX*/;  }
                static i64 get_i64_max() {  return i64.MAX /*i64.MAX*/;  }

                static u8 get_u8_min() {  return u8.MIN /*u8.MIN*/;  }
                static u16 get_u16_min() {  return u16.MIN /*u16.MIN*/;  }
                static u32 get_u32_min() {  return u32.MIN /*u32.MIN*/;  }
                static u64 get_u64_min() {  return u64.MIN /*u64.MIN*/;  }

                static i8 get_i8_min() {  return i8.MIN /*i8.MIN*/;  }
                static i16 get_i16_min() {  return i16.MIN /*i16.MIN*/;  }
                static i32 get_i32_min() {  return i32.MIN /*i32.MIN*/;  }
                static i64 get_i64_min() {  return i64.MIN /*i64.MIN*/;  }
            }
            """;

        var files = TranspilerTestHelper.TranspileFinToCFilesWithDummyMain(code);
        string generatedCode = files.GetSingleWriterTextByFileName("MyClass.c");
        generatedCode.Should().Contain("return UINT8_MAX /*u8.MAX*/;");
        generatedCode.Should().Contain("return UINT16_MAX /*u16.MAX*/;");
        generatedCode.Should().Contain("return UINT32_MAX /*u32.MAX*/;");
        generatedCode.Should().Contain("return UINT64_MAX /*u64.MAX*/;");
        generatedCode.Should().Contain("return INT8_MAX /*i8.MAX*/;");
        generatedCode.Should().Contain("return INT16_MAX /*i16.MAX*/;");
        generatedCode.Should().Contain("return INT32_MAX /*i32.MAX*/;");
        generatedCode.Should().Contain("return INT64_MAX /*i64.MAX*/;");

        generatedCode.Should().Contain("return 0 /*u8.MIN*/;");
        generatedCode.Should().Contain("return 0 /*u16.MIN*/;");
        generatedCode.Should().Contain("return 0 /*u32.MIN*/;");
        generatedCode.Should().Contain("return 0 /*u64.MIN*/;");
        generatedCode.Should().Contain("return INT8_MIN /*i8.MIN*/;");
        generatedCode.Should().Contain("return INT16_MIN /*i16.MIN*/;");
        generatedCode.Should().Contain("return INT32_MIN /*i32.MIN*/;");
        generatedCode.Should().Contain("return INT64_MIN /*i64.MIN*/;");
    }

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
