set SRC_DIR=mbedtls_wrapper
set MAKE_LIB=%SRC_DIR%\library
set CMAKE_LIB=%SRC_DIR%\cmake\build

if not defined PREFIX (set PREFIX=)
set LIBNAME=lib%PREFIX%fibo
set WIN_LIBNAME=%PREFIX%fibo

cd %SRC_DIR%
  SET VERSION=v1.0.0
  set GIT_VERSION=%VERSION%
  :: 'v1.5.2 ' -> '1.5.2'
  set FILE_VERSION=%VERSION:~1%
cd ..
