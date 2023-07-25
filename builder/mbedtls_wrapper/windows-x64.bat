@echo OFF
SETLOCAL ENABLEDELAYEDEXPANSION

set SCRIPT_DIR=%~dp0
call %SCRIPT_DIR%\settings.bat
set OS=windows
set ARCH=x64
set PLATFORM=x64
if not defined OUTPUT_DIR (set OUTPUT_DIR=pkg\%SRC_DIR%\%GIT_VERSION%\%OS%\%PLATFORM%)

:: copy libs
if exist mbedtls_wrapper\cmake\mbedtls (
  rm -r mbedtls_wrapper\cmake\mbedtls
)
mkdir mbedtls_wrapper\cmake\mbedtls\include\mbedtls
mkdir mbedtls_wrapper\cmake\mbedtls\include\psa
mkdir mbedtls_wrapper\cmake\mbedtls\library
copy /Y NUL mbedtls_wrapper\cmake\mbedtls\library\CmakeLists.txt
cp -f mbedtls\cmake\build.dir\library\Release\mbed*.lib mbedtls_wrapper\cmake\mbedtls\
cp -f mbedtls\include\*.txt mbedtls_wrapper\cmake\mbedtls\include\
cp -f mbedtls\include\mbedtls\*.h mbedtls_wrapper\cmake\mbedtls\include\mbedtls\
cp -f mbedtls\include\psa\*.h mbedtls_wrapper\cmake\mbedtls\include\psa
cp -f mbedtls\library\*.h mbedtls_wrapper\cmake\mbedtls\library\

:: build
call %SCRIPT_DIR%\core\builder-windows.bat
@REM if errorlevel 1 (
@REM   echo build failed.
@REM   exit /b %errorlevel%
@REM )

@REM :: confirm
@REM dir %CMAKE_LIB%\Release\*.lib
@REM dir %CMAKE_LIB%\Release\*.dll
@REM dir %CMAKE_LIB%\Release\*.exe

@REM :: copy
@REM mkdir %OUTPUT_DIR%
@REM cp %CMAKE_LIB%\Release\%WIN_LIBNAME%.dll .\%OUTPUT_DIR%\%WIN_LIBNAME%.dll
@REM cp %CMAKE_LIB%\Release\%WIN_LIBNAME%.lib .\%OUTPUT_DIR%\%WIN_LIBNAME%.lib
@REM cp %CMAKE_LIB%\Release\%WIN_LIBNAME%cli.exe .\%OUTPUT_DIR%\%WIN_LIBNAME%cli.exe
