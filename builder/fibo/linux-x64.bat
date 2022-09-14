@echo OFF
SETLOCAL ENABLEDELAYEDEXPANSION

set SCRIPT_DIR=%~dp0
call %SCRIPT_DIR%\settings.bat
set OS=linux
set PLATFORM=x64
if not defined OUTPUT_DIR (set OUTPUT_DIR=pkg\%SRC_DIR%\%GIT_VERSION%\%OS%\%PLATFORM%)

:: build
docker run --rm -v "%SCRIPT_DIR%/core:/builder" -v "%cd%/%SRC_DIR%:/src" -e "PREFIX=%PREFIX%" alpine:latest /bin/sh /builder/builder-linux-x64.sh
if errorlevel 1 (
  echo build failed.
  exit /b %errorlevel%
)

:: confirm
dir %CMAKE_LIB%\*.a
dir %CMAKE_LIB%\*.so*

:: copy
mkdir %OUTPUT_DIR%
cp .\%CMAKE_LIB%\%LIBNAME%.a .\%OUTPUT_DIR%\%LIBNAME%.a
cp .\%CMAKE_LIB%\%LIBNAME%.so .\%OUTPUT_DIR%\%LIBNAME%.so
