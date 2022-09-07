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
if errorlevel 1 (
  echo build failed.
  exit /b %errorlevel%
)

:: confirm
dir %CMAKE_LIB%\*.a
dir %CMAKE_LIB%\*.so

:: copy
mkdir %OUTPUT_DIR%
cp %CMAKE_LIB%\%LIBNAME_CRYPTO%.a .\%OUTPUT_DIR%\%LIBNAME_CRYPTO%.a
cp %CMAKE_LIB%\%LIBNAME_CRYPTO%.so .\%OUTPUT_DIR%\%LIBNAME_CRYPTO%.so
cp %CMAKE_LIB%\%LIBNAME_TLS%.a .\%OUTPUT_DIR%\%LIBNAME_TLS%.a
cp %CMAKE_LIB%\%LIBNAME_TLS%.so .\%OUTPUT_DIR%\%LIBNAME_TLS%.so
cp %CMAKE_LIB%\%LIBNAME_X509%.a .\%OUTPUT_DIR%\%LIBNAME_X509%.a
cp %CMAKE_LIB%\%LIBNAME_X509%.so .\%OUTPUT_DIR%\%LIBNAME_X509%.so
