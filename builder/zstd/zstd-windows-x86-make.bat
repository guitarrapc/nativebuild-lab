@echo OFF
SETLOCAL ENABLEDELAYEDEXPANSION

set SCRIPT_DIR=%~dp0
call %SCRIPT_DIR%\settings.bat
set OS=windows
set PLATFORM=x86
if not defined OUTPUT_DIR (set OUTPUT_DIR=pkg\%SRC_DIR%\%GIT_VERSION%\%OS%\%PLATFORM%)

:: build
::docker run --rm -v "%SCRIPT_DIR%/core:/builder" -v "%cd%/%SRC_DIR%:/src" ubuntu:22.04 /bin/bash /builder/builder-windows-x86-make.sh
docker run --rm -v "%SCRIPT_DIR%/core:/builder" -v "%cd%/%SRC_DIR%:/src" guitarrapc/ubuntu-mingw-w64:22.04.1 /bin/bash /builder/builder-windows-x86-make.sh
if errorlevel 1 (
  echo build failed.
  exit /b %errorlevel%
)

:: confirm
dir %MAKE_LIB%\dll\%LIBNAME%.dll
dir %MAKE_PROGRAMS%\%EXENAME%.exe

:: copy
mkdir %OUTPUT_DIR%\mingw\
cp .\%MAKE_LIB%\dll\%LIBNAME%.dll .\%OUTPUT_DIR%\mingw\%LIBNAME%.dll
cp .\%MAKE_PROGRAMS%\%EXENAME%.exe .\%OUTPUT_DIR%\mingw\%EXENAME%.exe
