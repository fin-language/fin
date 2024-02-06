using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace finlang.Transpiler;

public class WorkspaceLoader
{
    public static Solution LoadSolution(string slnPath)
    {
        if (!MSBuildLocator.IsRegistered)
            MSBuildLocator.RegisterDefaults();

        var workspace = MSBuildWorkspace.Create();
        return workspace.OpenSolutionAsync(slnPath).Result;
    }

    public static Project LoadProject(string csprojPath)
    {
        if (!MSBuildLocator.IsRegistered)
            MSBuildLocator.RegisterDefaults();

        var workspace = MSBuildWorkspace.Create();
        return workspace.OpenProjectAsync(csprojPath).Result;
    }
}
