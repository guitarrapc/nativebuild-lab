@echo OFF
SETLOCAL ENABLEDELAYEDEXPANSION

set SCRIPT_DIR=%~dp0
call %SCRIPT_DIR%\settings.bat
set OS=windows
set PLATFORM=x64
if not defined OUTPUT_DIR (set OUTPUT_DIR=pkg\%SRC_DIR%\%GIT_VERSION%\%OS%\%PLATFORM%)

:: build
::docker run --rm -v "%SCRIPT_DIR%/core:/builder" -v "%cd%/%SRC_DIR%:/src" ubuntu:22.04 /bin/bash /builder/builder-windows-x64-mingw.sh
docker run --rm -v "%SCRIPT_DIR%/core:/builder" -v "%cd%/%SRC_DIR%:/src" guitarrapc/ubuntu-mingw-w64:22.04.1 /bin/bash /builder/builder-windows-x64-mingw.sh

:: confirm
dir %SRC_DIR%\lib\dll\%LIBNAME%.dll
dir %SRC_DIR%\programs\%EXENAME%.exe

:: copy
mkdir %OUTPUT_DIR%\mingw
cp .\%SRC_DIR%\lib\dll\%LIBNAME%.dll .\%OUTPUT_DIR%\mingw\%LIBNAME%.dll
cp .\%SRC_DIR%\programs\%EXENAME%.exe .\%OUTPUT_DIR%\mingw\%EXENAME%.exe
