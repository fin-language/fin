using fin.sim.lang;

namespace fin.sim.test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        u8 a = 0;
        i8 b = -1;
        var c = a + b;



        Assert.True(a > b);
    }
}