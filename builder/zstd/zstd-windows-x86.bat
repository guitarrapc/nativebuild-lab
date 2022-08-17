:: run on cmd
SETLOCAL ENABLEDELAYEDEXPANSION
cd zstd
FOR /F "tokens=* USEBACKQ" %%F IN (`git tag --points-at HEAD`) DO ( SET ZSTD_VERSION=%%F )
cd ..
:: 'v1.5.2 ' -> 'v1.5.2'
set GIT_ZSTD_VERSION=%ZSTD_VERSION:~0,-1%
:: 'v1.5.2 ' -> '1.5.2'
set FILE_ZSTD_VERSION=%ZSTD_VERSION:~1,-1%
set OS=windows
set PLATFORM=x86
if not defined OUTPUT_DIR (set OUTPUT_DIR=pkg\zstd\%GIT_ZSTD_VERSION%\%OS%\%PLATFORM%)

:: build
::docker run --rm -v "%cd%/builder/zstd/docker:/builder" -v "%cd%/zstd:/src" ubuntu:22.04 /bin/bash /builder/zstd-builder-windows-x86.sh
docker run --rm -v "%cd%/builder/zstd/docker:/builder" -v "%cd%/zstd:/src" guitarrapc/ubuntu-mingw-w64:22.04.1 /bin/bash /builder/zstd-builder-windows-x86.sh

:: confirm
dir zstd\lib\dll\libzstd.dll
dir zstd\programs\zstd.exe

:: copy
mkdir %OUTPUT_DIR%\mingw\
cp .\zstd\lib\dll\libzstd.dll .\%OUTPUT_DIR%\mingw\libzstd.dll
cp .\zstd\programs\zstd.exe .\%OUTPUT_DIR%\mingw\zstd.exe
