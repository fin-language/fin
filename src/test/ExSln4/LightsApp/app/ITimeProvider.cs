using finlang;
namespace app;

public interface ITimeProvider : IFinObj
{
    u32 get_time();
}
