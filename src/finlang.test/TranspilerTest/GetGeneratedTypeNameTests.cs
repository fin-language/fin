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

        var mangledNameProvider = transpiler.GetMangledNameProvider();

        mangledNameProvider.FromFinType("IVehicle").Should().Be("IVehicle");
        mangledNameProvider.FromFinType("Bike").Should().Be("Bike");
        mangledNameProvider.FromFinType("TireType").Should().Be("TireType");
        //
        mangledNameProvider.FromFinType("rocket.IBooster").Should().Be("rocket_IBooster");
        mangledNameProvider.FromFinType("rocket.Rocket").Should().Be("rocket_Rocket");
        mangledNameProvider.FromFinType("rocket.FuelType").Should().Be("rocket_FuelType");
        //
        mangledNameProvider.FromFinType("food.oven.IOven").Should().Be("food_oven_IOven");
        mangledNameProvider.FromFinType("food.oven.Grill").Should().Be("food_oven_Grill");
        mangledNameProvider.FromFinType("food.oven.Grill.GrillDrawer").Should().Be("food_oven_Grill_GrillDrawer");
    }
}
