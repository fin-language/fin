// AUTOGENERATED FILE. Do not modify this file manually, it will be overwritten.
// finlang generated this file for C# `app.TimeProvider` type.
// Source file: `LightsApp/app/TimeProvider.cs` (relative to C# solution).

#pragma once

#include "app_TimeProvider_ffi.h" // You need to provide this
#include <stdint.h>
#include "app_ITimeProvider.h"


// Class is a Foreign Function Interface. No struct generated.

uint32_t app_TimeProvider_get_time(app_TimeProvider * self);

// vtable is extern to allow const initializations
extern const app_ITimeProvider_vtable app_TimeProvider__app_ITimeProvider_vtable_imp;

// Up conversion from app_TimeProvider to app_ITimeProvider interface
// MAA stands for Macro Aggregate Assignment. See https://github.com/fin-language/fin/issues/60 
#define MAA_app_TimeProvider__to__app_ITimeProvider(self_arg)    { .obj = self_arg, .obj_vtable = (const app_ITimeProvider_vtable*)(&app_TimeProvider__app_ITimeProvider_vtable_imp.get_time) }
// MCL stands for Macro Compound Literal. See https://github.com/fin-language/fin/issues/60 
#define MCL_app_TimeProvider__to__app_ITimeProvider(self_arg)    (app_ITimeProvider){ .obj = self_arg, .obj_vtable = (const app_ITimeProvider_vtable*)(&app_TimeProvider__app_ITimeProvider_vtable_imp.get_time) }
