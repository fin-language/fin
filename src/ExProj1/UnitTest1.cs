using ExProj1;
using FluentAssertions;

namespace ExProj1;

[Interceptor]
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

    public int @stackalloc()
    {
        Span<int> first = stackalloc int[10];
        return first[0];
    }
}

[Interceptor]
public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        //fin.unsafe_math();
        Sample sample = new();
        sample.Method();
        Span<int> first = stackalloc int[10];
        int x = sample.GetInt();
    }

    [Fact]
    public void Test2()
    {
        InterceptorAttribute.methodBases.Count.Should().Be(1);
    }
}