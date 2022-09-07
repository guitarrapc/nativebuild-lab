@echo OFF
SETLOCAL ENABLEDELAYEDEXPANSION

set SCRIPT_DIR=%~dp0
call %SCRIPT_DIR%\settings.bat
set OS=android
set ABI=x86_64
set PLATFORM=x64
if not defined OUTPUT_DIR (set OUTPUT_DIR=pkg\%SRC_DIR%\%GIT_VERSION%\%OS%\%PLATFORM%)

:: build
docker run --rm -v "%SCRIPT_DIR%/core:/builder" -v "%cd%/%SRC_DIR%:/src" -e "ABI=%ABI%" ubuntu:22.04 /bin/bash /builder/builder-android.sh
if errorlevel 1 (
  echo build failed.
  exit /b %errorlevel%
)

:: confirm
dir %SRC_DIR%\lib\%LIBNAME%.a
dir %SRC_DIR%\lib\%LIBNAME%.so
dir %SRC_DIR%\programs\%EXENAME%

:: copy
mkdir %OUTPUT_DIR%
cp .\%SRC_DIR%\lib\%LIBNAME%.a .\%OUTPUT_DIR%\%LIBNAME%.a
cp .\%SRC_DIR%\lib\%LIBNAME%.so .\%OUTPUT_DIR%\%LIBNAME%.so
cp .\%SRC_DIR%\programs\%EXENAME% .\%OUTPUT_DIR%\%EXENAME%
