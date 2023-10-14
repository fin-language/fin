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
    public void AddPositiveLiteralTest()
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
        { var c = i8 + 254; c.Should().BeOfType<i16>(); c.Should().Be(255); }
        { var c = i8 + 65534; c.Should().BeOfType<i32>(); c.Should().Be(65535); }
        { var c = i8 + 4294967294; c.Should().BeOfType<i64>(); c.Should().Be(4294967295); }
        { var c = i16 + 126; c.Should().BeOfType<i16>(); c.Should().Be(127); }
        { var c = i16 + 32766; c.Should().BeOfType<i16>(); c.Should().Be(32767); }
        { var c = i16 + 2147483646; c.Should().BeOfType<i32>(); c.Should().Be(2147483647); }
        { var c = i16 + 9223372036854775806; c.Should().BeOfType<i64>(); c.Should().Be(9223372036854775807); }
        { var c = i16 + 254; c.Should().BeOfType<i16>(); c.Should().Be(255); }
        { var c = i16 + 65534; c.Should().BeOfType<i32>(); c.Should().Be(65535); }
        { var c = i16 + 4294967294; c.Should().BeOfType<i64>(); c.Should().Be(4294967295); }
        { var c = i32 + 126; c.Should().BeOfType<i32>(); c.Should().Be(127); }
        { var c = i32 + 32766; c.Should().BeOfType<i32>(); c.Should().Be(32767); }
        { var c = i32 + 2147483646; c.Should().BeOfType<i32>(); c.Should().Be(2147483647); }
        { var c = i32 + 9223372036854775806; c.Should().BeOfType<i64>(); c.Should().Be(9223372036854775807); }
        { var c = i32 + 254; c.Should().BeOfType<i32>(); c.Should().Be(255); }
        { var c = i32 + 65534; c.Should().BeOfType<i32>(); c.Should().Be(65535); }
        { var c = i32 + 4294967294; c.Should().BeOfType<i64>(); c.Should().Be(4294967295); }
        { var c = i64 + 126; c.Should().BeOfType<i64>(); c.Should().Be(127); }
        { var c = i64 + 32766; c.Should().BeOfType<i64>(); c.Should().Be(32767); }
        { var c = i64 + 2147483646; c.Should().BeOfType<i64>(); c.Should().Be(2147483647); }
        { var c = i64 + 9223372036854775806; c.Should().BeOfType<i64>(); c.Should().Be(9223372036854775807); }
        { var c = i64 + 254; c.Should().BeOfType<i64>(); c.Should().Be(255); }
        { var c = i64 + 65534; c.Should().BeOfType<i64>(); c.Should().Be(65535); }
        { var c = i64 + 4294967294; c.Should().BeOfType<i64>(); c.Should().Be(4294967295); }
        { var c = u8 + 126; c.Should().BeOfType<u8>(); c.Should().Be(127); } // error
        { var c = u8 + 32766; c.Should().BeOfType<u16>(); c.Should().Be(32767); }
        { var c = u8 + 2147483646; c.Should().BeOfType<u32>(); c.Should().Be(2147483647); }
        { var c = u8 + 9223372036854775806; c.Should().BeOfType<u64>(); c.Should().Be(9223372036854775807); }
        { var c = u8 + 254; c.Should().BeOfType<u8>(); c.Should().Be(255); }
        { var c = u8 + 65534; c.Should().BeOfType<u16>(); c.Should().Be(65535); }
        { var c = u8 + 4294967294; c.Should().BeOfType<u32>(); c.Should().Be(4294967295); }
        { var c = u8 + 18446744073709551614; c.Should().BeOfType<u64>(); c.Should().Be(18446744073709551615); }
        { var c = u16 + 126; c.Should().BeOfType<u16>(); c.Should().Be(127); }
        { var c = u16 + 32766; c.Should().BeOfType<u16>(); c.Should().Be(32767); }
        { var c = u16 + 2147483646; c.Should().BeOfType<u32>(); c.Should().Be(2147483647); }
        { var c = u16 + 9223372036854775806; c.Should().BeOfType<u64>(); c.Should().Be(9223372036854775807); }
        { var c = u16 + 254; c.Should().BeOfType<u16>(); c.Should().Be(255); }
        { var c = u16 + 65534; c.Should().BeOfType<u16>(); c.Should().Be(65535); }
        { var c = u16 + 4294967294; c.Should().BeOfType<u32>(); c.Should().Be(4294967295); }
        { var c = u16 + 18446744073709551614; c.Should().BeOfType<u64>(); c.Should().Be(18446744073709551615); }
        { var c = u32 + 126; c.Should().BeOfType<u32>(); c.Should().Be(127); }
        { var c = u32 + 32766; c.Should().BeOfType<u32>(); c.Should().Be(32767); }
        { var c = u32 + 2147483646; c.Should().BeOfType<u32>(); c.Should().Be(2147483647); }
        { var c = u32 + 9223372036854775806; c.Should().BeOfType<u64>(); c.Should().Be(9223372036854775807); }
        { var c = u32 + 254; c.Should().BeOfType<u32>(); c.Should().Be(255); }
        { var c = u32 + 65534; c.Should().BeOfType<u32>(); c.Should().Be(65535); }
        { var c = u32 + 4294967294; c.Should().BeOfType<u32>(); c.Should().Be(4294967295); }
        { var c = u32 + 18446744073709551614; c.Should().BeOfType<u64>(); c.Should().Be(18446744073709551615); }
        { var c = u64 + 126; c.Should().BeOfType<u64>(); c.Should().Be(127); }
        { var c = u64 + 32766; c.Should().BeOfType<u64>(); c.Should().Be(32767); }
        { var c = u64 + 2147483646; c.Should().BeOfType<u64>(); c.Should().Be(2147483647); }
        { var c = u64 + 9223372036854775806; c.Should().BeOfType<u64>(); c.Should().Be(9223372036854775807); }
        { var c = u64 + 254; c.Should().BeOfType<u64>(); c.Should().Be(255); }
        { var c = u64 + 65534; c.Should().BeOfType<u64>(); c.Should().Be(65535); }
        { var c = u64 + 4294967294; c.Should().BeOfType<u64>(); c.Should().Be(4294967295); }
        { var c = u64 + 18446744073709551614; c.Should().BeOfType<u64>(); c.Should().Be(18446744073709551615); }
        
    }

    [Fact]
    public void AddPositive1LiteralTest()
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
    
        { var c = i8 + 1; c.Should().BeOfType<i8>(); c.Should().Be(2); }
        { var c = i16 + 1; c.Should().BeOfType<i16>(); c.Should().Be(2); }
        { var c = i32 + 1; c.Should().BeOfType<i32>(); c.Should().Be(2); }
        { var c = i64 + 1; c.Should().BeOfType<i64>(); c.Should().Be(2); }
        { var c = u8 + 1; c.Should().BeOfType<u8>(); c.Should().Be(2); } //error
        { var c = u16 + 1; c.Should().BeOfType<u16>(); c.Should().Be(2); }
        { var c = u32 + 1; c.Should().BeOfType<u32>(); c.Should().Be(2); }
        { var c = u64 + 1; c.Should().BeOfType<u64>(); c.Should().Be(2); }
        
    }

    [Fact]
    public void NegLiteralTest()
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
    
        { var c = i8 + -128; c.Should().BeOfType<i8>(); c.Should().Be(-127); }
        { var c = i8 + -32768; c.Should().BeOfType<i16>(); c.Should().Be(-32767); }
        { var c = i8 + -2147483648; c.Should().BeOfType<i32>(); c.Should().Be(-2147483647); }
        { var c = i8 + -9223372036854775808; c.Should().BeOfType<i64>(); c.Should().Be(-9223372036854775807); }
        { var c = i16 + -128; c.Should().BeOfType<i16>(); c.Should().Be(-127); }
        { var c = i16 + -32768; c.Should().BeOfType<i16>(); c.Should().Be(-32767); }
        { var c = i16 + -2147483648; c.Should().BeOfType<i32>(); c.Should().Be(-2147483647); }
        { var c = i16 + -9223372036854775808; c.Should().BeOfType<i64>(); c.Should().Be(-9223372036854775807); }
        { var c = i32 + -128; c.Should().BeOfType<i32>(); c.Should().Be(-127); }
        { var c = i32 + -32768; c.Should().BeOfType<i32>(); c.Should().Be(-32767); }
        { var c = i32 + -2147483648; c.Should().BeOfType<i32>(); c.Should().Be(-2147483647); }
        { var c = i32 + -9223372036854775808; c.Should().BeOfType<i64>(); c.Should().Be(-9223372036854775807); }
        { var c = i64 + -128; c.Should().BeOfType<i64>(); c.Should().Be(-127); }
        { var c = i64 + -32768; c.Should().BeOfType<i64>(); c.Should().Be(-32767); }
        { var c = i64 + -2147483648; c.Should().BeOfType<i64>(); c.Should().Be(-2147483647); }
        { var c = i64 + -9223372036854775808; c.Should().BeOfType<i64>(); c.Should().Be(-9223372036854775807); }
        { var c = u8 + -128; c.Should().BeOfType<i16>(); c.Should().Be(-127); }
        { var c = u8 + /* required cast */ (i16)(-32768); c.Should().BeOfType<i16>(); c.Should().Be(-32767); }
        { var c = u8 + /* required cast */ (i32)(-2147483648); c.Should().BeOfType<i32>(); c.Should().Be(-2147483647); }
        { var c = u8 + /* required cast */ (i64)(-9223372036854775808); c.Should().BeOfType<i64>(); c.Should().Be(-9223372036854775807); }
        { var c = u16 + /* required cast */ (i8)(-128); c.Should().BeOfType<i32>(); c.Should().Be(-127); }
        { var c = u16 + /* required cast */ (i16)(-32768); c.Should().BeOfType<i32>(); c.Should().Be(-32767); }
        { var c = u16 + /* required cast */ (i32)(-2147483648); c.Should().BeOfType<i32>(); c.Should().Be(-2147483647); }
        { var c = u16 + /* required cast */ (i64)(-9223372036854775808); c.Should().BeOfType<i64>(); c.Should().Be(-9223372036854775807); }
        { var c = u32 + /* required cast */ (i8)(-128); c.Should().BeOfType<i64>(); c.Should().Be(-127); }
        { var c = u32 + /* required cast */ (i16)(-32768); c.Should().BeOfType<i64>(); c.Should().Be(-32767); }
        { var c = u32 + /* required cast */ (i32)(-2147483648); c.Should().BeOfType<i64>(); c.Should().Be(-2147483647); }
        { var c = u32 + /* required cast */ (i64)(-9223372036854775808); c.Should().BeOfType<i64>(); c.Should().Be(-9223372036854775807); }
        
    }

    [Fact]
    public void AddNeg1LiteralTest()
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
    
        { var c = i8 + -1; c.Should().BeOfType<i8>(); c.Should().Be(0); }
        { var c = i16 + -1; c.Should().BeOfType<i16>(); c.Should().Be(0); }
        { var c = i32 + -1; c.Should().BeOfType<i32>(); c.Should().Be(0); }
        { var c = i64 + -1; c.Should().BeOfType<i64>(); c.Should().Be(0); }
        { var c = u8 + /* required cast */ (i8)(-1); c.Should().BeOfType<i16>(); c.Should().Be(0); }
        { var c = u16 + /* required cast */ (i8)(-1); c.Should().BeOfType<i32>(); c.Should().Be(0); }
        { var c = u32 + /* required cast */ (i8)(-1); c.Should().BeOfType<i64>(); c.Should().Be(0); }
        
    }

}