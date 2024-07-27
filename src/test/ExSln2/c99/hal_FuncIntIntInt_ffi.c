#include "hal_FuncIntIntInt.h"

void hal_FuncIntIntInt_ctor(hal_FuncIntIntInt * self, hal_FuncIntIntInt_FuncPtr func_ptr)
{
    self->ptr = func_ptr;
}

void hal_FuncIntIntInt_set(hal_FuncIntIntInt * self, hal_FuncIntIntInt_FuncPtr func_ptr)
{
    self->ptr = func_ptr;
}

int32_t hal_FuncIntIntInt_call(hal_FuncIntIntInt * self, int32_t a, int32_t b)
{
    return self->ptr(a, b);
}
