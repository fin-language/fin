﻿#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.


namespace finlang;

public class SimOnly
{
    /// <summary>
    /// This is used to mark methods that should only exist/run in C# simulations.<br/>
    /// The contained code will not exist in generated C code.<br/>
    /// You are free to use any C# code in here (not restricted to fin subset).
    /// </summary>
    /// <param name="action"></param>
    public static void Run(Action action)
    {
        action();
    }

    public static void Throw(Exception e)
    {
        throw e;
    }

    public static void ThrowNotImplemented(string? msg = null)
    {
        throw new NotImplementedException(msg);
    }
}