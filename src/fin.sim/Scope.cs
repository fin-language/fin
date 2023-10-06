using System.Reflection;
using FinMath = fin.sim.lang.Math;

namespace fin.sim;

public class Scope
{
    public FinMath.Mode mode;
    public Err? implicit_err;

    // array access setting...

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
