namespace finlang.Transpiler;

public interface IMangledNameProvider
{
    /// <summary>
    /// Returns the mangled C99 type name for the given Fin type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    string FromFinType<T>() where T : class;

    /// <summary>
    /// Returns the mangled C99 type name for the given Fin type.
    /// FQN stands for fully qualified name. It is the name of the type in C# that includes the namespace.
    /// </summary>
    /// <param name="finTypeFqn"></param>
    /// <returns></returns>
    string FromFinType(string finTypeFqn);
}