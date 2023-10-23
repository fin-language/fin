#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.


using System;

namespace finlang;

public class simonly
{
    /// <summary>
    /// This is used to mark methods that should only exist/run in C# simulations.<br/>
    /// The contained code will not exist in generated C code.<br/>
    /// You are free to use any C# code in here (not restricted to fin subset).
    /// </summary>
    /// <param name="action"></param>
    [simonly]
    public static void run(Action action)
    {
        action();
    }
}