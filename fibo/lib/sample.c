#include <stdio.h>
#include "sample.h"

void get_sample_data(sample_data_t *output, sample_data_t *input)
{
    output->value = input->value + 0.2f;
    output->num = input->num +20;
}

int echo_no_export(int n)
{
    return 1;
}
