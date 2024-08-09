using app;
using finlang;
using FluentAssertions;
using NSubstitute;

namespace Tests;

public class MainControllerTest
{
    public class FakeLed : FinObj, ILed
    {
        public bool state;
        public void set_state(bool state) => this.state = state;
    }

    public class FakeTimeProvider : FinObj, ITimeProvider
    {
        public u32 time_ms;
        public u32 get_time() => time_ms;
    }

    [Fact]
    public void Test()
    {
        math.unsafe_mode();

        var time_provider = new FakeTimeProvider();
        var fakeLed1 = new FakeLed();
        var fakeLed2 = new FakeLed();

        var controller = new MainController(time_provider, fakeLed1, fakeLed2);
        fakeLed1.state.Should().BeTrue();
        fakeLed2.state.Should().BeFalse();

        // test just before the toggle
        time_provider.time_ms = 999;
        controller.update();
        fakeLed1.state.Should().BeTrue();
        fakeLed2.state.Should().BeFalse();

        // test at the toggle
        time_provider.time_ms = 1000;
        controller.update();
        fakeLed1.state.Should().BeFalse();
        fakeLed2.state.Should().BeTrue();

        // get to the next toggle
        // and let's use a mock for Led1
        time_provider.time_ms += 1000;
        var led1 = Substitute.For<ILed>();
        controller._led1 = led1;
        controller.update();

        // check that the state was toggled
        led1.Received(1).set_state(true);
    }
}
