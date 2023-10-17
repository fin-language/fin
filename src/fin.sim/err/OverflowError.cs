namespace fin.sim.err;

public class OverflowError : Error
{
    // track the value that overflowed?
}

public class UnderflowError : Error
{
    // track the value that underflowed?
}

/// <summary>
/// Shift by negative, or by more than the size of the type.
/// </summary>
public class ShiftMisuse : Error
{
    // track the value that overflowed?
}
