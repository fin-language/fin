using finlang;
using hal;
using System.Collections.Generic;

namespace mcu.avr8;

/// <summary>
/// We want a .c/h file to be generated for this class and have it setup the vtable.
/// </summary>
public class Avr8Gpio : FinObj, IGpio
{
    /// <summary>
    /// Example: PORTB. 
    /// Note: DDRx = PORTx - 1. PINx = PORTx - 2. See Section 30 of data for Register Summary.
    /// </summary>
    [override_type("volatile uint8_t *")]
    public string _port;

    public u8 _pin_mask;

    [System.ThreadStatic]
    [simonly] public static HashSet<string> _instantiations = new HashSet<string>();
    [simonly] public GpioDirection sim_direction = GpioDirection.Input;
    [simonly] public bool sim_pullup = false;
    [simonly] public bool sim_state = false;

    public Avr8Gpio([override_type("volatile uint8_t *")] string port, u8 pin)
    {
        math.unsafe_mode();
        _port = port;
        _pin_mask = ((u8)1 << pin);

        SimOnly.Run(() => { ThrowIfAlreadyInstantiated(port, pin); });
    }

    [simonly]
    private static void ThrowIfAlreadyInstantiated(string port, u8 pin)
    {
        // this can happen because of the ThreadStatic attribute
        if (_instantiations == null)
            _instantiations = new HashSet<string>();

        string key = port + pin;
        if (_instantiations.Contains(key))
            throw new System.InvalidOperationException($"GPIO {key} already instantiated.");
        _instantiations.Add(key);
    }

    [ffi]
    public void set_direction(GpioDirection direction)
    {
        sim_direction = direction;
    }

    [ffi]
    public bool enable_pullup()
    {
        sim_pullup = true;
        return true;
    }

    [ffi]
    public bool disable_pullup()
    {
        sim_pullup = false;
        return true;
    }

    public bool get_output_state()
    {
        return read_input();
    }

    [ffi]
    public bool read_input()
    {
        ThrowIfSetToOutput();
        return sim_state;
    }

    [ffi]
    public void set_output_state(bool state)
    {
        ThrowIfSetToInput();
        sim_state = state;
    }

    [simonly]
    private void ThrowIfSetToInput()
    {
        if (sim_direction == GpioDirection.Input)
            throw new System.InvalidOperationException("Invalid method call when GPIO direction is input.");
    }

    [simonly]
    private void ThrowIfSetToOutput()
    {
        if (sim_direction == GpioDirection.Output)
            throw new System.InvalidOperationException("Invalid method call when GPIO direction is output.");
    }

    public void toggle()
    {
        ThrowIfSetToInput();

        if (read_input())
        {
            set_output_state(false);
        }
        else
        {
            set_output_state(true);
        }
    }
}
