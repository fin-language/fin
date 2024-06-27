using System;

namespace finlang.test;

public class c_array_Test
{
    [Fact]
    public void Valid()
    {
        u8 length = 5;
        c_array<u8> counts = mem.stack(new c_array<u8>(5));

        for (u8 i = 0; i < length; i++)
            counts.unsafe_set(i, value: i);

        counts._simCsRealMemoryArray.Should().BeEquivalentTo(new u8[] { 0, 1, 2, 3, 4 });
    }

    [Fact]
    public void ValidAlias()
    {
        u8 length = 5;
        c_array<u8> counts0 = mem.stack(new c_array<u8>(5));

        //setup some values
        for (u8 i = 0; i < length; i++)
            counts0.unsafe_set(i, value: i);

        counts0.SimGetValues().Should().BeEquivalentTo(new u8[] { 0, 1, 2, 3, 4 });
     
        // get an alias with offset of 1
        c_array<u8> counts1 = counts0.alias_with_offset(offset: 1);
        counts1.SimGetValues().Should().BeEquivalentTo(new u8[] { 1, 2, 3, 4 });

        // get an alias with offset of 1 from the alias with offset of 1 (a total offset of 2)
        c_array<u8> counts2 = counts1.alias_with_offset(offset: 1);
        counts2.SimGetValues().Should().BeEquivalentTo(new u8[] { 2, 3, 4 });
        counts2 = counts0.alias_with_offset(offset: 2);
        counts2.SimGetValues().Should().BeEquivalentTo(new u8[] { 2, 3, 4 });

        counts0.unsafe_get(2).Should().Be(2);
        counts1.unsafe_get(1).Should().Be(2);
        counts2.unsafe_get(0).Should().Be(2);

        counts0.unsafe_set(2, 22);
        counts0.SimGetValues().Should().BeEquivalentTo(new u8[] { 0, 1, 22, 3, 4 });
        counts1.SimGetValues().Should().BeEquivalentTo(new u8[] { 1, 22, 3, 4 });
        counts2.SimGetValues().Should().BeEquivalentTo(new u8[] { 22, 3, 4 });
        counts0.unsafe_get(2).Should().Be(22);
        counts1.unsafe_get(1).Should().Be(22);
        counts2.unsafe_get(0).Should().Be(22);

        // get a negative offset from counts2
        c_array<u8> counts2n1 = counts2.alias_with_offset(offset: -1);
        counts2n1.SimGetValues().Should().BeEquivalentTo(new u8[] { 1, 22, 3, 4 });
        counts2n1.unsafe_get(0).Should().Be(1);
    }

    [Fact]
    public void WriteOutOfBounds()
    {
        c_array<u8> counts = mem.stack(new c_array<u8>(5));

        Action action;
        action = () => counts.unsafe_set(5, value: 1);
        action.Should().Throw<IndexOutOfRangeException>().WithMessage("Attempted writing value `1` to invalid index `5` of C style naked array of length 5. https://github.com/fin-language/fin/issues/14");

        action = () => counts.unsafe_set(-5, value: 88);
        action.Should().Throw<IndexOutOfRangeException>().WithMessage("Attempted writing value `88` to invalid index `-5` of C style naked array of length 5. https://github.com/fin-language/fin/issues/14");

        i8 index = -1;
        action = () => counts.unsafe_set(index, value: 0);
        action.Should().Throw<IndexOutOfRangeException>().WithMessage("Attempted writing value `0` to invalid index `-1` of C style naked array of length 5. https://github.com/fin-language/fin/issues/14");

        index = -128;
        action = () => counts.unsafe_set(index, value: 0);
        action.Should().Throw<IndexOutOfRangeException>().WithMessage("Attempted writing value `0` to invalid index `-128` of C style naked array of length 5. https://github.com/fin-language/fin/issues/14");
    }

    [Fact]
    public void ReadOutOfBounds()
    {
        c_array<u8> counts = mem.stack(new c_array<u8>(5));

        Action action;
        action = () => counts.unsafe_get(-1);
        action.Should().Throw<IndexOutOfRangeException>().WithMessage("Attempted reading invalid index `-1` of C style naked array of length 5. https://github.com/fin-language/fin/issues/14");

        action = () => counts.unsafe_get(5);
        action.Should().Throw<IndexOutOfRangeException>().WithMessage("Attempted reading invalid index `5` of C style naked array of length 5. https://github.com/fin-language/fin/issues/14");
    }


    [Fact]
    public void ImplicitConversionFromArray()
    {
        c_array<u8> counts = new u8[] { 0, 1, 2, 3, 4 };
        counts.SimGetValues().Should().BeEquivalentTo(new u8[] { 0, 1, 2, 3, 4 });

        var counts2 = new c_array<u8>(new u8[] { 10, 11, 12, 13, 14 });
        counts = counts2;
        counts.SimGetValues().Should().BeEquivalentTo(new u8[] { 10, 11, 12, 13, 14 });
    }
}











