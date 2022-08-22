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
set PLATFORM=arm64
if not defined OUTPUT_DIR (set OUTPUT_DIR=pkg\zstd\%GIT_ZSTD_VERSION%\%OS%\%PLATFORM%)

:: build
docker run -it --rm -v "%cd%/builder/zstd/core:/builder" -v "%cd%/zstd:/src" ubuntu:22.04

@REM :: confirm
@REM dir zstd\lib\libzstd.a
@REM dir zstd\lib\libzstd.so*

@REM :: copy
@REM mkdir %OUTPUT_DIR%
@REM cp .\zstd\lib\libzstd.a .\%OUTPUT_DIR%\libzstd.a
@REM cp .\zstd\lib\libzstd.so.%FILE_ZSTD_VERSION% .\%OUTPUT_DIR%\libzstd.so
@REM cp .\zstd\programs\zstd .\%OUTPUT_DIR%\zstd
