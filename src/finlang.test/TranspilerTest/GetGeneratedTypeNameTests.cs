using finlang.Transpiler;

namespace finlang.test.TranspilerTest;

public class GetGeneratedTypeNameTests
{
    [Fact]
    public void InterfaceMethodDeclaration()
    {
        const string main = """
            // to satisfy the compiler
            class DummyMain
            {
                static void Main(string[] args){}
            }
            """;

        const string fileNoNamespace = """
            using finlang;

            public interface IVehicle : IFinObj
            {
                u8 get_id();
            }
            
            public enum TireType
            {
                Winter,
                Summer
            }

            public class Bike: FinObj
            {
                u8 id;
            }
            """;

        const string fileNamespace = """
            using finlang;

            namespace rocket;

            public interface IBooster : IFinObj
            {
                u8 get_id();
            }
            
            public enum FuelType
            {
                Liquid,
                Solid
            }

            public class Rocket: FinObj
            {
                u8 id;
            }
            """;

        const string fileComplex = """
            using finlang;

            namespace food.oven {
                public interface IOven : IFinObj
                {
                    u8 get_id();
                }
            
                public class Grill: FinObj
                {
                    u8 id;

                    public class GrillDrawer: FinObj
                    {
                        u8 id;
                    }
                }
            }
            """;

        (CTranspiler transpiler, _) = TranspilerTestHelper.Transpile([main, fileNoNamespace, fileNamespace, fileComplex]);

        transpiler.GetCTypeNameFromFinType("IVehicle").Should().Be("IVehicle");
        transpiler.GetCTypeNameFromFinType("Bike").Should().Be("Bike");
        transpiler.GetCTypeNameFromFinType("TireType").Should().Be("TireType");
        //
        transpiler.GetCTypeNameFromFinType("rocket.IBooster").Should().Be("rocket_IBooster");
        transpiler.GetCTypeNameFromFinType("rocket.Rocket").Should().Be("rocket_Rocket");
        transpiler.GetCTypeNameFromFinType("rocket.FuelType").Should().Be("rocket_FuelType");
        //
        transpiler.GetCTypeNameFromFinType("food.oven.IOven").Should().Be("food_oven_IOven");
        transpiler.GetCTypeNameFromFinType("food.oven.Grill").Should().Be("food_oven_Grill");
        transpiler.GetCTypeNameFromFinType("food.oven.Grill.GrillDrawer").Should().Be("food_oven_Grill_GrillDrawer");
    }
}
