using finlang;
using finlang.Transpiler;
namespace finlang.test.TranspilerTest;

public class InterfaceMethodInvocation
{
    [Fact]
    public void Test()
    {
        const string code = """
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
            """;

        CTranspiler transpiler = new CTranspiler(code);

    }
}
