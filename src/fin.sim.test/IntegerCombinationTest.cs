// NOTE!!! Auto generated
using FluentAssertions;
using Xunit;
using fin.sim.lang;

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
}