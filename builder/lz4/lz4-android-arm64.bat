@echo OFF
SETLOCAL ENABLEDELAYEDEXPANSION

set SCRIPT_DIR=%~dp0
call %SCRIPT_DIR%\settings.bat
set OS=android
set ABI=arm64-v8a
set PLATFORM=arm64
if not defined OUTPUT_DIR (set OUTPUT_DIR=pkg\%SRC_DIR%\%GIT_VERSION%\%OS%\%PLATFORM%)

:: build
docker run --rm -v "%SCRIPT_DIR%/core:/builder" -v "%cd%/%SRC_DIR%:/src" -e "ABI=%ABI%" ubuntu:22.04 /bin/bash /builder/builder-android.sh
if errorlevel 1 (
  echo build failed.
  exit /b %errorlevel%
)

:: confirm
dir %MAKE_LIB%\%LIBNAME%.a
dir %MAKE_LIB%\%LIBNAME%.so
dir %MAKE_PROGRAMS%\%EXENAME%

:: copy
mkdir %OUTPUT_DIR%
cp .\%MAKE_LIB%\%LIBNAME%.a .\%OUTPUT_DIR%\%LIBNAME%.a
cp .\%MAKE_LIB%\%LIBNAME%.so .\%OUTPUT_DIR%\%LIBNAME%.so
cp .\%MAKE_PROGRAMS%\%EXENAME% .\%OUTPUT_DIR%\%EXENAME%
