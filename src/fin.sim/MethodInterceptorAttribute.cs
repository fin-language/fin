using MethodDecorator.Fody.Interfaces;
using System;
using System.Reflection;

namespace fin.sim;

/// <summary>
/// Required for accurate simulations. This attribute is used by Fody.MethodDecorator to intercept all methods.
/// This allows us to track the call stack, take into account scope settings like `Math.unsafe_allowed()`.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Class | AttributeTargets.Assembly | AttributeTargets.Module)]
public class MethodInterceptorAttribute : Attribute, IAspectMatchingRule, IMethodDecorator
{
    public string AttributeTargetTypes { get; set; } = "";
    public bool AttributeExclude { get; set; }
    public int AttributePriority { get; set; }
    public int AspectPriority { get; set; }

    public MethodInterceptorAttribute() {}

    // instance, method and args can be captured here and stored in attribute instance fields
    // for future usage in OnEntry/OnExit/OnException
    public void Init(object? instance, MethodBase method, object[] args)
    {
        ScopeTracker.Push(new Scope(instance, method, args));
    }

    public void OnEntry()
    {
        // not sure how this differs from Init
    }

    public void OnExit()
    {
        ScopeTracker.Pop();
    }

    public void OnException(Exception exception)
    {
        ScopeTracker.Pop();
    }
}
