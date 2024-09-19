# The Gist
`c_array_sized<u8> data` transpiles to `uint8_t data[X]`.

Our current workaround for size is that it comes from the `mem.init(new c_array_sized<u8>(5))` call. This is a bit of a hack, but it works for now.

This class will eventually be replaced with a more elegant solution.

Future might have something like `c_array<T, size>`.


# C Behavior
Before we get into the usage, let's talk about C behavior with arrays.

## You can't re-assign a c array
https://rextester.com/YJMTZ15622

```c
int main(void)
{
    int a[2] = {0, 1};
    int b[2] = {4, 5};
    a = b; // error: assignment to expression with array type
    printf("a[0] = %i", a[0]);
    return 0;
}
```

## Automatically decays to pointer
https://rextester.com/JZK54007

```c
int main(void)
{
    int a[2] = {0, 1};
    int * b = a;
    printf("b[0] = %i", b[0]);
    return 0;
}
```

## Decays to pointer on function call
C's biggest mistake article:
> once arrays leave the scope in which they are defined, they become pointers, and lose the information which gives the extent of the array — the array dimension.<br>
> https://digitalmars.com/articles/C-biggest-mistake.html


https://rextester.com/PTCWB23387

```c
void my_func(char a[2]) {
    // warning: ‘sizeof’ on array function parameter ‘a’ will return size 
    // of ‘char *’ [-Wsizeof-array-argument]
    printf("char a[2]: sizeof(a) = %lu\n", sizeof(a));
}
```


<br>
<br>


# `c_array_sized<T>` Usage
## Use it for class fields
```cs
//fin
public class CArraySizedEx : FinObj
{
    // NO [mem] attribute
    c_array_sized<u8> data = mem.init(new c_array_sized<u8>(5));
}
```

```c
// c99 .h
struct CArraySizedEx
{
    uint8_t data[5];
};
```

## `.length`
```cs
//fin
public class CArraySizedEx : FinObj
{
    //...

    public u16 sum()
    {
        u16 sum = 0;
        for (u8 i = 0; i < data.length; i++)
        {
            sum += data.unsafe_get(i);
        }
        return sum;
    }
}
```

## For local variables
This probably is not supported yet. Very doable though.

```cs
//fin
public void my_func()
{
    c_array_sized<u8> data = mem.stack(new c_array_sized<u8>(5));
    //...
}
```

## Not allowed for function parameters
Not allowed to be declared for an function parameter as C doesn't support something like

```c
void my_func(uint8_t data[5]);  // user written, but it really decays to below
void my_func(uint8_t * data);  // what the compiler actually sees
```

> once arrays leave the scope in which they are defined, they become pointers, and lose the information which gives the extent of the array — the array dimension.
> https://digitalmars.com/articles/C-biggest-mistake.html



## Decays to `c_array<T>`
Just like c.


```cs
//fin
public class CArraySizedEx : FinObj
{
    // NO [mem] attribute
    c_array_sized<u8> data = mem.init(new c_array_sized<u8>(5));

    public u16 show_decay_to_c_array_parameter()
    {
        return sum_c_array(data, data.length.narrow_to_u8());
    }

    public u16 show_decay_to_c_array_assignment()
    {
        c_array<u8> arr = data;
        return sum_c_array(arr, data.length.narrow_to_u8());
    }

    public static u16 sum_c_array(c_array<u8> arr, u8 length)
    {
        //...
    }
}
```



