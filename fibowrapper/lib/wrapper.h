#include <stdio.h>

// __cplusplus enabled when compile with g++
#if defined (__cplusplus)
extern "C" {
#endif

#ifndef WRAPPERLIB_VISIBILITY
#  if defined(__GNUC__) && (__GNUC__ >= 4)
#    define WRAPPERLIB_VISIBILITY __attribute__ ((visibility ("default")))
#  else
#    define WRAPPERLIB_VISIBILITY
#  endif
#endif
#if defined(DLL_EXPORT)
#  define WRAPPERLIB_API __declspec(dllexport) WRAPPERLIB_VISIBILITY
#elif defined(DLL_IMPORT)
#  define WRAPPERLIB_API __declspec(dllimport) WRAPPERLIB_VISIBILITY /* It isn't required but allows to generate better code, saving a function pointer load from the IAT and an indirect jump.*/
#else
#  define WRAPPERLIB_API WRAPPERLIB_VISIBILITY
#endif

WRAPPERLIB_API int my_fibo(int n);

#if defined (__cplusplus)
}
#endif
