:: run on cmd
SETLOCAL ENABLEDELAYEDEXPANSION

call builder/zstd/settings.bat
set OS=windows
set PLATFORM=arm64
if not defined OUTPUT_DIR (set OUTPUT_DIR=pkg\%SRC_DIR%\%GIT_VERSION%\%OS%\%PLATFORM%)

:: build
docker run --rm -v "%cd%/builder/%SRC_DIR%/core:/builder" -v "%cd%/%SRC_DIR%:/src" mstorsjo/llvm-mingw:20220802 /bin/bash /builder/zstd-builder-windows-arm64.sh

:: confirm
dir %SRC_DIR%\lib\dll\%LIBNAME%.dll
dir %SRC_DIR%\programs\%EXENAME%.exe

:: copy
mkdir %OUTPUT_DIR%\mingw
cp .\%SRC_DIR%\lib\dll\%LIBNAME%.dll .\%OUTPUT_DIR%\mingw\%LIBNAME%.dll
cp .\%SRC_DIR%\programs\%EXENAME%.exe .\%OUTPUT_DIR%\mingw\%EXENAME%.exe
