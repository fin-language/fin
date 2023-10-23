using finlang;
using System;
using System.Collections.Generic;

namespace finlang;

public class ScopeTracker
{
    // NOTE: Do not specify initial values for fields marked with ThreadStaticAttribute, because such initialization occurs only once, when the class constructor executes, and therefore affects only one thread. If you do not specify an initial value, you can rely on the field being initialized to its default value if it is a value type, or to null if it is a reference type.
    [ThreadStatic]
    private static Stack<Scope>? _scopeStack;

    public static Stack<Scope> ScopeStack
    {
        get
        {
            if (_scopeStack == null)
            {
                _scopeStack = new();
            }
            return _scopeStack;
        }
    }

    public static Scope CurrentScope => ScopeStack.Peek();

    public static void Push(Scope scope)
    {
        ScopeStack.Push(scope);
    }

    public static void PopAndDestroyStackObjects()
    {
        var scope = ScopeStack.Pop();

        foreach (var item in scope.stackAllocatedObjects)
        {
            item.SimDestruct();
        }

        math.RestoreSettings(scope);
    }

    public static Scope Peek()
    {
        return ScopeStack.Peek();
    }
}
