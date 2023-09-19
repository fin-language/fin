namespace fin.sim.lang;

////these are needed to avoid implicit conversion confusions when mixing signed and unsigned.
public interface IHasU8 { public u8 v { get; } }
public interface IHasU16 { public u16 v { get; } }
public interface IHasU32 { public u32 v { get; } }
public interface IHasU64 { public u64 v { get; } }
public interface IHasI8 { public i8 v { get; } }
public interface IHasI16 { public i16 v { get; } }
public interface IHasI32 { public i32 v { get; } }
public interface IHasI64 { public i64 v { get; } }

