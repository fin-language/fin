#include <stdint.h>
#include <stdbool.h>


typedef struct hal_Led hal_Led;  // forward declaration
struct hal_Led
{
    hal_Gpio * _gpio;
};

