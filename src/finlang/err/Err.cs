using System.Runtime.CompilerServices;

namespace finlang.err;

/// <summary>
/// Error Result Recorder (Err).
/// </summary>
public class Err : FinObj
{
    protected Error? _error;

    /// <summary>
    /// User must read any error before cleared to avoid throwing error during simulation.
    /// </summary>
    protected bool _user_read_error = true;

    //public static readonly ErrCode OK = new("OK");
    //public static readonly ErrCode OVERFLOW = new("OVERFLOW");

    public Err()
    {

    }

    internal override void SimDestruct()
    {
        base.SimDestruct();

        if (this._error != null)
        {
            if (!this._user_read_error)
                throw new ErrMisuseException("Err error must be read and cleared before going out of scope (stack object destructed).");

            if (this._error != null)
                throw new ErrMisuseException("Err error must be cleared before going out of scope (stack object destructed).");
        }
    }

    public Error? get_error()
    {
        _user_read_error = true;
        return _error;
    }

    public bool has_error()
    {
        return get_error() != null;
    }

    public bool is_ok()
    {
        return !has_error();
    }

    /// <summary>
    /// Clears error. Throws if error has not been read yet, or if no error set.
    /// See also <see cref="disregard_any_error"/>.
    /// </summary>
    /// <exception cref="ErrMisuseException"></exception>
    public void clear()
    {
        if (_error == null)
            throw new ErrMisuseException($"Err error not set. If you really don't care use `{nameof(disregard_any_error)}()`.");

        if (!_user_read_error)
            throw new ErrMisuseException($"Err error not read before cleared. If you really don't care use `{nameof(disregard_any_error)}()`.");

        _error = null;
    }

    /// <summary>
    /// Gives no flips about whether an error exists or has been read first.
    /// </summary>
    public void disregard_any_error()
    {
        _error = null;
    }

    /// <summary>
    /// Replaces any existing error with new error.
    /// </summary>
    /// <param name="error"></param>
    /// <param name="method_name">Automatically provided by C# compiler. Don't set.</param>
    /// <param name="source_file_path">Automatically provided by C# compiler. Don't set.</param>
    /// <param name="source_line_number">Automatically provided by C# compiler. Don't set.</param>
    public void force_set(Error error, 
        [CallerMemberName] string? method_name = null,
        [CallerFilePath] string? source_file_path = null,
        [CallerLineNumber] int source_line_number = 0)
    {
        this._user_read_error = false;
        this._error = error;
        this._error.set_context(method_name, source_file_path, source_line_number);
    }

    /// <summary>
    /// If no error has been set, then set the error. See also <see cref="force_set"/>.
    /// </summary>
    /// <param name="error"></param>
    /// <param name="method_name">Automatically provided by C# compiler. Don't set.</param>
    /// <param name="source_file_path">Automatically provided by C# compiler. Don't set.</param>
    /// <param name="source_line_number">Automatically provided by C# compiler. Don't set.</param>
    public void add(Error error,
        [CallerMemberName] string? method_name = null,
        [CallerFilePath] string? source_file_path = null,
        [CallerLineNumber] int source_line_number = 0)
    {
        if (this._error == null)
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
        if (this._error == null)
        {
            force_set(error, null, null, 0);
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
               [CallerMemberName] string? method_name = null,
               [CallerFilePath] string? source_file_path = null,
               [CallerLineNumber] int source_line_number = 0)
    {
        if (this._error != null)
        {
            this._error.method_name ??= method_name;
            this._error.file ??= source_file_path;
            
            if (this._error.line == 0)
            {
                this._error.line = source_line_number;
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
        if (this._error != null)
        {
            this._error.method_name = method_name;
            this._error.file = source_file_path;
            this._error.line = source_line_number;
        }
    }
}