@echo OFF
SETLOCAL ENABLEDELAYEDEXPANSION

set SCRIPT_DIR=%~dp0
call %SCRIPT_DIR%\settings.bat
set OS=linux
set PLATFORM=x64
if not defined OUTPUT_DIR (set OUTPUT_DIR=pkg\%SRC_DIR%\%GIT_VERSION%\%OS%\%PLATFORM%)

:: build
docker run --rm -v "%SCRIPT_DIR%/core:/builder" -v "%cd%/%SRC_DIR%:/src" alpine:latest /bin/sh /builder/builder-linux-x64.sh

:: confirm
dir %SRC_DIR%\library\*.a
dir %SRC_DIR%\library\*.so*

:: copy
mkdir %OUTPUT_DIR%
cp .\%SRC_DIR%\library\%LIBNAME_CRYPTO%.a .\%OUTPUT_DIR%\%LIBNAME_CRYPTO%.a
cp .\%SRC_DIR%\library\%LIBNAME_CRYPTO%.so.12 .\%OUTPUT_DIR%\%LIBNAME_CRYPTO%.so
cp .\%SRC_DIR%\library\%LIBNAME_TLS%.a .\%OUTPUT_DIR%\%LIBNAME_TLS%.a
cp .\%SRC_DIR%\library\%LIBNAME_TLS%.so.18 .\%OUTPUT_DIR%\%LIBNAME_TLS%.so
cp .\%SRC_DIR%\library\%LIBNAME_X509%.a .\%OUTPUT_DIR%\%LIBNAME_X509%.a
cp .\%SRC_DIR%\library\%LIBNAME_X509%.so.4 .\%OUTPUT_DIR%\%LIBNAME_X509%.so
