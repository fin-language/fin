This project uses a few Fody weavers.

# Required for `fin`:
## MethodDecorator
Allows `fin` to intercept all method calls. Used for tracking stuff like `math.unsafe_mode();`

Settings in `AssemblyAttributes.cs`.

# Recommended (not required for `fin`):
## Virtuosity
Makes all methods be virtual. Useful for testing/mocking. You don't need to write `virtual`.

Settings in `FodyWeavers.xml`.


