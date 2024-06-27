using finlang;

// only transpile interfaces that have IFinObj as an ancestor
// https://github.com/fin-language/fin/issues/63
namespace issue63;

public interface ISimOnlyInterface // NOT part of the transpile
{
    string GetVariableName();
}

/// <summary>
/// This one should get picked up by the transpiler
/// </summary>
public interface INumberProvider : IFinObj
{
    u8 GetNumber();
}

public class InterfaceSelectionForTranspile : FinObj, INumberProvider, ISimOnlyInterface
{
    u8 my_number = 42;

    public u8 GetNumber()
    {
        return my_number;
    }

    [simonly]
    public string GetVariableName()
    {
        return nameof(my_number);
    }
}
