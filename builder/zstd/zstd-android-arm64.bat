:: run on cmd
SETLOCAL ENABLEDELAYEDEXPANSION

call builder/zstd/settings.bat
set OS=android
set ABI=arm64-v8a
set PLATFORM=arm64
if not defined OUTPUT_DIR (set OUTPUT_DIR=pkg\%SRC_DIR%\%GIT_VERSION%\%OS%\%PLATFORM%)

:: build
docker run --rm -v "%cd%/builder/%SRC_DIR%/core:/builder" -v "%cd%/%SRC_DIR%:/src" -e "ABI=%ABI%" ubuntu:22.04 /bin/bash /builder/zstd-builder-android.sh

:: confirm
dir %SRC_DIR%\build\cmake\build\lib\%LIBNAME%.a
dir %SRC_DIR%\build\cmake\build\lib\%LIBNAME%.so
dir %SRC_DIR%\build\cmake\build\programs\%EXENAME%

:: copy
mkdir %OUTPUT_DIR%
cp .\%SRC_DIR%\build\cmake\build\lib\%LIBNAME%.a .\%OUTPUT_DIR%\%LIBNAME%.a
cp .\%SRC_DIR%\build\cmake\build\lib\%LIBNAME%.so .\%OUTPUT_DIR%\%LIBNAME%.so
cp .\%SRC_DIR%\build\cmake\build\programs\%EXENAME% .\%OUTPUT_DIR%\%EXENAME%
