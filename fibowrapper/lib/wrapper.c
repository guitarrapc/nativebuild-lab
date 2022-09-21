#include <stdio.h>
#include "fibo.h"
#include "wrapper.h"

int wrapper(int n)
{
    return fibo(n);
}
