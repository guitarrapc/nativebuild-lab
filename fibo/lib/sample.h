#include <stdio.h>

typedef struct
{
    float value;
    int num;
} sample_data_t;

// __cplusplus enabled when compile with g++, to avoid name mangling via C++ compiler.
#if defined (__cplusplus)
extern "C" {
#endif

#ifndef FIBOLIB_VISIBILITY
#  if defined(__GNUC__) && (__GNUC__ >= 4)
#    define FIBOLIB_VISIBILITY __attribute__ ((visibility ("default")))
#  else
#    define FIBOLIB_VISIBILITY
#  endif
#endif
#if defined(DLL_EXPORT)
#  define FIBOLIB_API __declspec(dllexport) FIBOLIB_VISIBILITY
#elif defined(DLL_IMPORT)
#  define FIBOLIB_API __declspec(dllimport) FIBOLIB_VISIBILITY
#else
#  define FIBOLIB_API FIBOLIB_VISIBILITY
#endif

FIBOLIB_API void get_sample_data(sample_data_t *output, sample_data_t *input);

// This function won't export on DLL.
int echo_no_export(int n);

#if defined (__cplusplus)
}
#endif
