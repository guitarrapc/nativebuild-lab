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
set PLATFORM=arm64
if not defined OUTPUT_BASE (set OUTPUT_BASE=pkg)

:: build
docker run --rm -v "%cd%/builder/zstd:/builder" -v "%cd%/zstd:/src" mstorsjo/llvm-mingw:20220802 /bin/bash /builder/zstd-builder-windows-arm64.sh

:: confirm
dir zstd\lib\dll\libzstd.dll
dir zstd\programs\zstd.exe

:: copy
mkdir %OUTPUT_BASE%\zstd\%GIT_ZSTD_VERSION%\%OS%\%PLATFORM%\mingw
cp .\zstd\lib\dll\libzstd.dll .\%OUTPUT_BASE%\zstd\%GIT_ZSTD_VERSION%\%OS%\%PLATFORM%\mingw\libzstd.dll
cp .\zstd\programs\zstd.exe .\%OUTPUT_BASE%\zstd\%GIT_ZSTD_VERSION%\%OS%\%PLATFORM%\mingw\zstd.exe
