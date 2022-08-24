:: run on cmd
SETLOCAL ENABLEDELAYEDEXPANSION

call builder/zstd/settings.bat
set OS=linux
set PLATFORM=x64
if not defined OUTPUT_DIR (set OUTPUT_DIR=pkg\%SRC_DIR%\%GIT_VERSION%\%OS%\%PLATFORM%)

:: build
docker run --rm -v "%cd%/builder/%SRC_DIR%/core:/builder" -v "%cd%/%SRC_DIR%:/src" alpine:latest /bin/sh /builder/zstd-builder-linux-x64.sh

:: confirm
dir %SRC_DIR%\lib\%LIBNAME%.a
dir %SRC_DIR%\lib\%LIBNAME%.so*
dir %SRC_DIR%\%EXENAME%

:: copy
mkdir %OUTPUT_DIR%
cp .\%SRC_DIR%\lib\%LIBNAME%.a .\%OUTPUT_DIR%\%LIBNAME%.a
cp .\%SRC_DIR%\lib\%LIBNAME%.so.%FILE_VERSION% .\%OUTPUT_DIR%\%LIBNAME%.so
cp .\%SRC_DIR%\programs\%EXENAME% .\%OUTPUT_DIR%\%EXENAME%
