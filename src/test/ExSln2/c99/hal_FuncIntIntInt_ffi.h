#pragma once
#include <stdint.h>

typedef int32_t (*hal_FuncIntIntInt_FuncPtr)(int32_t a, int32_t b);

typedef struct hal_FuncIntIntInt {
    hal_FuncIntIntInt_FuncPtr ptr;
} hal_FuncIntIntInt;
