@echo OFF
SETLOCAL ENABLEDELAYEDEXPANSION

set SCRIPT_DIR=%~dp0
call %SCRIPT_DIR%\settings.bat
set OS=windows
set PLATFORM=arm64
if not defined OUTPUT_DIR (set OUTPUT_DIR=pkg\%SRC_DIR%\%GIT_VERSION%\%OS%\%PLATFORM%)

:: build
docker run --rm -v "%SCRIPT_DIR%/core:/builder" -v "%cd%/%SRC_DIR%:/src" mstorsjo/llvm-mingw:20220802 /bin/bash /builder/builder-windows-arm64-make.sh
if errorlevel 1 (
  echo build failed.
  exit /b %errorlevel%
)

:: confirm
dir %MAKE_LIB%\dll\%LIBNAME%.dll
dir %MAKE_PROGRAMS%\%EXENAME%.exe

:: copy
mkdir %OUTPUT_DIR%\mingw
cp .\%MAKE_LIB%\dll\%LIBNAME%.dll .\%OUTPUT_DIR%\mingw\%LIBNAME%.dll
cp .\%MAKE_PROGRAMS%\%EXENAME%.exe .\%OUTPUT_DIR%\mingw\%EXENAME%.exe
