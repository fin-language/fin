using Microsoft.CodeAnalysis;
using System.Text;

namespace finlang.Transpiler;

public class C99Transpiler
{
    public readonly StringBuilder hFileSb = new();
    public readonly StringBuilder cFileSb = new();

    public void Transpile(string slnPath)
    {
        Solution sln = SolutionLoader.Load(slnPath);

        foreach (var project in sln.Projects)
        {
            Console.WriteLine(project.AssemblyName);
        }

    }
}