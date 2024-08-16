using finlang.Output;
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

    public static CapturingTextWriterFactory TranspileFinToCFilesWithDummyMain(params string[] sourceFilesContents)
    {
        sourceFilesContents[0] = sourceFilesContents[0] + """
            // to satisfy the compiler
            class DummyMain
            {
                static void Main(string[] args){}
            }
            """;
        (_, CapturingTextWriterFactory writer) = Transpile(sourceFilesContents);
        return writer;
    }

    public static CapturingTextWriterFactory TranspileFinToCFiles(params string[] sourceFilesContents)
    {
        (_, CapturingTextWriterFactory writer) = Transpile(sourceFilesContents);
        return writer;
    }

    public static (CTranspiler, CapturingTextWriterFactory) Transpile(params string[] sourceFilesContents)
    {
        CTranspiler transpiler = new(destinationDirPath: ".");
        CapturingTextWriterFactory capturingTextWriterFactory = new();
        transpiler.textWriterFactory = capturingTextWriterFactory;
        transpiler.GatherDeclarationsForProject(AdhocCodeHelper.CreateProjectForCode(sourceFilesContents));
        transpiler.Generate();
        transpiler.SetFilePaths();
        transpiler.WriteFilesWithoutDirectoryWipeCreate();
        return (transpiler, capturingTextWriterFactory);
    }
}