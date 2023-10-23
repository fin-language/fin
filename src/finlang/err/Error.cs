namespace finlang.err;

public class Error
{
    public string? file;
    public string? method_name;
    public int line;

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
