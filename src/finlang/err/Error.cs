using System.Diagnostics;
using static finlang.math;

namespace finlang.err;

public class Error
{
    public string? file;
    public string? method_name;
    public int line;

    [simonly]
    public StackTrace? SimStackTrace { get; }

    [simonly]
    public string? SimMessage { get; }


    /// <summary>
    /// ThreadStatic
    /// </summary>
    /// NOTE: Do not specify initial values for fields marked with ThreadStaticAttribute, because such initialization occurs only once, when the class constructor executes, and therefore affects only one thread. If you do not specify an initial value, you can rely on the field being initialized to its default value if it is a value type, or to null if it is a reference type.
    [System.ThreadStatic]
    [simonly]
    private static bool simDoNotCaptureStackTraces;

    public Error()
    {
        if (!simDoNotCaptureStackTraces)
            SimStackTrace = new StackTrace();
    }

    public Error(string? simMessage)
    {
        if (!simDoNotCaptureStackTraces)
            SimStackTrace = new StackTrace();
        
        SimMessage = simMessage;
    }

    [simonly]
    public static void SimDoNotCaptureStackTraces()
    {
        simDoNotCaptureStackTraces = true;
    }

    [simonly]
    public static void SimCaptureStackTraces()
    {
        simDoNotCaptureStackTraces = false;
    }

    public void set_context(string? method_name, string? source_file_path, int source_line_number = 0)
    {
        this.method_name = method_name;
        this.file = source_file_path;
        this.line = source_line_number;
    }

    public virtual string to_string_short()
    {
        return this.GetType().FullName!;
    }

    // TODOLOW - have this use fin string instead.
    public virtual string to_string_full()
    {
        return to_string_short() + $": file: {file}, method: {method_name}, line: {line}";
    }
}
