## Windows

**Docker**

Cannot build for Windows in Linux.

```Dockerfile
# Dockerfile
# First image to build the binary
FROM alpine as builder

RUN apk --no-cache add make gcc libc-dev
COPY . /src
RUN mkdir /pkg && cd /src && OS=Windows make
```

```
make[1]: Entering directory '/src/lib'
compiling dynamic library 1.5.3
In file included from common/error_private.h:24,
                 from common/entropy_common.c:19:
common/../zstd_errors.h:31:28: warning: data definition has no type or storage class
   31 | #  define ZSTDERRORLIB_API __declspec(dllexport) ZSTDERRORLIB_VISIBILITY
      |                            ^~~~~~~~~~
common/../zstd_errors.h:88:1: note: in expansion of macro 'ZSTDERRORLIB_API'
   88 | ZSTDERRORLIB_API ZSTD_ErrorCode ZSTD_getErrorCode(size_t functionResult);
      | ^~~~~~~~~~~~~~~~
```


**msys**

Can not build.

```
scoop install make gcc
```

```
make
```
