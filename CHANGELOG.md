# Changelog
This project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

# Breaking Changes 🌱
Fin is very experimental right now! I wouldn't recommend using it for anything other than for exploration and toy examples right now.Fin is very experimental XXX right now! I wouldn't recommend using it for anything other than for exploration and toy examples right now.

We will occasionally make changes to `fin` that may affect your projects.

You can easily find changes by searching for `"### Changed"` in the in this markdown file.

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

--- 

## [0.1.6-alpha]
### Fixed
- fix ambiguous cast between fin types

### Added
- [ffi] attribute for c# classes (foreign function interface)

--- 

## [0.1.5-alpha]
### Added
- integer self type declaration like `my_u16.u16_`
    - https://github.com/fin-language/fin/issues/21

---

## [0.1.4-alpha]
### Added
- `narrow_to_x()` methods like `u32.narrow_to_u8()`
- `narrow_from()` methods like `u8.narrow_from_u32()`

### Fixed
- casting a C# int type to a fin type now uses a narrowing (checked) conversion.
- removed wrapping methods from signed types for now (C implementation defined behavior).

---

## [0.1.0-alpha]
Still only simulating in C#. Not generating C99 yet.

### Added
- `c_array<T>` naked C style array (no length)
- fin integer types `u8`, `u16`, `u32`, `u64`, `i8`, `i16`, `i32`, `i64` with most operators overloaded
- math unsafe mode and user error provided mode
- Err object for handling errors
    - detects if not checked and cleared when going out of scope
- `mem.stack()` and `mem.heap()` for allocating memory
