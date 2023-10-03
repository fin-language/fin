namespace fin.sim.lang;

internal class MathScope
{

}

public class Math
{
    internal enum Mode {
        Checked,
        Unsafe,
        // Exception,
    };

    private static ThreadLocal<Mode> mode = new();

    internal Mode CurrentMode => mode.Value;

    [simonly]
    public void normal_mode()
    {
        mode.Value = Mode.Checked;
    }

    [simonly]
    public void unsafe_mode()
    {
        mode.Value = Mode.Unsafe;
    }
}