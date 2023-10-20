namespace fin.lang;

////these are needed to avoid implicit conversion confusions when mixing signed and unsigned.
public interface IHasU8  { public u8  value { get; } }
public interface IHasU16 { public u16 value { get; } }
public interface IHasU32 { public u32 value { get; } }
public interface IHasU64 { public u64 value { get; } }
public interface IHasI8  { public i8  value { get; } }
public interface IHasI16 { public i16 value { get; } }
public interface IHasI32 { public i32 value { get; } }
public interface IHasI64 { public i64 value { get; } }

