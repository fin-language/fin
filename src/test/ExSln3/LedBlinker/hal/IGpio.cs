namespace hal;

public interface IGpio : IDigInOut
{
    bool enable_pullup();
    bool enable_pulldown();
}
