#include <stdio.h>
#include "fibo.h"

int fibo(int n)
{
    int prev = 1;
    int fprev = 1;
    int tmp = 1;

    for (int i = 1; i < n; i++)
    {
        tmp = prev + fprev;
        prev = fprev;
        fprev = tmp;
    }
    return tmp;
}
