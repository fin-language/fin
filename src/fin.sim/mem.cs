using System;

namespace fin.sim.lang;

public class mem
{
    /// <summary>
    /// Tells fin transpiler that object created by this method should be allocated on the stack.
    /// Intended to be used like <code>Bike bike = mem.stack(new Bike());</code>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T stack<T>(T obj)
    {
        // TODO remove when we have fin array types.
        if (obj is FinObj finObj)
            ScopeTracker.Peek().stackAllocatedObjects.Add(finObj);

        return obj;
    }

    /// <summary>
    /// Tells fin transpiler that object created by this method should be allocated on the heap.
    /// Intended to be used like <code>Bike bike = mem.heap(new Bike());</code>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T heap<T>(T obj)
    {
        throw new NotImplementedException();
    }

    public static void free<T>(T obj)
    {
        throw new NotImplementedException();
    }
}
