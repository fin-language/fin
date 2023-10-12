using fin.sim.err;
using fin.sim.lang;
using System.Reflection;

namespace fin.sim;

[simonly]
public class Scope
{
    public math.Mode mathMode;

    /// <summary>
    /// Todo support
    /// </summary>
    public Err? implicitErrArg;

    // TODO array access setting...

    public object? instance;
    MethodBase method;
    object[] args;

    public Scope(object? instance, MethodBase method, object[] args)
    {
        this.instance = instance;
        this.method = method;
        this.args = args;
    }
}
