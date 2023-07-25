#include <stdio.h>
#include "fibo.h"
#include "wrapper.h"

int my_fibo(int n)
{
    return _fibo(n);
}
