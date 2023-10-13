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
    public static T stack<T>(T obj) where T : FinObj
    {
        ScopeTracker.Peek().stackAllocatedObjects.Add(obj);
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
