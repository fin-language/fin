using FluentAssertions;
using finlang;
using hal;

namespace Tests;

public class LedTests
{
    [Fact]
    public void TurnOn()
    {

    }
}

public class SizeOfExTests
{
    [Fact]
    public void CalcParamSizes()
    {
        new SizeOfEx().calc_param_sizes(1, 2).Should().Be(5);
    }
}