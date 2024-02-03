using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace finlang.Transpiler;

public class SolutionLoader
{
    public static Solution Load(string slnPath)
    {
        if (!MSBuildLocator.IsRegistered)
            MSBuildLocator.RegisterDefaults();

        var workspace = MSBuildWorkspace.Create();
        //workspace.LoadMetadataForReferencedProjects = true;
        return workspace.OpenSolutionAsync(slnPath).Result;
    }
}
