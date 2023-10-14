// NOTE!!! Auto generated
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
        { var c = i8 + i16; c.Should().BeOfType<i16>(); }
        { var c = i8 + i32; c.Should().BeOfType<i32>(); }
        { var c = i8 + i64; c.Should().BeOfType<i64>(); }
        { var c = i8 + u8; c.Should().BeOfType<i16>(); }
        { var c = i8 + u16; c.Should().BeOfType<i32>(); }
        { var c = i8 + u32; c.Should().BeOfType<i64>(); }
        //{ var c = i8 + u64; c.Should().BeOfType<i128>(); }  // not allowed for now (need 128 bit or extra logic)
        { var c = i16 + i8; c.Should().BeOfType<i16>(); }
        { var c = i16 + i16; c.Should().BeOfType<i16>(); }
        { var c = i16 + i32; c.Should().BeOfType<i32>(); }
        { var c = i16 + i64; c.Should().BeOfType<i64>(); }
        { var c = i16 + u8; c.Should().BeOfType<i16>(); }
        { var c = i16 + u16; c.Should().BeOfType<i32>(); }
        { var c = i16 + u32; c.Should().BeOfType<i64>(); }
        //{ var c = i16 + u64; c.Should().BeOfType<i128>(); }  // not allowed for now (need 128 bit or extra logic)
        { var c = i32 + i8; c.Should().BeOfType<i32>(); }
        { var c = i32 + i16; c.Should().BeOfType<i32>(); }
        { var c = i32 + i32; c.Should().BeOfType<i32>(); }
        { var c = i32 + i64; c.Should().BeOfType<i64>(); }
        { var c = i32 + u8; c.Should().BeOfType<i32>(); }
        { var c = i32 + u16; c.Should().BeOfType<i32>(); }
        { var c = i32 + u32; c.Should().BeOfType<i64>(); }
        //{ var c = i32 + u64; c.Should().BeOfType<i128>(); }  // not allowed for now (need 128 bit or extra logic)
        { var c = i64 + i8; c.Should().BeOfType<i64>(); }
        { var c = i64 + i16; c.Should().BeOfType<i64>(); }
        { var c = i64 + i32; c.Should().BeOfType<i64>(); }
        { var c = i64 + i64; c.Should().BeOfType<i64>(); }
        { var c = i64 + u8; c.Should().BeOfType<i64>(); }
        { var c = i64 + u16; c.Should().BeOfType<i64>(); }
        { var c = i64 + u32; c.Should().BeOfType<i64>(); }
        //{ var c = i64 + u64; c.Should().BeOfType<i128>(); }  // not allowed for now (need 128 bit or extra logic)
        { var c = u8 + i8; c.Should().BeOfType<i16>(); }
        { var c = u8 + i16; c.Should().BeOfType<i16>(); }
        { var c = u8 + i32; c.Should().BeOfType<i32>(); }
        { var c = u8 + i64; c.Should().BeOfType<i64>(); }
        { var c = u8 + u8; c.Should().BeOfType<u8>(); }
        { var c = u8 + u16; c.Should().BeOfType<u16>(); }
        { var c = u8 + u32; c.Should().BeOfType<u32>(); }
        { var c = u8 + u64; c.Should().BeOfType<u64>(); }
        { var c = u16 + i8; c.Should().BeOfType<i32>(); }
        { var c = u16 + i16; c.Should().BeOfType<i32>(); }
        { var c = u16 + i32; c.Should().BeOfType<i32>(); }
        { var c = u16 + i64; c.Should().BeOfType<i64>(); }
        { var c = u16 + u8; c.Should().BeOfType<u16>(); }
        { var c = u16 + u16; c.Should().BeOfType<u16>(); }
        { var c = u16 + u32; c.Should().BeOfType<u32>(); }
        { var c = u16 + u64; c.Should().BeOfType<u64>(); }
        { var c = u32 + i8; c.Should().BeOfType<i64>(); }
        { var c = u32 + i16; c.Should().BeOfType<i64>(); }
        { var c = u32 + i32; c.Should().BeOfType<i64>(); }
        { var c = u32 + i64; c.Should().BeOfType<i64>(); }
        { var c = u32 + u8; c.Should().BeOfType<u32>(); }
        { var c = u32 + u16; c.Should().BeOfType<u32>(); }
        { var c = u32 + u32; c.Should().BeOfType<u32>(); }
        { var c = u32 + u64; c.Should().BeOfType<u64>(); }
        //{ var c = u64 + i8; c.Should().BeOfType<i128>(); }  // not allowed for now (need 128 bit or extra logic)
        //{ var c = u64 + i16; c.Should().BeOfType<i128>(); }  // not allowed for now (need 128 bit or extra logic)
        //{ var c = u64 + i32; c.Should().BeOfType<i128>(); }  // not allowed for now (need 128 bit or extra logic)
        //{ var c = u64 + i64; c.Should().BeOfType<i128>(); }  // not allowed for now (need 128 bit or extra logic)
        { var c = u64 + u8; c.Should().BeOfType<u64>(); }
        { var c = u64 + u16; c.Should().BeOfType<u64>(); }
        { var c = u64 + u32; c.Should().BeOfType<u64>(); }
        { var c = u64 + u64; c.Should().BeOfType<u64>(); }
        
    }
    [Fact]
    public void SameSignLiteralTest()
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
    
        { var c = i8 + 126; c.Should().BeOfType<i8>(); c.Should().Be(127); }
        { var c = i8 + 32766; c.Should().BeOfType<i16>(); c.Should().Be(32767); }
        { var c = i8 + 2147483646; c.Should().BeOfType<i32>(); c.Should().Be(2147483647); }
        { var c = i8 + 9223372036854775806; c.Should().BeOfType<i64>(); c.Should().Be(9223372036854775807); }
        { var c = i16 + 126; c.Should().BeOfType<i16>(); c.Should().Be(127); }
        { var c = i16 + 32766; c.Should().BeOfType<i16>(); c.Should().Be(32767); }
        { var c = i16 + 2147483646; c.Should().BeOfType<i32>(); c.Should().Be(2147483647); }
        { var c = i16 + 9223372036854775806; c.Should().BeOfType<i64>(); c.Should().Be(9223372036854775807); }
        { var c = i32 + 126; c.Should().BeOfType<i32>(); c.Should().Be(127); }
        { var c = i32 + 32766; c.Should().BeOfType<i32>(); c.Should().Be(32767); }
        { var c = i32 + 2147483646; c.Should().BeOfType<i32>(); c.Should().Be(2147483647); }
        { var c = i32 + 9223372036854775806; c.Should().BeOfType<i64>(); c.Should().Be(9223372036854775807); }
        { var c = i64 + 126; c.Should().BeOfType<i64>(); c.Should().Be(127); }
        { var c = i64 + 32766; c.Should().BeOfType<i64>(); c.Should().Be(32767); }
        { var c = i64 + 2147483646; c.Should().BeOfType<i64>(); c.Should().Be(2147483647); }
        { var c = i64 + 9223372036854775806; c.Should().BeOfType<i64>(); c.Should().Be(9223372036854775807); }
        { var c = u8 + 254; c.Should().BeOfType<u8>(); c.Should().Be(255); }
        { var c = u8 + 65534; c.Should().BeOfType<u16>(); c.Should().Be(65535); }
        { var c = u8 + 4294967294; c.Should().BeOfType<u32>(); c.Should().Be(4294967295); }
        { var c = u8 + 18446744073709551614; c.Should().BeOfType<u64>(); c.Should().Be(18446744073709551615); }
        { var c = u16 + 254; c.Should().BeOfType<u16>(); c.Should().Be(255); }
        { var c = u16 + 65534; c.Should().BeOfType<u16>(); c.Should().Be(65535); }
        { var c = u16 + 4294967294; c.Should().BeOfType<u32>(); c.Should().Be(4294967295); }
        { var c = u16 + 18446744073709551614; c.Should().BeOfType<u64>(); c.Should().Be(18446744073709551615); }
        { var c = u32 + 254; c.Should().BeOfType<u32>(); c.Should().Be(255); }
        { var c = u32 + 65534; c.Should().BeOfType<u32>(); c.Should().Be(65535); }
        { var c = u32 + 4294967294; c.Should().BeOfType<u32>(); c.Should().Be(4294967295); }
        { var c = u32 + 18446744073709551614; c.Should().BeOfType<u64>(); c.Should().Be(18446744073709551615); }
        { var c = u64 + 254; c.Should().BeOfType<u64>(); c.Should().Be(255); }
        { var c = u64 + 65534; c.Should().BeOfType<u64>(); c.Should().Be(65535); }
        { var c = u64 + 4294967294; c.Should().BeOfType<u64>(); c.Should().Be(4294967295); }
        { var c = u64 + 18446744073709551614; c.Should().BeOfType<u64>(); c.Should().Be(18446744073709551615); }
        
    }
    [Fact]
    public void DiffSignLiteralTest()
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
    
        { var c = u8 + -128; c.Should().BeOfType<i16>(); c.Should().Be(128); }
        { var c = u8 + -32768; c.Should().BeOfType<i16>(); c.Should().Be(32768); }
        { var c = u8 + -2147483648; c.Should().BeOfType<i32>(); c.Should().Be(2147483648); }
        { var c = u8 + -9223372036854775808; c.Should().BeOfType<i64>(); c.Should().Be(9223372036854775808); }
        { var c = u16 + -128; c.Should().BeOfType<i32>(); c.Should().Be(128); }
        { var c = u16 + -32768; c.Should().BeOfType<i32>(); c.Should().Be(32768); }
        { var c = u16 + -2147483648; c.Should().BeOfType<i32>(); c.Should().Be(2147483648); }
        { var c = u16 + -9223372036854775808; c.Should().BeOfType<i64>(); c.Should().Be(9223372036854775808); }
        { var c = u32 + -128; c.Should().BeOfType<i64>(); c.Should().Be(128); }
        { var c = u32 + -32768; c.Should().BeOfType<i64>(); c.Should().Be(32768); }
        { var c = u32 + -2147483648; c.Should().BeOfType<i64>(); c.Should().Be(2147483648); }
        { var c = u32 + -9223372036854775808; c.Should().BeOfType<i64>(); c.Should().Be(9223372036854775808); }
        //{ var c = u64 + -128; c.Should().BeOfType<i128>(); c.Should().Be(128); }  // not allowed for now (need 128 bit or extra logic)
        //{ var c = u64 + -32768; c.Should().BeOfType<i128>(); c.Should().Be(32768); }  // not allowed for now (need 128 bit or extra logic)
        //{ var c = u64 + -2147483648; c.Should().BeOfType<i128>(); c.Should().Be(2147483648); }  // not allowed for now (need 128 bit or extra logic)
        //{ var c = u64 + -9223372036854775808; c.Should().BeOfType<i128>(); c.Should().Be(9223372036854775808); }  // not allowed for now (need 128 bit or extra logic)
        
    }
}