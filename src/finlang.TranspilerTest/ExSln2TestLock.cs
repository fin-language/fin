namespace finlang.TranspilerTest;

public class XunitSolutionLock
{

}

/// <summary>
/// This helps prevent multiple tests all trying to access ExSln2.sln at the same time which will 
/// cause failures.
/// </summary>
[CollectionDefinition(nameof(ExSln2TestLock))]
public class ExSln2TestLock : ICollectionFixture<XunitSolutionLock>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
    // https://xunit.net/docs/shared-context.html
}