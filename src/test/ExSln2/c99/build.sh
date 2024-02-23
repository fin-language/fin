# gcc command to compile everything in gen directory
gcc -g -Wall -std=c11 main.c hal_Gpio_port_implementation.c gen/*.c -I gen/ -I . -o app_main

