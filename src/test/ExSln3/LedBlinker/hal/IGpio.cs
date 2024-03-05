namespace hal;

public enum GpioDirection
{
    Input,
    Output
}

public interface IGpio : IDigInOut
{
    bool enable_pullup();
    bool disable_pullup();
    void set_direction(GpioDirection direction);
}
