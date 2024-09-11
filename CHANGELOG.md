# Changelog
This project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

# Breaking Changes 🌱
Fin is very experimental right now! I wouldn't recommend using it for anything other than for exploration and toy examples right now.

We will occasionally make changes to `fin` that may affect your projects.

You can easily find changes by searching for `"### Change"` in this markdown file.

# Release Template

```
## [some_version]
### Added
### Fixed
### Fixed (minor, major, ...)
### Changed
### Changed (BREAKING-CHANGES, minor, ...)
```

# Releases
Test/interim releases are not documented here.


## [0.???]
### Fixed
- fix delegate and pointer type parameters
    - https://github.com/fin-language/fin/issues/86

### Added
- add duplicate include statements after fin type is resolved to c header file
    - we had files with multiple `#include <stdint.h>` statements because fin types like u8, u16, etc. all resolved to `#include <stdint.h>`.

---

## [0.6.1]
### Added
- Add `c_array_mem<T>`
    - https://github.com/fin-language/fin/issues/87

### Changed (minor)
- math mode defaults to `unsafe mode` for now.

---

## [0.6.0]
### Fixed
- mem - return pointer to mem object; get pointer of mem object on assignment
    - https://github.com/fin-language/fin/issues/79

### Added
- Add initial support for delegates
    - for now, delegates must be defined inside a fin class
    - https://github.com/fin-language/fin/issues/77
- Add default constructor if missing
    - https://github.com/fin-language/fin/issues/32
- Add wrapping add/sub/mul methods
    - https://github.com/fin-language/fin/issues/30
- Add support for fin numerics MIN, MAX constants like u8.MAX
    - https://github.com/fin-language/fin/issues/80

---

## [0.5.7]
### Changed (minor)
- skip creating blank .c files for Plain Old Data (POD) type structures
    - https://github.com/fin-language/fin/issues/75

---

## [0.5.6]
### Changed (minor)
- Relaxed type requirements on `IMangledNameProvider.FromFinType<T>`

---

## [0.5.5]
### Changed (minor)
- Moved `MangledNameProvider.FromFinType<T>` into `IMangledNameProvider` interface

---

## [0.5.4]
### Added
- Add `IMangledNameProvider`
    - Useful for additional user template based code generation

---

## [0.5.3]
### Added
- Add StringUtils indent functions

---

## [0.5.0]
### Changed (BREAKING-CHANGES)
- Renamed `finlang.Transpiler.Transpiler` to `finlang.Transpiler.CTranspiler` to avoid name conflicts between namespace and class.
- change private methods to be `static` without module prefix
    - https://github.com/fin-language/fin/issues/72

---

## [0.4.0]
### Changed (BREAKING-CHANGES)
- Transpiler - Interface conversion functions change: MCL and MAA macros.
    - https://github.com/fin-language/fin/issues/60
- Transpiler - option to specify FFI port implementation header file
    - It also defaults to `_ffi.h` isntead of `_port_implementation.h`
    - https://github.com/fin-language/fin/issues/69

---

## [0.3.4]
### Fixed
- Transpiler - fix #57 Generated vtable variable name should be different per file generated
    - https://github.com/fin-language/fin/issues/57

---

## [0.3.3]
### Changed
- Transpiler - only transpile interfaces that have IFinObj as an ancestor
    - https://github.com/fin-language/fin/issues/63
### Added
- Transpiler - allow environment variables to specify which type transpiler should focus on
    - https://github.com/fin-language/fin/issues/62

---

## [0.3.2]
### Fixed
- Transpiler - fix #58 incorrect vtable method return type for pointer to object
    - https://github.com/fin-language/fin/issues/58

---

## [0.3.1]
### Added
- Transpiler - support constant fields for numbers
	- https://github.com/fin-language/fin/issues/51
- Transpiler - support `[simonly]` attribute on fields
- Transpiler - initial support for `sizeof()` like functionality
	- https://github.com/fin-language/fin/issues/50
- Transpiler - initial style support for indent and line endings
    - https://github.com/fin-language/fin/issues/55

### Fixed
- Transpiler - fix `FinC.ignore_unused(this)`
    - https://github.com/fin-language/fin/issues/45
- Transpiler - fix interface generation for non-bool types

### Changed
- Transpiler - output `Source file` header info using Linux path separators
    - https://github.com/fin-language/fin/issues/46
- Transpiler - generated line endings now use style settings which default to Environment.NewLine.
    - https://github.com/fin-language/fin/issues/55

---

## [0.3.0]
### Added
- Transpiler - initial support for a field to be an actual class object type and not just a pointer to it.
    - field: `[mem] public SomeObj obj;`
- Transpiler - initial support for `c_array_sized<T>`.
    - ex: `[mem] c_array_sized<u8> data = mem.init(new c_array_sized<u8>(5));`
    - Currently only supports fields (not local variables).
    - Doesn't need `[mem]` because it is implied. You can add it though.
- Transpiler - add ability to customize the output file path and name
	- See `transpiler.SetFileNamer()` for more details.
- Transpiler - add default options to output finlang version, date/time, and MD5 filepath.
    - Also outputs source file path.
    - See `transpiler.Options` for more details.
- Transpiler - support `SimOnly.run(() => { /* code */ })` for running code in simulation mode only.
    - This code can be in constructors, or local methods...
- Transpiler - support C# binary/hex literals with underscore separators
    - `0b1010_1110` --> `0b10101110`
    - `0x1234_5678` --> `0x12345678`
- Transpiler - initial support for C# ref and out parameters of primitives
- Transpiler - initial support for C# in parameters of primitives
- Transpiler - initial support for field declaration initial values.
    - https://github.com/fin-language/fin/issues/38
- Transpiler - support constructing field mem objects
    - https://github.com/fin-language/fin/issues/37
- Transpiler - type override for fields and parameters
	- https://github.com/fin-language/fin/issues/41
- Transpiler - support `FinC.EchoToC("some code")`
    - https://github.com/fin-language/fin/issues/43
- Transpiler - support [add_include("some_include")] attribute on classes
    - https://github.com/fin-language/fin/issues/44

---

## [0.2.6]
### Added
- Transpiler - initial support for interfaces
    - interface inheritance, multiple interfaces, interface conversions...
- Transpiler - allow generating C to ignore unused warning: `FinC.ignore_unused(some_var)`
- Transpiler - support FFI on classes that implement interfaces
- Transpiler - support FFI on specific methods

---

## [0.2.5]
### Fixed
- Transpiler - now tracks header dependency of type `T` contained in `c_array<T>`
- Transpiler - fix pass `self` to method calls like `this.toggle()`
- Transpiler - prevent .h file from including itself in the same file

---

## [0.2.4]
### Added
- Transpiler - support nested classes/enums although indentation is not correct yet
- Transpiler - handle an object calling its own method without `this` like `toggle()`
- Transpiler - allow nullable types like `Led?`, convert `null` to `NULL`
- Transpiler - ignore  `required`, `virtual`, `override`, `sealed`, `in`
- Transpiler - private methods are prefixed with `PRIVATE_`

### Fixed
- Transpiler - fix `c_array` of reference types

---

## [0.2.3]
### Added
- Transpiler - support enumerations
    - They can't be nested inside a class yet.
- Transpiler - support fin numeric `wrap_lshift()`
    - ex: `(my_u8 + 10).wrap_lshift(i + 2)` --> `(uint8_t)((my_u8 + 10) << (i + 2))`

---

## [0.2.2]
### Added
- Transpiler - support narrowing casts.
    - ex: `b = (u8)my_i32;` --> `b = (uint8_t)my_i32;`
- Transpiler - support `u8.narrow_from(some_arg)`
    - ex: `u8.narrow_from(my_i32)` --> `(uint8_t)my_i32`
- Transpiler - support c style naked arrays `c_array<T>`
- Improve 1-1 transpilation of fin to C99
    - removed indentation from C# and other small improvements

--- 

## [0.2.1]
### Added
- Moved Transpiler into main finlang nuget package
- `transpiler.GetListOfAllGeneratedFiles()`

### Fixed
- Transpiler supports XML doc comments `///<summary>...</summary`

--- 

## [0.2.0]
### Fixed
- fix ambiguous cast between fin types

### Added
- [ffi] attribute for c# classes (foreign function interface)

--- 

## [0.1.5]
### Added
- integer self type declaration like `my_u16.u16_`
    - https://github.com/fin-language/fin/issues/21

---

## [0.1.4]
### Added
- `narrow_to_x()` methods like `u32.narrow_to_u8()`
- `narrow_from()` methods like `u8.narrow_from_u32()`

### Fixed
- casting a C# int type to a fin type now uses a narrowing (checked) conversion.
- removed wrapping methods from signed types for now (C implementation defined behavior).

---

## [0.1.0]
Still only simulating in C#. Not generating C99 yet.

### Added
- `c_array<T>` naked C style array (no length)
- fin integer types `u8`, `u16`, `u32`, `u64`, `i8`, `i16`, `i32`, `i64` with most operators overloaded
- math unsafe mode and user error provided mode
- Err object for handling errors
    - detects if not checked and cleared when going out of scope
- `mem.stack()` and `mem.heap()` for allocating memory
