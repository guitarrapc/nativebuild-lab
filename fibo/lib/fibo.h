#include <stdio.h>

// __cplusplus enabled when compile with g++
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
#  define FIBOLIB_API __declspec(dllimport) FIBOLIB_VISIBILITY /* It isn't required but allows to generate better code, saving a function pointer load from the IAT and an indirect jump.*/
#else
#  define FIBOLIB_API FIBOLIB_VISIBILITY
#endif

FIBOLIB_API int fibo(int n);

#if defined (__cplusplus)
}
#endif
