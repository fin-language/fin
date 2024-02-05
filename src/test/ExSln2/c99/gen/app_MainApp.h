#include <stdint.h>
#include <stdbool.h>


typedef struct app_MainApp app_MainApp;  // forward declaration
struct app_MainApp
{
    uint32_t _toggle_at_ms;
    hal_Led * _redLed;
};

