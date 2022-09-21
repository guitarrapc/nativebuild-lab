#include <stdio.h>
#include "sample.h"

const sample_data_t sample_data = {0.1f, 50};

void set_sample_data(sample_data_t *output, sample_data_t *input)
{
    output->value = input->value + 0.2f;
    output->num = input->num +20;
}
void get_sample_data(sample_data_t *output)
{
    output->value = sample_data.value;
    output->num = sample_data.num;
}

int echo_no_export(int n)
{
    return n;
}
