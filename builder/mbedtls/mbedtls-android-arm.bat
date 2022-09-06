@echo OFF
SETLOCAL ENABLEDELAYEDEXPANSION

set SCRIPT_DIR=%~dp0
call %SCRIPT_DIR%\settings.bat
set OS=android
set ABI=armeabi-v7a
set PLATFORM=arm
if not defined OUTPUT_DIR (set OUTPUT_DIR=pkg\%SRC_DIR%\%GIT_VERSION%\%OS%\%PLATFORM%)

:: build
docker run --rm -v "%SCRIPT_DIR%/core:/builder" -v "%cd%/%SRC_DIR%:/src" -e "ABI=%ABI%" -e "PREFIX=%PREFIX%" ubuntu:22.04 /bin/bash /builder/builder-android.sh

:: confirm
dir %SRC_DIR%\cmake\build.dir\library\%LIBNAME_CRYPTO%.a
dir %SRC_DIR%\cmake\build.dir\library\%LIBNAME_CRYPTO%.so
dir %SRC_DIR%\cmake\build.dir\library\%LIBNAME_TLS%.a
dir %SRC_DIR%\cmake\build.dir\library\%LIBNAME_TLS%.so
dir %SRC_DIR%\cmake\build.dir\library\%LIBNAME_X509%.a
dir %SRC_DIR%\cmake\build.dir\library\%LIBNAME_X509%.so

:: copy
mkdir %OUTPUT_DIR%
cp .\%SRC_DIR%\cmake\build.dir\library\%LIBNAME_CRYPTO%.a .\%OUTPUT_DIR%\%LIBNAME_CRYPTO%.a
cp .\%SRC_DIR%\cmake\build.dir\library\%LIBNAME_CRYPTO%.so .\%OUTPUT_DIR%\%LIBNAME_CRYPTO%.so
cp .\%SRC_DIR%\cmake\build.dir\library\%LIBNAME_TLS%.a .\%OUTPUT_DIR%\%LIBNAME_TLS%.a
cp .\%SRC_DIR%\cmake\build.dir\library\%LIBNAME_TLS%.so .\%OUTPUT_DIR%\%LIBNAME_TLS%.so
cp .\%SRC_DIR%\cmake\build.dir\library\%LIBNAME_X509%.a .\%OUTPUT_DIR%\%LIBNAME_X509%.a
cp .\%SRC_DIR%\cmake\build.dir\library\%LIBNAME_X509%.so .\%OUTPUT_DIR%\%LIBNAME_X509%.so
