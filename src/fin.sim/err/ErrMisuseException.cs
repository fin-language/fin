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

public class ErrMisuseException : System.Exception
{
    public ErrMisuseException() { }
    public ErrMisuseException(string message) : base(message) { }
}
