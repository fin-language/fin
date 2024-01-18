using finlang.err;
using System;
using System.Collections;
using System.Collections.Generic;

namespace finlang;

/// <summary>
/// NOT finished yet.
/// An array that uses a u8 for its size. Generated C code uses a flexible array member (FAM) to be efficient.
/// </summary>
/// <typeparam name="T"></typeparam>
public class arr8<T> : FinObj, IEnumerable<T>
{
    public u8 size => _size;

    /// <summary>
    /// Only accessible for test/simulation C# code. Not accessible to fin application code. Doesn't exist in generated C code.
    /// </summary>
    [simonly]
    public u8 _size;

    /// <summary>
    /// Only accessible for test/simulation C# code. Not accessible to fin application code. Doesn't exist in generated C code.
    /// </summary>
    [simonly]
    public T[] _cSharpArray;

    public arr8(u8 size)
    {
        if (size == 0)
            throw new ArgumentException("Array size must be greater than 0.");

        _size = size;
        _cSharpArray = new T[size];
    }

    /// <summary>
    /// Constructs an array from a C# array. The underlying C# array is cloned.
    /// </summary>
    /// <param name="values"></param>
    public arr8(T[] values) : this((u8)values.Length)
    {
        values.CopyTo(_cSharpArray, 0);
    }

    /// <summary>
    /// Not supported in code gen until tuples are supported.
    /// </summary>
    /// <returns></returns>
    public (c_array<T>, u8 size) to_c_array()
    {
        c_array<T> c_arr = c_array<T>.AliasCSharpArray(_cSharpArray, 0);
        return (c_arr, _size);
    }

    /// <summary>
    /// You MUST check err before using result. Will eventually be enforced by compiler.<br/>
    /// If there is an existing or new error, returns the first element of the array.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="err"></param>
    /// <returns></returns>
    [requires_err_check]
    public T get(int index, Err err)
    {
        // TODO discuss if we should return anything if there is an error
        if (err.has_error())
            return _cSharpArray[0]; // fixme discuss. some global shared data instead? insist on check before use?

        if (index < 0 || index >= _cSharpArray.Length)
        {
            err.add(new OutOfBoundsError($"Attempted reading invalid index `{index}` of array of length {_cSharpArray.Length}."));
            return _cSharpArray[0]; // fixme discuss. some global shared data instead? insist on check before use?
        }
        return _cSharpArray[index];
    }

    public T try_get(int index, T default_value, Err err)
    {
        // TODO discuss if we should return anything if there is an error
        if (err.has_error())
            return default_value; // fixme discuss. some global shared data instead?

        if (index < 0 || index >= _cSharpArray.Length)
        {
            err.add(new OutOfBoundsError($"Attempted reading invalid index `{index}` of array of length {_cSharpArray.Length}."));
            return default_value;
        }
        return _cSharpArray[index];
    }

    // See https://github.com/fin-language/fin/issues/17
    //// this doesn't work with value types. We want them to return a Nullable<T>, but they return 0 instead.
    //public T? get(int index)
    //{
    //    if (index < 0 || index >= _cSharpArray.Length)
    //    {
    //        return default;
    //    }

    //    return _cSharpArray[index];
    //}

    /// <summary>
    /// Will not write to array if there is an existing error.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="value"></param>
    /// <param name="err"></param>
    public void set(int index, T value, Err err)
    {
        // TODO discuss if we should avoid write if there is an error
        if (err.has_error())
            return;

        if (index < 0 || index >= _cSharpArray.Length)
        {
            err.add(new OutOfBoundsError($"Attempted writing value `{value}` to invalid index `{index}` of array of length {_cSharpArray.Length}."));
        }

        _cSharpArray[index] = value;
    }

    /// <summary>
    /// Only checked during simulation.<br/>
    /// Not checked in generated C code.<br/>
    /// </summary>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public T unsafe_get(int index)
    {
        if (index < 0 || index >= _cSharpArray.Length)
        {
            throw new IndexOutOfRangeException($"Attempted reading invalid index `{index}` of array of length {_cSharpArray.Length}.");
        }
        return _cSharpArray[index];
    }

    /// <summary>
    /// Only checked during simulation.<br/>
    /// Not checked in generated C code.<br/>
    /// </summary>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public void unsafe_set(int index, T value)
    {
        if (index < 0 || index >= _cSharpArray.Length)
        {
            throw new IndexOutOfRangeException($"Attempted writing value `{value}` to invalid index `{index}` of array of length {_cSharpArray.Length}.");
        }

        _cSharpArray[index] = value;
    }

    // Do this for slices
    //public arr8(c_array<T> values, u8 length)
    //{
    //    _size = (u8)values._cSharpArray.Length;
    //    _cSharpArray = values;
    //}

    // needs error support first
    //public T this[int index]
    //{
    //    get
    //    {
    //        // TODO support memory safety modes like
    //        return _cSharpArray.unsafe_get(index);
    //    }

    //    set
    //    {
    //        _cSharpArray.unsafe_set(index, value: value);
    //    }
    //}

    /// <summary>
    /// Allows foreach loops.
    /// </summary>
    [simonly]
    public IEnumerator<T> GetEnumerator()
    {
        return (_cSharpArray.GetEnumerator() as IEnumerator<T>)!;
    }

    /// <summary>
    /// Allows foreach loops.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _cSharpArray.GetEnumerator();
    }
}
