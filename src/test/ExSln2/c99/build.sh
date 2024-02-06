# gcc command to compile everything in gen directory
gcc -g -Wall -std=c99 main.c gen/*.c -I gen/ -I . -o app_main

