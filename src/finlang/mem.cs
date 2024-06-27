using finlang.err;
using System;

namespace finlang;

public class mem
{
    [simonly]
    public enum AccessMode
    {
        /// <summary>
        /// "Not specified" is the default mode. 
        /// We avoid the name "UnSpecified" because the start matches "Unsafe" making auto complete mistakes more likely.
        /// </summary>
        NotSpecified,

        /// <summary>
        /// User provided Err object to track errors.
        /// </summary>
        UserProvidedErr,

        /// <summary>
        /// Using global implicit Err object to track errors.
        /// </summary>
        //ImplicitErr,

        // Someday: Exception
    };

    /// <summary>
    /// ThreadStatic
    /// </summary>
    /// NOTE: Do not specify initial values for fields marked with ThreadStaticAttribute, because such initialization occurs only once, when the class constructor executes, and therefore affects only one thread. If you do not specify an initial value, you can rely on the field being initialized to its default value if it is a value type, or to null if it is a reference type.
    [System.ThreadStatic]
    [simonly]
    private static AccessMode mode;

    /// <summary>
    /// ThreadStatic
    /// </summary>
    /// NOTE: Do not specify initial values for fields marked with ThreadStaticAttribute, because such initialization occurs only once, when the class constructor executes, and therefore affects only one thread. If you do not specify an initial value, you can rely on the field being initialized to its default value if it is a value type, or to null if it is a reference type.
    [System.ThreadStatic]
    [simonly]
    internal static Err? userProvidedErr;

    /// <summary>
    /// Should only be used in constructors or field initializers.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T init<T>(T obj) where T : FinObj
    {
        return obj;
    }

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

    /// <summary>
    /// equivalent to `sizeof(uint8_t)` in C.
    /// </summary>
    public static u8 size_of(u8 _) => 1;
    /// <summary>
    /// equivalent to `sizeof(uint16_t)` in C.
    /// </summary>
    public static u8 size_of(u16 _) => 2;
    /// <summary>
    /// equivalent to `sizeof(uint32_t)` in C.
    /// </summary>
    public static u8 size_of(u32 _) => 4;
    /// <summary>
    /// equivalent to `sizeof(uint64_t)` in C.
    /// </summary>
    public static u8 size_of(u64 _) => 8;

    /// <summary>
    /// equivalent to `sizeof(int8_t)` in C.
    /// </summary>
    public static u8 size_of(i8 _) => 1;
    /// <summary>
    /// equivalent to `sizeof(int16_t)` in C.
    /// </summary>
    public static u8 size_of(i16 _) => 2;
    /// <summary>
    /// equivalent to `sizeof(int32_t)` in C.
    /// </summary>
    public static u8 size_of(i32 _) => 4;
    /// <summary>
    /// equivalent to `sizeof(int64_t)` in C.
    /// </summary>
    public static u8 size_of(i64 _) => 8;
}
