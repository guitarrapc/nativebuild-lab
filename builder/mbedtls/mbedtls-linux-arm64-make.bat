@echo OFF
SETLOCAL ENABLEDELAYEDEXPANSION

set SCRIPT_DIR=%~dp0
call %SCRIPT_DIR%\settings.bat
set OS=linux
set PLATFORM=arm64
if not defined OUTPUT_DIR (set OUTPUT_DIR=pkg\%SRC_DIR%\%GIT_VERSION%\%OS%\%PLATFORM%)

:: build
docker run --rm -v "%SCRIPT_DIR%/core:/builder" -v "%cd%/%SRC_DIR%:/src" ubuntu:22.04 /bin/bash /builder/builder-linux-arm64-make.sh
if errorlevel 1 (
  echo build failed.
  exit /b %errorlevel%
)

:: confirm
dir %MAKE_LIB%\*.a
dir %MAKE_LIB%\*.so*

:: copy
mkdir %OUTPUT_DIR%
cp .\%MAKE_LIB%\%LIBNAME_CRYPTO%.a .\%OUTPUT_DIR%\%LIBNAME_CRYPTO%.a
cp .\%MAKE_LIB%\%LIBNAME_CRYPTO%.so.12 .\%OUTPUT_DIR%\%LIBNAME_CRYPTO%.so
cp .\%MAKE_LIB%\%LIBNAME_TLS%.a .\%OUTPUT_DIR%\%LIBNAME_TLS%.a
cp .\%MAKE_LIB%\%LIBNAME_TLS%.so.18 .\%OUTPUT_DIR%\%LIBNAME_TLS%.so
cp .\%MAKE_LIB%\%LIBNAME_X509%.a .\%OUTPUT_DIR%\%LIBNAME_X509%.a
cp .\%MAKE_LIB%\%LIBNAME_X509%.so.4 .\%OUTPUT_DIR%\%LIBNAME_X509%.so
