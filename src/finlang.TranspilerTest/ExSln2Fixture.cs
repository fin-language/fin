using Xunit.Sdk;

namespace finlang.TranspilerTest;

public class XunitSolutionLock
{

}

/// <summary>
/// This helps prevent multiple tests all trying to access ExSln2.sln at the same time which will 
/// cause failures.
/// </summary>
[CollectionDefinition(nameof(ExSln2Fixture))]
public class ExSln2Fixture : ICollectionFixture<XunitSolutionLock>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
    // https://xunit.net/docs/shared-context.html

    public static string GetSlnPath()
    {
        return PathHelpers.GetThisDir() + "/../test/ExSln2/ExSln2.sln";
    }

    public static string GetSlnDir()
    {
        return Path.GetDirectoryName(GetSlnPath()) ?? throw new NullException("");
    }
}