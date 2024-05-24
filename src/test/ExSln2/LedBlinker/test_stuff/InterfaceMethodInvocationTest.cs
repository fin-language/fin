using finlang;

namespace hal;

public class InterfaceMethodInvocationTest: FinObj
{
    public interface IVehicle : IFinObj
    {
        u8 get_id();
    }

    public interface IBike : IVehicle
    {
        void pedal();
    }

    public static void test(IBike bike)
    {
        bike.pedal();
        bike.get_id();
    }
}
