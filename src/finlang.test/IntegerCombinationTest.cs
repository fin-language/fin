// NOTE!!! Auto generated
using System;

namespace finlang.test;

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
    
        { var c = i8 + i8; c.Should().BeOfType<i8>(); }{ var c = i8 + i8; c.Should().BeOfType<i8>(); }
        { var c = i8 + i16; c.Should().BeOfType<i16>(); }{ var c = i8 + i16; c.Should().BeOfType<i16>(); }
        { var c = i8 + i32; c.Should().BeOfType<i32>(); }{ var c = i8 + i32; c.Should().BeOfType<i32>(); }
        { var c = i8 + i64; c.Should().BeOfType<i64>(); }{ var c = i8 + i64; c.Should().BeOfType<i64>(); }
        { var c = i8 + u8; c.Should().BeOfType<i16>(); }{ var c = i8 + u8; c.Should().BeOfType<i16>(); }
        { var c = i8 + u16; c.Should().BeOfType<i32>(); }{ var c = i8 + u16; c.Should().BeOfType<i32>(); }
        { var c = i8 + u32; c.Should().BeOfType<i64>(); }{ var c = i8 + u32; c.Should().BeOfType<i64>(); }
        //{ var c = i8 + u64; c.Should().BeOfType<i128>(); }{ var c = i8 + u64; c.Should().BeOfType<i128>(); }  // not allowed for now (need 128 bit or extra logic)
        { var c = i16 + i8; c.Should().BeOfType<i16>(); }{ var c = i16 + i8; c.Should().BeOfType<i16>(); }
        { var c = i16 + i16; c.Should().BeOfType<i16>(); }{ var c = i16 + i16; c.Should().BeOfType<i16>(); }
        { var c = i16 + i32; c.Should().BeOfType<i32>(); }{ var c = i16 + i32; c.Should().BeOfType<i32>(); }
        { var c = i16 + i64; c.Should().BeOfType<i64>(); }{ var c = i16 + i64; c.Should().BeOfType<i64>(); }
        { var c = i16 + u8; c.Should().BeOfType<i16>(); }{ var c = i16 + u8; c.Should().BeOfType<i16>(); }
        { var c = i16 + u16; c.Should().BeOfType<i32>(); }{ var c = i16 + u16; c.Should().BeOfType<i32>(); }
        { var c = i16 + u32; c.Should().BeOfType<i64>(); }{ var c = i16 + u32; c.Should().BeOfType<i64>(); }
        //{ var c = i16 + u64; c.Should().BeOfType<i128>(); }{ var c = i16 + u64; c.Should().BeOfType<i128>(); }  // not allowed for now (need 128 bit or extra logic)
        { var c = i32 + i8; c.Should().BeOfType<i32>(); }{ var c = i32 + i8; c.Should().BeOfType<i32>(); }
        { var c = i32 + i16; c.Should().BeOfType<i32>(); }{ var c = i32 + i16; c.Should().BeOfType<i32>(); }
        { var c = i32 + i32; c.Should().BeOfType<i32>(); }{ var c = i32 + i32; c.Should().BeOfType<i32>(); }
        { var c = i32 + i64; c.Should().BeOfType<i64>(); }{ var c = i32 + i64; c.Should().BeOfType<i64>(); }
        { var c = i32 + u8; c.Should().BeOfType<i32>(); }{ var c = i32 + u8; c.Should().BeOfType<i32>(); }
        { var c = i32 + u16; c.Should().BeOfType<i32>(); }{ var c = i32 + u16; c.Should().BeOfType<i32>(); }
        { var c = i32 + u32; c.Should().BeOfType<i64>(); }{ var c = i32 + u32; c.Should().BeOfType<i64>(); }
        //{ var c = i32 + u64; c.Should().BeOfType<i128>(); }{ var c = i32 + u64; c.Should().BeOfType<i128>(); }  // not allowed for now (need 128 bit or extra logic)
        { var c = i64 + i8; c.Should().BeOfType<i64>(); }{ var c = i64 + i8; c.Should().BeOfType<i64>(); }
        { var c = i64 + i16; c.Should().BeOfType<i64>(); }{ var c = i64 + i16; c.Should().BeOfType<i64>(); }
        { var c = i64 + i32; c.Should().BeOfType<i64>(); }{ var c = i64 + i32; c.Should().BeOfType<i64>(); }
        { var c = i64 + i64; c.Should().BeOfType<i64>(); }{ var c = i64 + i64; c.Should().BeOfType<i64>(); }
        { var c = i64 + u8; c.Should().BeOfType<i64>(); }{ var c = i64 + u8; c.Should().BeOfType<i64>(); }
        { var c = i64 + u16; c.Should().BeOfType<i64>(); }{ var c = i64 + u16; c.Should().BeOfType<i64>(); }
        { var c = i64 + u32; c.Should().BeOfType<i64>(); }{ var c = i64 + u32; c.Should().BeOfType<i64>(); }
        //{ var c = i64 + u64; c.Should().BeOfType<i128>(); }{ var c = i64 + u64; c.Should().BeOfType<i128>(); }  // not allowed for now (need 128 bit or extra logic)
        { var c = u8 + i8; c.Should().BeOfType<i16>(); }{ var c = u8 + i8; c.Should().BeOfType<i16>(); }
        { var c = u8 + i16; c.Should().BeOfType<i16>(); }{ var c = u8 + i16; c.Should().BeOfType<i16>(); }
        { var c = u8 + i32; c.Should().BeOfType<i32>(); }{ var c = u8 + i32; c.Should().BeOfType<i32>(); }
        { var c = u8 + i64; c.Should().BeOfType<i64>(); }{ var c = u8 + i64; c.Should().BeOfType<i64>(); }
        { var c = u8 + u8; c.Should().BeOfType<u8>(); }{ var c = u8 + u8; c.Should().BeOfType<u8>(); }
        { var c = u8 + u16; c.Should().BeOfType<u16>(); }{ var c = u8 + u16; c.Should().BeOfType<u16>(); }
        { var c = u8 + u32; c.Should().BeOfType<u32>(); }{ var c = u8 + u32; c.Should().BeOfType<u32>(); }
        { var c = u8 + u64; c.Should().BeOfType<u64>(); }{ var c = u8 + u64; c.Should().BeOfType<u64>(); }
        { var c = u16 + i8; c.Should().BeOfType<i32>(); }{ var c = u16 + i8; c.Should().BeOfType<i32>(); }
        { var c = u16 + i16; c.Should().BeOfType<i32>(); }{ var c = u16 + i16; c.Should().BeOfType<i32>(); }
        { var c = u16 + i32; c.Should().BeOfType<i32>(); }{ var c = u16 + i32; c.Should().BeOfType<i32>(); }
        { var c = u16 + i64; c.Should().BeOfType<i64>(); }{ var c = u16 + i64; c.Should().BeOfType<i64>(); }
        { var c = u16 + u8; c.Should().BeOfType<u16>(); }{ var c = u16 + u8; c.Should().BeOfType<u16>(); }
        { var c = u16 + u16; c.Should().BeOfType<u16>(); }{ var c = u16 + u16; c.Should().BeOfType<u16>(); }
        { var c = u16 + u32; c.Should().BeOfType<u32>(); }{ var c = u16 + u32; c.Should().BeOfType<u32>(); }
        { var c = u16 + u64; c.Should().BeOfType<u64>(); }{ var c = u16 + u64; c.Should().BeOfType<u64>(); }
        { var c = u32 + i8; c.Should().BeOfType<i64>(); }{ var c = u32 + i8; c.Should().BeOfType<i64>(); }
        { var c = u32 + i16; c.Should().BeOfType<i64>(); }{ var c = u32 + i16; c.Should().BeOfType<i64>(); }
        { var c = u32 + i32; c.Should().BeOfType<i64>(); }{ var c = u32 + i32; c.Should().BeOfType<i64>(); }
        { var c = u32 + i64; c.Should().BeOfType<i64>(); }{ var c = u32 + i64; c.Should().BeOfType<i64>(); }
        { var c = u32 + u8; c.Should().BeOfType<u32>(); }{ var c = u32 + u8; c.Should().BeOfType<u32>(); }
        { var c = u32 + u16; c.Should().BeOfType<u32>(); }{ var c = u32 + u16; c.Should().BeOfType<u32>(); }
        { var c = u32 + u32; c.Should().BeOfType<u32>(); }{ var c = u32 + u32; c.Should().BeOfType<u32>(); }
        { var c = u32 + u64; c.Should().BeOfType<u64>(); }{ var c = u32 + u64; c.Should().BeOfType<u64>(); }
        //{ var c = u64 + i8; c.Should().BeOfType<i128>(); }{ var c = u64 + i8; c.Should().BeOfType<i128>(); }  // not allowed for now (need 128 bit or extra logic)
        //{ var c = u64 + i16; c.Should().BeOfType<i128>(); }{ var c = u64 + i16; c.Should().BeOfType<i128>(); }  // not allowed for now (need 128 bit or extra logic)
        //{ var c = u64 + i32; c.Should().BeOfType<i128>(); }{ var c = u64 + i32; c.Should().BeOfType<i128>(); }  // not allowed for now (need 128 bit or extra logic)
        //{ var c = u64 + i64; c.Should().BeOfType<i128>(); }{ var c = u64 + i64; c.Should().BeOfType<i128>(); }  // not allowed for now (need 128 bit or extra logic)
        { var c = u64 + u8; c.Should().BeOfType<u64>(); }{ var c = u64 + u8; c.Should().BeOfType<u64>(); }
        { var c = u64 + u16; c.Should().BeOfType<u64>(); }{ var c = u64 + u16; c.Should().BeOfType<u64>(); }
        { var c = u64 + u32; c.Should().BeOfType<u64>(); }{ var c = u64 + u32; c.Should().BeOfType<u64>(); }
        { var c = u64 + u64; c.Should().BeOfType<u64>(); }{ var c = u64 + u64; c.Should().BeOfType<u64>(); }
        
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
    
        // fin + literal tests
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
        { var c = u8 + 126; c.Should().BeOfType<u8>(); c.Should().Be(127); }
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
        
    
        // literal + fin tests
        { var c = 126 + i8; c.Should().BeOfType<i8>(); c.Should().Be(127); }
        { var c = i16.from(32766) + i8; c.Should().BeOfType<i16>(); c.Should().Be(32767); }
        { var c = (i16)(32766) + i8; c.Should().BeOfType<i16>(); c.Should().Be(32767); }
        //        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12
        { var c = i32.from(2147483646) + i8; c.Should().BeOfType<i32>(); c.Should().Be(2147483647); }
        { var c = (i32)(2147483646) + i8; c.Should().BeOfType<i32>(); c.Should().Be(2147483647); }
        //        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12
        { var c = i64.from(9223372036854775806) + i8; c.Should().BeOfType<i64>(); c.Should().Be(9223372036854775807); }
        { var c = (i64)(9223372036854775806L) + i8; c.Should().BeOfType<i64>(); c.Should().Be(9223372036854775807); }
        //        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12
        { var c = i16.from(254) + i8; c.Should().BeOfType<i16>(); c.Should().Be(255); }
        { var c = (i16)(254) + i8; c.Should().BeOfType<i16>(); c.Should().Be(255); }
        //        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12
        { var c = i32.from(65534) + i8; c.Should().BeOfType<i32>(); c.Should().Be(65535); }
        { var c = (i32)(65534) + i8; c.Should().BeOfType<i32>(); c.Should().Be(65535); }
        //        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12
        { var c = i64.from(4294967294) + i8; c.Should().BeOfType<i64>(); c.Should().Be(4294967295); }
        { var c = (i64)(4294967294L) + i8; c.Should().BeOfType<i64>(); c.Should().Be(4294967295); }
        //        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12
        { var c = 126 + i16; c.Should().BeOfType<i16>(); c.Should().Be(127); }
        { var c = 32766 + i16; c.Should().BeOfType<i16>(); c.Should().Be(32767); }
        { var c = i32.from(2147483646) + i16; c.Should().BeOfType<i32>(); c.Should().Be(2147483647); }
        { var c = (i32)(2147483646) + i16; c.Should().BeOfType<i32>(); c.Should().Be(2147483647); }
        //        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12
        { var c = i64.from(9223372036854775806) + i16; c.Should().BeOfType<i64>(); c.Should().Be(9223372036854775807); }
        { var c = (i64)(9223372036854775806L) + i16; c.Should().BeOfType<i64>(); c.Should().Be(9223372036854775807); }
        //        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12
        { var c = 254 + i16; c.Should().BeOfType<i16>(); c.Should().Be(255); }
        { var c = i32.from(65534) + i16; c.Should().BeOfType<i32>(); c.Should().Be(65535); }
        { var c = (i32)(65534) + i16; c.Should().BeOfType<i32>(); c.Should().Be(65535); }
        //        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12
        { var c = i64.from(4294967294) + i16; c.Should().BeOfType<i64>(); c.Should().Be(4294967295); }
        { var c = (i64)(4294967294L) + i16; c.Should().BeOfType<i64>(); c.Should().Be(4294967295); }
        //        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12
        { var c = 126 + i32; c.Should().BeOfType<i32>(); c.Should().Be(127); }
        { var c = 32766 + i32; c.Should().BeOfType<i32>(); c.Should().Be(32767); }
        { var c = 2147483646 + i32; c.Should().BeOfType<i32>(); c.Should().Be(2147483647); }
        { var c = i64.from(9223372036854775806) + i32; c.Should().BeOfType<i64>(); c.Should().Be(9223372036854775807); }
        { var c = (i64)(9223372036854775806L) + i32; c.Should().BeOfType<i64>(); c.Should().Be(9223372036854775807); }
        //        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12
        { var c = 254 + i32; c.Should().BeOfType<i32>(); c.Should().Be(255); }
        { var c = 65534 + i32; c.Should().BeOfType<i32>(); c.Should().Be(65535); }
        { var c = i64.from(4294967294) + i32; c.Should().BeOfType<i64>(); c.Should().Be(4294967295); }
        { var c = (i64)(4294967294L) + i32; c.Should().BeOfType<i64>(); c.Should().Be(4294967295); }
        //        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12
        { var c = 126 + i64; c.Should().BeOfType<i64>(); c.Should().Be(127); }
        { var c = 32766 + i64; c.Should().BeOfType<i64>(); c.Should().Be(32767); }
        { var c = 2147483646 + i64; c.Should().BeOfType<i64>(); c.Should().Be(2147483647); }
        { var c = 9223372036854775806 + i64; c.Should().BeOfType<i64>(); c.Should().Be(9223372036854775807); }
        { var c = 254 + i64; c.Should().BeOfType<i64>(); c.Should().Be(255); }
        { var c = 65534 + i64; c.Should().BeOfType<i64>(); c.Should().Be(65535); }
        { var c = 4294967294 + i64; c.Should().BeOfType<i64>(); c.Should().Be(4294967295); }
        { var c = 126 + u8; c.Should().BeOfType<u8>(); c.Should().Be(127); }
        { var c = u16.from(32766) + u8; c.Should().BeOfType<u16>(); c.Should().Be(32767); }
        { var c = (u16)(32766) + u8; c.Should().BeOfType<u16>(); c.Should().Be(32767); }
        //        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12
        { var c = u32.from(2147483646) + u8; c.Should().BeOfType<u32>(); c.Should().Be(2147483647); }
        { var c = (u32)(2147483646) + u8; c.Should().BeOfType<u32>(); c.Should().Be(2147483647); }
        //        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12
        { var c = u64.from(9223372036854775806) + u8; c.Should().BeOfType<u64>(); c.Should().Be(9223372036854775807); }
        { var c = (u64)(9223372036854775806) + u8; c.Should().BeOfType<u64>(); c.Should().Be(9223372036854775807); }
        //        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12
        { var c = 254 + u8; c.Should().BeOfType<u8>(); c.Should().Be(255); }
        { var c = u16.from(65534) + u8; c.Should().BeOfType<u16>(); c.Should().Be(65535); }
        { var c = (u16)(65534) + u8; c.Should().BeOfType<u16>(); c.Should().Be(65535); }
        //        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12
        { var c = u32.from(4294967294) + u8; c.Should().BeOfType<u32>(); c.Should().Be(4294967295); }
        { var c = (u32)(4294967294) + u8; c.Should().BeOfType<u32>(); c.Should().Be(4294967295); }
        //        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12
        { var c = u64.from(18446744073709551614) + u8; c.Should().BeOfType<u64>(); c.Should().Be(18446744073709551615); }
        { var c = (u64)(18446744073709551614) + u8; c.Should().BeOfType<u64>(); c.Should().Be(18446744073709551615); }
        //        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12
        { var c = 126 + u16; c.Should().BeOfType<u16>(); c.Should().Be(127); }
        { var c = 32766 + u16; c.Should().BeOfType<u16>(); c.Should().Be(32767); }
        { var c = u32.from(2147483646) + u16; c.Should().BeOfType<u32>(); c.Should().Be(2147483647); }
        { var c = (u32)(2147483646) + u16; c.Should().BeOfType<u32>(); c.Should().Be(2147483647); }
        //        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12
        { var c = u64.from(9223372036854775806) + u16; c.Should().BeOfType<u64>(); c.Should().Be(9223372036854775807); }
        { var c = (u64)(9223372036854775806) + u16; c.Should().BeOfType<u64>(); c.Should().Be(9223372036854775807); }
        //        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12
        { var c = 254 + u16; c.Should().BeOfType<u16>(); c.Should().Be(255); }
        { var c = 65534 + u16; c.Should().BeOfType<u16>(); c.Should().Be(65535); }
        { var c = u32.from(4294967294) + u16; c.Should().BeOfType<u32>(); c.Should().Be(4294967295); }
        { var c = (u32)(4294967294) + u16; c.Should().BeOfType<u32>(); c.Should().Be(4294967295); }
        //        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12
        { var c = u64.from(18446744073709551614) + u16; c.Should().BeOfType<u64>(); c.Should().Be(18446744073709551615); }
        { var c = (u64)(18446744073709551614) + u16; c.Should().BeOfType<u64>(); c.Should().Be(18446744073709551615); }
        //        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12
        { var c = 126 + u32; c.Should().BeOfType<u32>(); c.Should().Be(127); }
        { var c = 32766 + u32; c.Should().BeOfType<u32>(); c.Should().Be(32767); }
        { var c = 2147483646 + u32; c.Should().BeOfType<u32>(); c.Should().Be(2147483647); }
        { var c = u64.from(9223372036854775806) + u32; c.Should().BeOfType<u64>(); c.Should().Be(9223372036854775807); }
        { var c = (u64)(9223372036854775806) + u32; c.Should().BeOfType<u64>(); c.Should().Be(9223372036854775807); }
        //        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12
        { var c = 254 + u32; c.Should().BeOfType<u32>(); c.Should().Be(255); }
        { var c = 65534 + u32; c.Should().BeOfType<u32>(); c.Should().Be(65535); }
        { var c = 4294967294 + u32; c.Should().BeOfType<u32>(); c.Should().Be(4294967295); }
        { var c = u64.from(18446744073709551614) + u32; c.Should().BeOfType<u64>(); c.Should().Be(18446744073709551615); }
        { var c = (u64)(18446744073709551614) + u32; c.Should().BeOfType<u64>(); c.Should().Be(18446744073709551615); }
        //        ↑↑ conversion above required for https://github.com/fin-language/fin/issues/12
        { var c = 126 + u64; c.Should().BeOfType<u64>(); c.Should().Be(127); }
        { var c = 32766 + u64; c.Should().BeOfType<u64>(); c.Should().Be(32767); }
        { var c = 2147483646 + u64; c.Should().BeOfType<u64>(); c.Should().Be(2147483647); }
        { var c = 9223372036854775806 + u64; c.Should().BeOfType<u64>(); c.Should().Be(9223372036854775807); }
        { var c = 254 + u64; c.Should().BeOfType<u64>(); c.Should().Be(255); }
        { var c = 65534 + u64; c.Should().BeOfType<u64>(); c.Should().Be(65535); }
        { var c = 4294967294 + u64; c.Should().BeOfType<u64>(); c.Should().Be(4294967295); }
        { var c = 18446744073709551614 + u64; c.Should().BeOfType<u64>(); c.Should().Be(18446744073709551615); }
        
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
    
        // fin + literal tests
        { var c = i8 + 1; c.Should().BeOfType<i8>(); c.Should().Be(2); }
        { var c = i16 + 1; c.Should().BeOfType<i16>(); c.Should().Be(2); }
        { var c = i32 + 1; c.Should().BeOfType<i32>(); c.Should().Be(2); }
        { var c = i64 + 1; c.Should().BeOfType<i64>(); c.Should().Be(2); }
        { var c = u8 + 1; c.Should().BeOfType<u8>(); c.Should().Be(2); }
        { var c = u16 + 1; c.Should().BeOfType<u16>(); c.Should().Be(2); }
        { var c = u32 + 1; c.Should().BeOfType<u32>(); c.Should().Be(2); }
        { var c = u64 + 1; c.Should().BeOfType<u64>(); c.Should().Be(2); }
        
    
        // literal + fin tests
        { var c = 1 + i8; c.Should().BeOfType<i8>(); c.Should().Be(2); }
        { var c = 1 + i16; c.Should().BeOfType<i16>(); c.Should().Be(2); }
        { var c = 1 + i32; c.Should().BeOfType<i32>(); c.Should().Be(2); }
        { var c = 1 + i64; c.Should().BeOfType<i64>(); c.Should().Be(2); }
        { var c = 1 + u8; c.Should().BeOfType<u8>(); c.Should().Be(2); }
        { var c = 1 + u16; c.Should().BeOfType<u16>(); c.Should().Be(2); }
        { var c = 1 + u32; c.Should().BeOfType<u32>(); c.Should().Be(2); }
        { var c = 1 + u64; c.Should().BeOfType<u64>(); c.Should().Be(2); }
        
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
    
        // fin + literal tests
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
        { var c = u8 + i8.from(-128); c.Should().BeOfType<i16>(); c.Should().Be(-127); }
        //             conversion above required for https://github.com/fin-language/fin/issues/11
        { var c = u8 + i16.from(-32768); c.Should().BeOfType<i16>(); c.Should().Be(-32767); }
        //             conversion above required for https://github.com/fin-language/fin/issues/11
        { var c = u8 + i32.from(-2147483648); c.Should().BeOfType<i32>(); c.Should().Be(-2147483647); }
        //             conversion above required for https://github.com/fin-language/fin/issues/11
        { var c = u8 + i64.from(-9223372036854775808); c.Should().BeOfType<i64>(); c.Should().Be(-9223372036854775807); }
        //             conversion above required for https://github.com/fin-language/fin/issues/11
        { var c = u16 + i8.from(-128); c.Should().BeOfType<i32>(); c.Should().Be(-127); }
        //             conversion above required for https://github.com/fin-language/fin/issues/11
        { var c = u16 + i16.from(-32768); c.Should().BeOfType<i32>(); c.Should().Be(-32767); }
        //             conversion above required for https://github.com/fin-language/fin/issues/11
        { var c = u16 + i32.from(-2147483648); c.Should().BeOfType<i32>(); c.Should().Be(-2147483647); }
        //             conversion above required for https://github.com/fin-language/fin/issues/11
        { var c = u16 + i64.from(-9223372036854775808); c.Should().BeOfType<i64>(); c.Should().Be(-9223372036854775807); }
        //             conversion above required for https://github.com/fin-language/fin/issues/11
        { var c = u32 + i8.from(-128); c.Should().BeOfType<i64>(); c.Should().Be(-127); }
        //             conversion above required for https://github.com/fin-language/fin/issues/11
        { var c = u32 + i16.from(-32768); c.Should().BeOfType<i64>(); c.Should().Be(-32767); }
        //             conversion above required for https://github.com/fin-language/fin/issues/11
        { var c = u32 + i32.from(-2147483648); c.Should().BeOfType<i64>(); c.Should().Be(-2147483647); }
        //             conversion above required for https://github.com/fin-language/fin/issues/11
        { var c = u32 + i64.from(-9223372036854775808); c.Should().BeOfType<i64>(); c.Should().Be(-9223372036854775807); }
        //             conversion above required for https://github.com/fin-language/fin/issues/11
        
    
        // literal + fin tests
        { var c = -128 + i8; c.Should().BeOfType<i8>(); c.Should().Be(-127); }
        //↓↓ Specifying literal type required for https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12
        {
            { var c = i16.from(-32768) + i8; c.Should().BeOfType<i16>(); c.Should().Be(-32767); }  // .from() is preferred
            { var c = (i16)(-32768) + i8; c.Should().BeOfType<i16>(); c.Should().Be(-32767); }
        }
        //↓↓ Specifying literal type required for https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12
        {
            { var c = i32.from(-2147483648) + i8; c.Should().BeOfType<i32>(); c.Should().Be(-2147483647); }  // .from() is preferred
            { var c = (i32)(-2147483648) + i8; c.Should().BeOfType<i32>(); c.Should().Be(-2147483647); }
        }
        //↓↓ Specifying literal type required for https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12
        {
            { var c = i64.from(-9223372036854775808) + i8; c.Should().BeOfType<i64>(); c.Should().Be(-9223372036854775807); }  // .from() is preferred
            { var c = (i64)(-9223372036854775808) + i8; c.Should().BeOfType<i64>(); c.Should().Be(-9223372036854775807); }
        }
        { var c = -128 + i16; c.Should().BeOfType<i16>(); c.Should().Be(-127); }
        { var c = -32768 + i16; c.Should().BeOfType<i16>(); c.Should().Be(-32767); }
        //↓↓ Specifying literal type required for https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12
        {
            { var c = i32.from(-2147483648) + i16; c.Should().BeOfType<i32>(); c.Should().Be(-2147483647); }  // .from() is preferred
            { var c = (i32)(-2147483648) + i16; c.Should().BeOfType<i32>(); c.Should().Be(-2147483647); }
        }
        //↓↓ Specifying literal type required for https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12
        {
            { var c = i64.from(-9223372036854775808) + i16; c.Should().BeOfType<i64>(); c.Should().Be(-9223372036854775807); }  // .from() is preferred
            { var c = (i64)(-9223372036854775808) + i16; c.Should().BeOfType<i64>(); c.Should().Be(-9223372036854775807); }
        }
        { var c = -128 + i32; c.Should().BeOfType<i32>(); c.Should().Be(-127); }
        { var c = -32768 + i32; c.Should().BeOfType<i32>(); c.Should().Be(-32767); }
        { var c = -2147483648 + i32; c.Should().BeOfType<i32>(); c.Should().Be(-2147483647); }
        //↓↓ Specifying literal type required for https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12
        {
            { var c = i64.from(-9223372036854775808) + i32; c.Should().BeOfType<i64>(); c.Should().Be(-9223372036854775807); }  // .from() is preferred
            { var c = (i64)(-9223372036854775808) + i32; c.Should().BeOfType<i64>(); c.Should().Be(-9223372036854775807); }
        }
        { var c = -128 + i64; c.Should().BeOfType<i64>(); c.Should().Be(-127); }
        { var c = -32768 + i64; c.Should().BeOfType<i64>(); c.Should().Be(-32767); }
        { var c = -2147483648 + i64; c.Should().BeOfType<i64>(); c.Should().Be(-2147483647); }
        { var c = -9223372036854775808 + i64; c.Should().BeOfType<i64>(); c.Should().Be(-9223372036854775807); }
        //↓↓ Unsigned case always requires specifying literal type for negatives https://github.com/fin-language/fin/issues/11, https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12, 
        {
            { var c = i8.from(-128) + u8; c.Should().BeOfType<i16>(); c.Should().Be(-127); }  // .from() is preferred
            { var c = (i8)(-128) + u8; c.Should().BeOfType<i16>(); c.Should().Be(-127); }
        }
        //↓↓ Unsigned case always requires specifying literal type for negatives https://github.com/fin-language/fin/issues/11, https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12, 
        {
            { var c = i16.from(-32768) + u8; c.Should().BeOfType<i16>(); c.Should().Be(-32767); }  // .from() is preferred
            { var c = (i16)(-32768) + u8; c.Should().BeOfType<i16>(); c.Should().Be(-32767); }
        }
        //↓↓ Unsigned case always requires specifying literal type for negatives https://github.com/fin-language/fin/issues/11, https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12, 
        {
            { var c = i32.from(-2147483648) + u8; c.Should().BeOfType<i32>(); c.Should().Be(-2147483647); }  // .from() is preferred
            { var c = (i32)(-2147483648) + u8; c.Should().BeOfType<i32>(); c.Should().Be(-2147483647); }
        }
        //↓↓ Unsigned case always requires specifying literal type for negatives https://github.com/fin-language/fin/issues/11, https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12, 
        {
            { var c = i64.from(-9223372036854775808) + u8; c.Should().BeOfType<i64>(); c.Should().Be(-9223372036854775807); }  // .from() is preferred
            { var c = (i64)(-9223372036854775808) + u8; c.Should().BeOfType<i64>(); c.Should().Be(-9223372036854775807); }
        }
        //↓↓ Unsigned case always requires specifying literal type for negatives https://github.com/fin-language/fin/issues/11, https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12, 
        {
            { var c = i8.from(-128) + u16; c.Should().BeOfType<i32>(); c.Should().Be(-127); }  // .from() is preferred
            { var c = (i8)(-128) + u16; c.Should().BeOfType<i32>(); c.Should().Be(-127); }
        }
        //↓↓ Unsigned case always requires specifying literal type for negatives https://github.com/fin-language/fin/issues/11, https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12, 
        {
            { var c = i16.from(-32768) + u16; c.Should().BeOfType<i32>(); c.Should().Be(-32767); }  // .from() is preferred
            { var c = (i16)(-32768) + u16; c.Should().BeOfType<i32>(); c.Should().Be(-32767); }
        }
        //↓↓ Unsigned case always requires specifying literal type for negatives https://github.com/fin-language/fin/issues/11, https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12, 
        {
            { var c = i32.from(-2147483648) + u16; c.Should().BeOfType<i32>(); c.Should().Be(-2147483647); }  // .from() is preferred
            { var c = (i32)(-2147483648) + u16; c.Should().BeOfType<i32>(); c.Should().Be(-2147483647); }
        }
        //↓↓ Unsigned case always requires specifying literal type for negatives https://github.com/fin-language/fin/issues/11, https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12, 
        {
            { var c = i64.from(-9223372036854775808) + u16; c.Should().BeOfType<i64>(); c.Should().Be(-9223372036854775807); }  // .from() is preferred
            { var c = (i64)(-9223372036854775808) + u16; c.Should().BeOfType<i64>(); c.Should().Be(-9223372036854775807); }
        }
        //↓↓ Unsigned case always requires specifying literal type for negatives https://github.com/fin-language/fin/issues/11, https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12, 
        {
            { var c = i8.from(-128) + u32; c.Should().BeOfType<i64>(); c.Should().Be(-127); }  // .from() is preferred
            { var c = (i8)(-128) + u32; c.Should().BeOfType<i64>(); c.Should().Be(-127); }
        }
        //↓↓ Unsigned case always requires specifying literal type for negatives https://github.com/fin-language/fin/issues/11, https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12, 
        {
            { var c = i16.from(-32768) + u32; c.Should().BeOfType<i64>(); c.Should().Be(-32767); }  // .from() is preferred
            { var c = (i16)(-32768) + u32; c.Should().BeOfType<i64>(); c.Should().Be(-32767); }
        }
        //↓↓ Unsigned case always requires specifying literal type for negatives https://github.com/fin-language/fin/issues/11, https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12, 
        {
            { var c = i32.from(-2147483648) + u32; c.Should().BeOfType<i64>(); c.Should().Be(-2147483647); }  // .from() is preferred
            { var c = (i32)(-2147483648) + u32; c.Should().BeOfType<i64>(); c.Should().Be(-2147483647); }
        }
        //↓↓ Unsigned case always requires specifying literal type for negatives https://github.com/fin-language/fin/issues/11, https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12, 
        {
            { var c = i64.from(-9223372036854775808) + u32; c.Should().BeOfType<i64>(); c.Should().Be(-9223372036854775807); }  // .from() is preferred
            { var c = (i64)(-9223372036854775808) + u32; c.Should().BeOfType<i64>(); c.Should().Be(-9223372036854775807); }
        }
        
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
    
        // fin + literal tests
        { var c = i8 + -1; c.Should().BeOfType<i8>(); c.Should().Be(0); }
        { var c = i16 + -1; c.Should().BeOfType<i16>(); c.Should().Be(0); }
        { var c = i32 + -1; c.Should().BeOfType<i32>(); c.Should().Be(0); }
        { var c = i64 + -1; c.Should().BeOfType<i64>(); c.Should().Be(0); }
        //↓↓ Unsigned case always requires specifying literal type for negatives https://github.com/fin-language/fin/issues/11, https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12, 
        { var c = u8 + i8.from(-1); c.Should().BeOfType<i16>(); c.Should().Be(0); }
        //↓↓ Unsigned case always requires specifying literal type for negatives https://github.com/fin-language/fin/issues/11, https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12, 
        { var c = u16 + i8.from(-1); c.Should().BeOfType<i32>(); c.Should().Be(0); }
        //↓↓ Unsigned case always requires specifying literal type for negatives https://github.com/fin-language/fin/issues/11, https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12, 
        { var c = u32 + i8.from(-1); c.Should().BeOfType<i64>(); c.Should().Be(0); }
        
    
        // literal + fin tests
        { var c = -1 + i8; c.Should().BeOfType<i8>(); c.Should().Be(0); }
        { var c = -1 + i16; c.Should().BeOfType<i16>(); c.Should().Be(0); }
        { var c = -1 + i32; c.Should().BeOfType<i32>(); c.Should().Be(0); }
        { var c = -1 + i64; c.Should().BeOfType<i64>(); c.Should().Be(0); }
        //↓↓ Unsigned case always requires specifying literal type for negatives https://github.com/fin-language/fin/issues/11, https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12, 
        { var c = i8.from(-1) + u8; c.Should().BeOfType<i16>(); c.Should().Be(0); }
        //↓↓ Unsigned case always requires specifying literal type for negatives https://github.com/fin-language/fin/issues/11, https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12, 
        { var c = i8.from(-1) + u16; c.Should().BeOfType<i32>(); c.Should().Be(0); }
        //↓↓ Unsigned case always requires specifying literal type for negatives https://github.com/fin-language/fin/issues/11, https://github.com/fin-language/fin/issues/13, https://github.com/fin-language/fin/issues/12, 
        { var c = i8.from(-1) + u32; c.Should().BeOfType<i64>(); c.Should().Be(0); }
        
    }

    [Fact]
    public void wrap_lshift_LiteralTest()
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
    
        { var c = u8.wrap_lshift(1); c.Should().BeOfType<u8>(); c.Should().Be(1 * 2); }
        { var c = u8.wrap_lshift(8 - 1); c = c.wrap_lshift(1); c.Should().BeOfType<u8>(); c.Should().Be(0, "should have overflowed"); }
        { var c = u8.wrap_lshift(7); c = c.wrap_lshift(1); c.Should().BeOfType<u8>(); c.Should().Be(0, "should have overflowed"); }
        { Action a = () => { u8.wrap_lshift(8); }; a.Should().Throw<OverflowException>("shifting by size of integer type"); }
        { Action a = () => { u8.wrap_lshift(9); }; a.Should().Throw<OverflowException>("shifting by more than size of integer type"); }
        { var c = u8.wrap_lshift(i8); c.Should().BeOfType<u8>(); c.Should().Be(1 * 2); }
        { var c = u8.wrap_lshift((i8)1); c.Should().BeOfType<u8>(); c.Should().Be(1 * 2); }
        { var c = u8.wrap_lshift(i16); c.Should().BeOfType<u8>(); c.Should().Be(1 * 2); }
        { var c = u8.wrap_lshift((i16)1); c.Should().BeOfType<u8>(); c.Should().Be(1 * 2); }
        { var c = u8.wrap_lshift(i32); c.Should().BeOfType<u8>(); c.Should().Be(1 * 2); }
        { var c = u8.wrap_lshift((i32)1); c.Should().BeOfType<u8>(); c.Should().Be(1 * 2); }
        { var c = u8.wrap_lshift(i64); c.Should().BeOfType<u8>(); c.Should().Be(1 * 2); }
        { var c = u8.wrap_lshift((i64)1L); c.Should().BeOfType<u8>(); c.Should().Be(1 * 2); }
        { var c = u8.wrap_lshift(u8); c.Should().BeOfType<u8>(); c.Should().Be(1 * 2); }
        { var c = u8.wrap_lshift((u8)1); c.Should().BeOfType<u8>(); c.Should().Be(1 * 2); }
        { var c = u8.wrap_lshift(u16); c.Should().BeOfType<u8>(); c.Should().Be(1 * 2); }
        { var c = u8.wrap_lshift((u16)1); c.Should().BeOfType<u8>(); c.Should().Be(1 * 2); }
        { var c = u8.wrap_lshift(u32); c.Should().BeOfType<u8>(); c.Should().Be(1 * 2); }
        { var c = u8.wrap_lshift((u32)1); c.Should().BeOfType<u8>(); c.Should().Be(1 * 2); }
        { var c = u8.wrap_lshift(u64); c.Should().BeOfType<u8>(); c.Should().Be(1 * 2); }
        { var c = u8.wrap_lshift((u64)1); c.Should().BeOfType<u8>(); c.Should().Be(1 * 2); }
        { var c = u16.wrap_lshift(1); c.Should().BeOfType<u16>(); c.Should().Be(1 * 2); }
        { var c = u16.wrap_lshift(16 - 1); c = c.wrap_lshift(1); c.Should().BeOfType<u16>(); c.Should().Be(0, "should have overflowed"); }
        { var c = u16.wrap_lshift(15); c = c.wrap_lshift(1); c.Should().BeOfType<u16>(); c.Should().Be(0, "should have overflowed"); }
        { Action a = () => { u16.wrap_lshift(16); }; a.Should().Throw<OverflowException>("shifting by size of integer type"); }
        { Action a = () => { u16.wrap_lshift(17); }; a.Should().Throw<OverflowException>("shifting by more than size of integer type"); }
        { var c = u16.wrap_lshift(i8); c.Should().BeOfType<u16>(); c.Should().Be(1 * 2); }
        { var c = u16.wrap_lshift((i8)1); c.Should().BeOfType<u16>(); c.Should().Be(1 * 2); }
        { var c = u16.wrap_lshift(i16); c.Should().BeOfType<u16>(); c.Should().Be(1 * 2); }
        { var c = u16.wrap_lshift((i16)1); c.Should().BeOfType<u16>(); c.Should().Be(1 * 2); }
        { var c = u16.wrap_lshift(i32); c.Should().BeOfType<u16>(); c.Should().Be(1 * 2); }
        { var c = u16.wrap_lshift((i32)1); c.Should().BeOfType<u16>(); c.Should().Be(1 * 2); }
        { var c = u16.wrap_lshift(i64); c.Should().BeOfType<u16>(); c.Should().Be(1 * 2); }
        { var c = u16.wrap_lshift((i64)1L); c.Should().BeOfType<u16>(); c.Should().Be(1 * 2); }
        { var c = u16.wrap_lshift(u8); c.Should().BeOfType<u16>(); c.Should().Be(1 * 2); }
        { var c = u16.wrap_lshift((u8)1); c.Should().BeOfType<u16>(); c.Should().Be(1 * 2); }
        { var c = u16.wrap_lshift(u16); c.Should().BeOfType<u16>(); c.Should().Be(1 * 2); }
        { var c = u16.wrap_lshift((u16)1); c.Should().BeOfType<u16>(); c.Should().Be(1 * 2); }
        { var c = u16.wrap_lshift(u32); c.Should().BeOfType<u16>(); c.Should().Be(1 * 2); }
        { var c = u16.wrap_lshift((u32)1); c.Should().BeOfType<u16>(); c.Should().Be(1 * 2); }
        { var c = u16.wrap_lshift(u64); c.Should().BeOfType<u16>(); c.Should().Be(1 * 2); }
        { var c = u16.wrap_lshift((u64)1); c.Should().BeOfType<u16>(); c.Should().Be(1 * 2); }
        { var c = u32.wrap_lshift(1); c.Should().BeOfType<u32>(); c.Should().Be(1 * 2); }
        { var c = u32.wrap_lshift(32 - 1); c = c.wrap_lshift(1); c.Should().BeOfType<u32>(); c.Should().Be(0, "should have overflowed"); }
        { var c = u32.wrap_lshift(31); c = c.wrap_lshift(1); c.Should().BeOfType<u32>(); c.Should().Be(0, "should have overflowed"); }
        { Action a = () => { u32.wrap_lshift(32); }; a.Should().Throw<OverflowException>("shifting by size of integer type"); }
        { Action a = () => { u32.wrap_lshift(33); }; a.Should().Throw<OverflowException>("shifting by more than size of integer type"); }
        { var c = u32.wrap_lshift(i8); c.Should().BeOfType<u32>(); c.Should().Be(1 * 2); }
        { var c = u32.wrap_lshift((i8)1); c.Should().BeOfType<u32>(); c.Should().Be(1 * 2); }
        { var c = u32.wrap_lshift(i16); c.Should().BeOfType<u32>(); c.Should().Be(1 * 2); }
        { var c = u32.wrap_lshift((i16)1); c.Should().BeOfType<u32>(); c.Should().Be(1 * 2); }
        { var c = u32.wrap_lshift(i32); c.Should().BeOfType<u32>(); c.Should().Be(1 * 2); }
        { var c = u32.wrap_lshift((i32)1); c.Should().BeOfType<u32>(); c.Should().Be(1 * 2); }
        { var c = u32.wrap_lshift(i64); c.Should().BeOfType<u32>(); c.Should().Be(1 * 2); }
        { var c = u32.wrap_lshift((i64)1L); c.Should().BeOfType<u32>(); c.Should().Be(1 * 2); }
        { var c = u32.wrap_lshift(u8); c.Should().BeOfType<u32>(); c.Should().Be(1 * 2); }
        { var c = u32.wrap_lshift((u8)1); c.Should().BeOfType<u32>(); c.Should().Be(1 * 2); }
        { var c = u32.wrap_lshift(u16); c.Should().BeOfType<u32>(); c.Should().Be(1 * 2); }
        { var c = u32.wrap_lshift((u16)1); c.Should().BeOfType<u32>(); c.Should().Be(1 * 2); }
        { var c = u32.wrap_lshift(u32); c.Should().BeOfType<u32>(); c.Should().Be(1 * 2); }
        { var c = u32.wrap_lshift((u32)1); c.Should().BeOfType<u32>(); c.Should().Be(1 * 2); }
        { var c = u32.wrap_lshift(u64); c.Should().BeOfType<u32>(); c.Should().Be(1 * 2); }
        { var c = u32.wrap_lshift((u64)1); c.Should().BeOfType<u32>(); c.Should().Be(1 * 2); }
        { var c = u64.wrap_lshift(1); c.Should().BeOfType<u64>(); c.Should().Be(1 * 2); }
        { var c = u64.wrap_lshift(64 - 1); c = c.wrap_lshift(1); c.Should().BeOfType<u64>(); c.Should().Be(0, "should have overflowed"); }
        { var c = u64.wrap_lshift(63); c = c.wrap_lshift(1); c.Should().BeOfType<u64>(); c.Should().Be(0, "should have overflowed"); }
        { Action a = () => { u64.wrap_lshift(64); }; a.Should().Throw<OverflowException>("shifting by size of integer type"); }
        { Action a = () => { u64.wrap_lshift(65); }; a.Should().Throw<OverflowException>("shifting by more than size of integer type"); }
        { var c = u64.wrap_lshift(i8); c.Should().BeOfType<u64>(); c.Should().Be(1 * 2); }
        { var c = u64.wrap_lshift((i8)1); c.Should().BeOfType<u64>(); c.Should().Be(1 * 2); }
        { var c = u64.wrap_lshift(i16); c.Should().BeOfType<u64>(); c.Should().Be(1 * 2); }
        { var c = u64.wrap_lshift((i16)1); c.Should().BeOfType<u64>(); c.Should().Be(1 * 2); }
        { var c = u64.wrap_lshift(i32); c.Should().BeOfType<u64>(); c.Should().Be(1 * 2); }
        { var c = u64.wrap_lshift((i32)1); c.Should().BeOfType<u64>(); c.Should().Be(1 * 2); }
        { var c = u64.wrap_lshift(i64); c.Should().BeOfType<u64>(); c.Should().Be(1 * 2); }
        { var c = u64.wrap_lshift((i64)1L); c.Should().BeOfType<u64>(); c.Should().Be(1 * 2); }
        { var c = u64.wrap_lshift(u8); c.Should().BeOfType<u64>(); c.Should().Be(1 * 2); }
        { var c = u64.wrap_lshift((u8)1); c.Should().BeOfType<u64>(); c.Should().Be(1 * 2); }
        { var c = u64.wrap_lshift(u16); c.Should().BeOfType<u64>(); c.Should().Be(1 * 2); }
        { var c = u64.wrap_lshift((u16)1); c.Should().BeOfType<u64>(); c.Should().Be(1 * 2); }
        { var c = u64.wrap_lshift(u32); c.Should().BeOfType<u64>(); c.Should().Be(1 * 2); }
        { var c = u64.wrap_lshift((u32)1); c.Should().BeOfType<u64>(); c.Should().Be(1 * 2); }
        { var c = u64.wrap_lshift(u64); c.Should().BeOfType<u64>(); c.Should().Be(1 * 2); }
        { var c = u64.wrap_lshift((u64)1); c.Should().BeOfType<u64>(); c.Should().Be(1 * 2); }
        
    }
}