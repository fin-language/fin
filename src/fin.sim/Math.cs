using System;

namespace fin.sim.lang;

/// <summary>
/// Lower case name because it is meant to be a language concept and not a Class type.
/// </summary>
public class math
{
    [simonly]
    public enum Mode {
        /// <summary>
        /// "Not specified" is the default mode. 
        /// We avoid the name "UnSpecified" because the start matches "Unsafe" making auto complete mistakes more likely.
        /// </summary>
        NotSpecified,
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
    private static Err? implicitErr;

    /// <summary>
    /// Should not normally be used by application code.
    /// </summary>
    [simonly]
    public static Mode CurrentMode => mode;

    [simonly]
    internal static void StoreSettingsAndDefault(Scope scope)
    {
        StoreSettings(scope);
        DefaultSettings();
    }

    [simonly]
    internal static void DefaultSettings()
    {
        mode = Mode.NotSpecified;
        implicitErr = null;
    }

    [simonly]
    internal static void StoreSettings(Scope scope)
    {
        scope.mathMode = mode;
        scope.implicitErrArg = implicitErr;
    }

    /// <summary>
    /// Will oneday have code analyzers to ensure math mode is specified. This works for now.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    [simonly]
    public static void ThrowIfModeNotSpecified()
    {
        if (math.CurrentMode == math.Mode.NotSpecified)
        {
            throw new InvalidOperationException("Math mode must be specified for now (until fin default established).");
        }
    }

    [simonly]
    internal static void RestoreSettings(Scope scope)
    {
        mode = scope.mathMode;
        implicitErr = scope.implicitErrArg;
    }

    public static void capture_err(Err err)
    {
        implicitErr = err;
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