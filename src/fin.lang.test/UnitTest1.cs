using fin.lang;

namespace fin.lang.test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        math.unsafe_mode();
        u8 a = 0;
        i8 b = -1;
        var c = a + b;

        a = u8.MAX;
        
        //a = a + 1;
        //a++;

        i8? i8m = b;
        var x = i8m++;
        var bb = i8m;
    }

    class Car
    {
        public int fuel;
    }

    void Test2(Car? a, Car b, Car c)
    {
        Car[] cars = new Car[5];

        int i = 0;
        int? fuel = cars[i - 1]?.fuel + cars[i - 2]?.fuel;

        c.fuel = (a?.fuel + b?.fuel) ?? 0;

        if (c != null)
            c.fuel = (a?.fuel + b?.fuel) ?? 0;

        int? x;
        x = a?.fuel + b?.fuel;
    }
}











public class Chair
{
    public void Test()
    {
        checked
        {
            byte a = 255;
            byte b = 1;
            byte c2 = ((byte)(a + b));
        }

        object o = 1;

        var c = o as Chair; // reserve "as" for returns null on failure

    }
}











