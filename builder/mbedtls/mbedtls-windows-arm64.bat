@echo OFF
SETLOCAL ENABLEDELAYEDEXPANSION

set SCRIPT_DIR=%~dp0
call %SCRIPT_DIR%\settings.bat
set OS=windows
set ARCH=ARM64
set PLATFORM=arm64
if not defined OUTPUT_DIR (set OUTPUT_DIR=pkg\%SRC_DIR%\%GIT_VERSION%\%OS%\%PLATFORM%)

:: build
call %SCRIPT_DIR%\core\builder-windows.bat
if errorlevel 1 (
  echo build failed.
  exit /b %errorlevel%
)

:: confirm
dir %CMAKE_LIB%\Release\*.lib
dir %CMAKE_LIB%\Release\*.dll

:: copy
mkdir %OUTPUT_DIR%
cp %CMAKE_LIB%\Release\%WIN_LIBNAME_CRYPTO%.dll .\%OUTPUT_DIR%\%WIN_LIBNAME_CRYPTO%.dll
cp %CMAKE_LIB%\Release\%WIN_LIBNAME_CRYPTO%.lib .\%OUTPUT_DIR%\%WIN_LIBNAME_CRYPTO%.lib
cp %CMAKE_LIB%\Release\%WIN_LIBNAME_TLS%.dll .\%OUTPUT_DIR%\%WIN_LIBNAME_TLS%.dll
cp %CMAKE_LIB%\Release\%WIN_LIBNAME_TLS%.lib .\%OUTPUT_DIR%\%WIN_LIBNAME_TLS%.lib
cp %CMAKE_LIB%\Release\%WIN_LIBNAME_X509%.dll .\%OUTPUT_DIR%\%WIN_LIBNAME_X509%.dll
cp %CMAKE_LIB%\Release\%WIN_LIBNAME_X509%.lib .\%OUTPUT_DIR%\%WIN_LIBNAME_X509%.lib
