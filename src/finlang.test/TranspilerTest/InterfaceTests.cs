using finlang.test.Output;
using finlang.Transpiler;

namespace finlang.test.TranspilerTest;

/// <summary>
/// 
/// </summary>
public class InterfaceTests : IClassFixture<InterfaceTests.CompilationFixture>
{
    CompilationFixture compilationFixture;

    public InterfaceTests(CompilationFixture compilationFixture)
    {
        this.compilationFixture = compilationFixture;
    }

    [Fact]
    public void InterfaceMethodInvocation()
    {
        string cCode = compilationFixture.GetFileCode("MyClass.c");

        //cCode.Should().MatchRegex(@"(?mx) \s+ IBike_pedal\(bike\); \s+");
        cCode.Should().Contain(" IBike_pedal(bike);");
        cCode.Should().Contain(" IBike_get_id(bike);"); // note that this is an inherited interface method. get_id() is defined in IVehicle.
        cCode.Should().Contain(" IVehicle_get_id(vehicle);");
    }

    [Fact]
    public void InterfaceMethodDeclaration()
    {
        string cCode = compilationFixture.GetFileCode("IBike.h");
        string strippedCode = StringUtils.RemoveAllHorizontalSpaceChars(cCode); // to not be sensitive to future whitespace changes

        // match: uint8_t (*get_id)(void * self);
        strippedCode.Should().Contain("\n" + "uint8_t(*get_id)(void*self);");
        // match: void (*pedal)(void * self);
        strippedCode.Should().Contain("\n" + "void(*pedal)(void*self);");

        // regex version of matching is a lot more work to maintain
        cCode.Should().MatchRegex(@"(?xm)
            ^          \s*
            uint8_t    \s* 
            [(]        \s* 
                [*]    \s*
                get_id \s*
            [)]        \s*
            [(]        \s*
                void   \s*
                [*]    \s*
                self   \s*
            [)]        \s*
            ;
        ");
    }


    //-----------------------------------------------------------------------------------

    public class CompilationFixture : IDisposable
    {
        CapturingTextWriterFactory files;

        public string GetFileCode(string fileName)
        {
            return files.GetSingleWriterTextByFileName(fileName);
        }

        public CompilationFixture()
        {
            const string code = """
                using finlang;

                public interface IVehicle : IFinObj
                {
                    u8 get_id();
                }
            
                public interface IBike : IVehicle
                {
                    void pedal();
                }

                public class MyClass: FinObj
                {
                    public static void use_bike(IBike bike)
                    {
                        bike.pedal();
                        bike.get_id(); // inherited from IVehicle
                    }

                    public static void use_vehicle(IVehicle vehicle)
                    {
                        vehicle.get_id();
                    }
                }

                // to satisfy the compiler
                class DummyMain
                {
                    static void Main(string[] args){}
                }
                """;
            this.files = TranspilerTestHelper.TranspileFinToCFiles(code);
        }

        public void Dispose()
        {
        }
    }
}
