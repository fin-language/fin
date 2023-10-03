using MethodDecorator.Fody.Interfaces;
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

    public static Stack<MethodBase> methodBases = new();

    // instance, method and args can be captured here and stored in attribute instance fields
    // for future usage in OnEntry/OnExit/OnException
    public void Init(object instance, MethodBase method, object[] args)
    {
        methodBases.Push(method);
        //str = string.Format("Init: {0} [{1}]", method.DeclaringType.FullName + "." + method.Name, args.Length);
    }

    public void OnEntry()
    {
        int x = 0;
    }

    public void OnExit()
    {
        int x = 0;
        methodBases.Pop();
    }

    public void OnException(Exception exception)
    {
        //var str = string.Format("OnException: {0}: {1}", exception.GetType(), exception.Message);
        methodBases.Pop();
    }
}
