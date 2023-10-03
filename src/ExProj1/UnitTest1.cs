using ExProj1;
using fin.sim;
using FluentAssertions;

namespace ExProj1;

public class Sample
{
    public void Method()
    {
        int x = GetInt();
    }

    public int GetInt()
    {
        return 2234;
    }
}

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        //fin.unsafe_math();
        Sample sample = new();
        sample.Method();
    }

    [Fact]
    public void Test2()
    {
        MethodInterceptorAttribute.methodBases.Count.Should().Be(1);
    }
}