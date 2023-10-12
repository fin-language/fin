using fin.sim.lang;
using System.Runtime.CompilerServices;

namespace fin.sim.err;

///// <summary>
///// Why not just use a u32 or enum?
///// Because we need to allow for user code and libraries to add new error codes.
///// If a library adds a new error code, then the user code will not be able to
///// </summary>
//public class ErrCode
//{
//    //public readonly u32 code;
//    public readonly string name;

//    // require unique name? Require namespace/prefix?
//    public ErrCode(string name)
//    {
//        //this.code = code;
//        this.name = name;
//    }
//}

/// <summary>
/// Error Result Recorder (Err).
/// </summary>
public class Err
{
    protected Error? error;

    //public static readonly ErrCode OK = new("OK");
    //public static readonly ErrCode OVERFLOW = new("OVERFLOW");

    public Error? get_error()
    {
        return error;
    }

    public bool has_error()
    {
        return error != null;
    }

    public bool is_ok()
    {
        return !has_error();
    }

    public void clear()
    {
        error = null;
    }

    /// <summary>
    /// Replaces any existing error with new error.
    /// </summary>
    /// <param name="error"></param>
    /// <param name="method_name">Automatically provided by C# compiler. Don't set.</param>
    /// <param name="source_file_path">Automatically provided by C# compiler. Don't set.</param>
    /// <param name="source_line_number">Automatically provided by C# compiler. Don't set.</param>
    public void force_set(Error error, 
        [CallerMemberName] string method_name = "",
        [CallerFilePath] string source_file_path = "",
        [CallerLineNumber] int source_line_number = 0)
    {
        this.error = error;
        this.error.set_context(method_name, source_file_path, source_line_number);
    }

    /// <summary>
    /// If no error has been set, then set the error. See also <see cref="force_set"/>.
    /// </summary>
    /// <param name="error"></param>
    /// <param name="method_name">Automatically provided by C# compiler. Don't set.</param>
    /// <param name="source_file_path">Automatically provided by C# compiler. Don't set.</param>
    /// <param name="source_line_number">Automatically provided by C# compiler. Don't set.</param>
    public void add(Error error,
        [CallerMemberName] string method_name = "",
        [CallerFilePath] string source_file_path = "",
        [CallerLineNumber] int source_line_number = 0)
    {
        if (this.error == null)
        {
            force_set(error, method_name, source_file_path, source_line_number);
        }
    }

    /// <summary>
    /// If no error has been set, then set the error. See also <see cref="force_set"/>.
    /// Doesn't provide error location context.
    /// </summary>
    public void add_without_context(Error error)
    {
        if (this.error == null)
        {
            this.error = error;
        }
    }

    /// <summary>
    /// If an error has been set, this will add context to the error if it doesn't already have context.
    /// Useful for adding context to math errors.
    /// </summary>
    /// <param name="error"></param>
    /// <param name="method_name">Automatically provided by C# compiler. Don't set.</param>
    /// <param name="source_file_path">Automatically provided by C# compiler. Don't set.</param>
    /// <param name="source_line_number">Automatically provided by C# compiler. Don't set.</param>
    public void provide_context(
               [CallerMemberName] string method_name = "",
               [CallerFilePath] string source_file_path = "",
               [CallerLineNumber] int source_line_number = 0)
    {
        if (this.error != null)
        {
            this.error.method_name ??= method_name;
            this.error.file ??= source_file_path;
            
            if (this.error.line == 0)
            {
                this.error.line = source_line_number;
            }
        }
    }

    /// <summary>
    /// If an error has been set, this will override context to the error.
    /// </summary>
    /// <param name="error"></param>
    /// <param name="method_name">Automatically provided by C# compiler. Don't set.</param>
    /// <param name="source_file_path">Automatically provided by C# compiler. Don't set.</param>
    /// <param name="source_line_number">Automatically provided by C# compiler. Don't set.</param>
    public void override_context(
               [CallerMemberName] string method_name = "",
               [CallerFilePath] string source_file_path = "",
               [CallerLineNumber] int source_line_number = 0)
    {
        if (this.error != null)
        {
            this.error.method_name = method_name;
            this.error.file = source_file_path;
            this.error.line = source_line_number;
        }
    }
}