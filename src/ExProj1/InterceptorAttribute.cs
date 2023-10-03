using ExProj1;
using MethodDecorator.Fody.Interfaces;
using System.Reflection;

// Attribute should be "registered" by adding as module or assembly custom attribute.
// ??? why is this needed? Seems to work without it.
[module: Interceptor]

namespace ExProj1;

// Any attribute which provides OnEntry/OnExit/OnException with proper args
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Class | AttributeTargets.Assembly | AttributeTargets.Module)]
public class InterceptorAttribute : Attribute, IMethodDecorator
{
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
