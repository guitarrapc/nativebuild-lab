#include <stdio.h>
#include <stdlib.h>
#include "fibo.h"

static void help(const char* command)
{
    printf("CLI program to calculate fibonacci numbers.\n");
    printf("\n");
    printf("Usage: %s NUM\n", command);
    printf("  NUM: 0 - 45 (integer)\n");
    printf("Sample:\n");
    printf("  %s 10\n", command);
}

int main(int argc, const char** argv)
{
    if (argc == 1)
    {
        help(argv[0]);
        return 0;
    }

    int n = atoi(argv[1]);

    if (n < 0)
    {
        printf("Do not specify negative number.\n");
        return 1;
    }

    if (n > 45)
    {
        printf("Do not exceed 45m you can specify 0 to 45.\n");
        return 1;
    }

    int result = fibo(n);
    printf("%d\n", result);
    return 0;
}
