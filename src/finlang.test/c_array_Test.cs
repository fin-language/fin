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
            counts[i] = i;

        counts._cSharpArray.Should().BeEquivalentTo(new u8[] { 0, 1, 2, 3, 4 });
    }

    [Fact]
    public void WriteOutOfBounds()
    {
        c_array<u8> counts = mem.stack(new c_array<u8>(5));

        Action action;
        action = () => counts[5] = 1;
        action.Should().Throw<IndexOutOfRangeException>().WithMessage("Attempted writing value `1` to invalid index `5` of C style naked array of length 5. https://github.com/fin-language/fin/issues/14");

        action = () => counts[-1] = 88;
        action.Should().Throw<IndexOutOfRangeException>().WithMessage("Attempted writing value `88` to invalid index `-1` of C style naked array of length 5. https://github.com/fin-language/fin/issues/14");

        i8 index = -1;
        action = () => counts[index] = 0;
        action.Should().Throw<IndexOutOfRangeException>().WithMessage("Attempted writing value `0` to invalid index `-1` of C style naked array of length 5. https://github.com/fin-language/fin/issues/14");

        index = -128;
        action = () => counts[index] = 0;
        action.Should().Throw<IndexOutOfRangeException>().WithMessage("Attempted writing value `0` to invalid index `-128` of C style naked array of length 5. https://github.com/fin-language/fin/issues/14");
    }
}











