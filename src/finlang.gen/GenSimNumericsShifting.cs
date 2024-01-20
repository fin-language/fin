namespace finlang.gen;

public class GenSimNumericsShifting
{

    public static string AddWrapShiftCode(TypeInfo actualType, TypeInfo shiftAmountType, bool is_left)
    {
        string op = is_left ? "<<" : ">>";
        string title = is_left ? "Left" : "Right";
        string func_suffix = is_left ? "lshift" : "rshift";
        string shiftAmountTypeStr = shiftAmountType.fin_name;
        string shiftValueGetter = $"shift_amount._csReadValue";

        if (shiftAmountType.is_signed)
        {
            shiftAmountTypeStr = GenSimNumerics.GenIHasTypeName(shiftAmountType);
            shiftValueGetter = "shift_amount.value._csReadValue";
        }

        string template = $$"""

            /// <summary>
            /// {{title}} shifts the bits discarding overflow bits without error.<br/>
            /// Shift does not change the value of this object (without an assignment).<br/>
            /// Transpiles to C99 code something like {{DocHelper.Code($"({actualType.GetC99BackingTypeName()})(my_num {op} shift_amount)")}}<br/>
            /// Sim exception or Error if shift by negative amount or amount larger than type (undefined C99 behaviors).
            /// Math mode is required to be specified to handle bad shift amounts.
            /// </summary>
            public {{actualType}} wrap_{{func_suffix}}({{shiftAmountTypeStr}} shift_amount)
            {
                ThrowIfMathModeNotSpecified();
                var shift_amount_value = {{shiftValueGetter}};

                {{BuildErrorChecks(actualType, valueGetter: "this._csReadValue")}}

                {{actualType}} result = unchecked(({{actualType.GetBackingTypeName()}})(this._csReadValue {{op}} (byte)shift_amount_value));
                return result;
            }

            """;

        return template;
    }


    public static string AddShiftRightOperator(TypeInfo actualType, TypeInfo shiftAmountType)
    {
        string shiftAmountTypeStr = shiftAmountType.fin_name;
        string shiftValueGetter = $"shift_amount._csReadValue";

        if (shiftAmountType.is_signed)
        {
            shiftAmountTypeStr = GenSimNumerics.GenIHasTypeName(shiftAmountType);
            shiftValueGetter = "shift_amount.value._csReadValue";
        }

        string template = $$"""

            /// <summary>
            /// Right shifts the bits (no overflow possible).<br/>
            /// Shift does not change the value of this object (without an assignment).<br/>
            /// Transpiles to C99 code something like {{DocHelper.Code($"({actualType.GetC99BackingTypeName()})(my_num >> shift_amount)")}}<br/>
            /// Sim exception or if shift by negative amount or amount larger than type (undefined C99 behaviors).
            /// Math mode is required to be specified to handle bad shift amounts.
            /// </summary>
            public static {{actualType}} operator >>({{actualType}} a, {{shiftAmountTypeStr}} shift_amount)
            {
                ThrowIfMathModeNotSpecified();
                var shift_amount_value = {{shiftValueGetter}};

                {{BuildErrorChecks(actualType, valueGetter: "a")}}

                {{actualType}} result = unchecked(({{actualType.GetBackingTypeName()}})(a._csReadValue >> (byte)shift_amount_value));
                return result;
            }

            """;

        return template;
    }

    public static string AddShiftLeftOperator(TypeInfo actualType, TypeInfo shiftAmountType)
    {
        string shiftAmountTypeStr = shiftAmountType.fin_name;
        string shiftValueGetter = $"shift_amount._csReadValue";

        if (shiftAmountType.is_signed)
        {
            shiftAmountTypeStr = GenSimNumerics.GenIHasTypeName(shiftAmountType);
            shiftValueGetter = "shift_amount.value._csReadValue";
        }

        string template = $$"""

            /// <summary>
            /// Left shifts the bits (error on overflow).<br/>
            /// Shift does not change the value of this object (without an assignment).<br/>
            /// Transpiles to C99 code something like {{DocHelper.Code($"({actualType.GetC99BackingTypeName()})(my_num << shift_amount)")}}<br/>
            /// Sim exception or Error if overflow or if shift by negative amount or amount larger than type (undefined C99 behaviors).
            /// Math mode is required to be specified to handle bad shift amounts.
            /// </summary>
            public static {{actualType}} operator <<({{actualType}} a, {{shiftAmountTypeStr}} shift_amount)
            {
                ThrowIfMathModeNotSpecified();
                var shift_amount_value = {{shiftValueGetter}};

                {{BuildErrorChecks(actualType, valueGetter: "a")}}

                var result = a._csValue << (byte)shift_amount_value; // shift_amount_value must fit in byte because of above checks

                if (result < a || result > {{actualType}}.MAX)
                {
                    switch (math.CurrentMode)
                    {
                        case math.Mode.Unsafe:
                            throw new OverflowException($"Left shifting a {{actualType}} integer (value `{a._csReadValue}`) by `{shift_amount_value}` caused the value to overflow (promote first if needed).");
                        case math.Mode.UserProvidedErr:
                            math.userProvidedErr!.add_without_context(new err.OverflowError());
                            return 0;
                        default:
                            throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
                    }
                }

                return ({{actualType}})result;
            }

            """;

        return template;
    }


    public static string BuildErrorChecks(TypeInfo actualType, string valueGetter)
    {
        string template = $$"""
                if (shift_amount_value < 0)
                {
                    switch (math.CurrentMode)
                    {
                        case math.Mode.Unsafe:
                            throw new OverflowException($"Shift misuse! Shifting a value `{{{valueGetter}}}` by a negative amount `{shift_amount}` is undefined behavior in C.");
                        case math.Mode.UserProvidedErr:
                            math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                            break;
                        default:
                            throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
                    }
                }

                if (shift_amount_value >= {{actualType.width}})
                {
                    switch (math.CurrentMode)
                    {
                        case math.Mode.Unsafe:
                            throw new OverflowException($"Overshift! Shifting a {{actualType}} integer (value `{{{valueGetter}}}`) by `{shift_amount_value}` is undefined behavior in C (can't shift more than a type's bit width).");
                        case math.Mode.UserProvidedErr:
                            math.userProvidedErr!.add_without_context(new err.ShiftMisuse());
                            break;
                        default:
                            throw new NotSupportedException($"Unsupported math mode `{math.CurrentMode}`.");
                    }
                }
            """;

        return template;
    }
}
