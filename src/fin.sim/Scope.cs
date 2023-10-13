﻿using fin.sim.err;
using fin.sim.lang;
using System.Collections.Generic;
using System.Reflection;

namespace fin.sim;

[simonly]
public class Scope
{
    public math.Mode prevMathMode;

    /// <summary>
    /// Todo support
    /// </summary>
    public Err? prevMathUserProvidedErr;

    public List<FinObj> stackAllocatedObjects = new();

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
