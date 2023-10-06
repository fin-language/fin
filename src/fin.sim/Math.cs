namespace fin.sim.lang;

public class Math
{
    [simonly]
    public enum Mode {
        Checked,
        Unsafe,
        // Exception,
    };

    /// <summary>
    /// ThreadStatic
    /// </summary>
    /// NOTE: Do not specify initial values for fields marked with ThreadStaticAttribute, because such initialization occurs only once, when the class constructor executes, and therefore affects only one thread. If you do not specify an initial value, you can rely on the field being initialized to its default value if it is a value type, or to null if it is a reference type.
    [System.ThreadStatic]
    [simonly]
    private static Mode mode;

    /// <summary>
    /// ThreadStatic
    /// </summary>
    /// NOTE: Do not specify initial values for fields marked with ThreadStaticAttribute, because such initialization occurs only once, when the class constructor executes, and therefore affects only one thread. If you do not specify an initial value, you can rely on the field being initialized to its default value if it is a value type, or to null if it is a reference type.
    [System.ThreadStatic]
    [simonly]
    private static Err? implicit_err;

    /// <summary>
    /// Should not normally be used by application code.
    /// </summary>
    [simonly]
    public static Mode CurrentMode => mode;

    [simonly]
    internal static void StoreSettingsAndDefault(Scope scope)
    {
        scope.mode = mode;
        scope.implicit_err = implicit_err;
        mode = Mode.Checked;
        implicit_err = null;
    }

    [simonly]
    internal static void RestoreSettings(Scope scope)
    {
        mode = scope.mode;
        implicit_err = scope.implicit_err;
    }

    public static void capture_err(Err err)
    {
        implicit_err = err;
    }

    [simonly]
    public static void normal_mode()
    {
        mode = Mode.Checked;
    }

    [simonly]
    public static void unsafe_mode()
    {
        mode = Mode.Unsafe;
    }
}