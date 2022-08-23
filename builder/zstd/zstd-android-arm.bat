:: run on cmd
SETLOCAL ENABLEDELAYEDEXPANSION
cd zstd
FOR /F "tokens=* USEBACKQ" %%F IN (`git tag --points-at HEAD`) DO ( SET ZSTD_VERSION=%%F )
cd ..
:: 'v1.5.2 ' -> 'v1.5.2'
set GIT_ZSTD_VERSION=%ZSTD_VERSION:~0,-1%
:: 'v1.5.2 ' -> '1.5.2'
set FILE_ZSTD_VERSION=%ZSTD_VERSION:~1,-1%
set OS=android
set ABI=armeabi-v7a
set PLATFORM=arm
if not defined OUTPUT_DIR (set OUTPUT_DIR=pkg\zstd\%GIT_ZSTD_VERSION%\%OS%\%PLATFORM%)

:: build
docker run --rm -v "%cd%/builder/zstd/core:/builder" -v "%cd%/zstd:/src" -e "ABI=%ABI%" ubuntu:22.04 /bin/bash /builder/zstd-builder-android.sh

:: confirm
dir zstd\build\cmake\build\lib\libzstd.a
dir zstd\build\cmake\build\lib\libzstd.so
dir zstd\build\cmake\build\programs\zstd

:: copy
mkdir %OUTPUT_DIR%
cp .\zstd\build\cmake\build\lib\libzstd.a .\%OUTPUT_DIR%\libzstd.a
cp .\zstd\build\cmake\build\lib\libzstd.so .\%OUTPUT_DIR%\libzstd.so
cp .\zstd\build\cmake\build\programs\zstd .\%OUTPUT_DIR%\zstd
