using finlang.test.Output;
using finlang.Transpiler;
namespace finlang.test.TranspilerTest;

public class TranspilerTestHelper
{
    public static string TranspileFinToCFile(string code, string fileName)
    {
        CapturingTextWriterFactory capturingTextWriterFactory = TranspileFinToCFiles(code);
        var cCode = capturingTextWriterFactory.GetSingleWriterTextByFileName(fileName);
        return cCode;
    }

    public static CapturingTextWriterFactory TranspileFinToCFiles(string code)
    {
        CTranspiler transpiler = new(destinationDirPath: ".");
        CapturingTextWriterFactory capturingTextWriterFactory = new();
        transpiler.textWriterFactory = capturingTextWriterFactory;
        transpiler.GatherDeclarationsForProject(AdhocCodeHelper.CreateProjectForCode(code));
        transpiler.Generate();
        transpiler.SetFilePaths();
        transpiler.WriteFilesWithoutDirectoryWipeCreate();
        return capturingTextWriterFactory;
    }
}