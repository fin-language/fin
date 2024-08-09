using app;
using finlang;
using FluentAssertions;
using NSubstitute;

namespace Tests;

public class StopWatchTest
{
    [Fact]
    public void Test()
    {
        math.unsafe_mode();
        var time_provider = Substitute.For<ITimeProvider>();
        var stop_watch = new StopWatch(time_provider);

        u32 time_ms = 0;
        time_provider.get_time().Returns(_ => time_ms);
        stop_watch.reset();
        stop_watch._started_at_ms.Should().Be(0);
        stop_watch.get_elapsed_ms().Should().Be(0);

        time_ms = 1000;
        stop_watch.get_elapsed_ms().Should().Be(1000);

        time_ms = 2000;
        stop_watch.get_elapsed_ms().Should().Be(2000);

        // test overflow
        time_ms = 0xFFFFFFFF;
        stop_watch.reset();
        time_ms = 1;
        stop_watch.get_elapsed_ms().Should().Be(2);
    }
}
