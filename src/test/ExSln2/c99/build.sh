# gcc command to compile everything in gen directory
gcc -g -Wall -Wpedantic -std=c11 main.c hal_Gpio_ffi.c gen/*.c -I gen/ -I . -o app_main

