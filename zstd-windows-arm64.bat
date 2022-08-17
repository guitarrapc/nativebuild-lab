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

:: build
::docker run --rm -v "%cd%/builder/zstd:/builder" -v "%cd%/zstd:/src" ubuntu:22.04 /bin/bash /builder/zstd-builder-windows-x86.sh
::docker run --rm -v "%cd%/builder/zstd:/builder" -v "%cd%/zstd:/src" guitarrapc/ubuntu-mingw-w64:22.04.1 /bin/bash /builder/zstd-builder-windows-x86.sh
docker run -it --rm -v "%cd%/builder/zstd:/builder" -v "%cd%/zstd:/src" guitarrapc/ubuntu-mingw-w64:22.04.1

@REM :: confirm
@REM dir zstd\lib\dll\libzstd.dll
@REM dir zstd\programs\zstd.exe

@REM :: copy
@REM mkdir pkg\zstd\%GIT_ZSTD_VERSION%\%OS%\%PLATFORM%
@REM cp .\zstd\lib\dll\libzstd.dll .\pkg\zstd\%GIT_ZSTD_VERSION%\%OS%\%PLATFORM%\libzstd.dll
@REM cp .\zstd\programs\zstd.exe .\pkg\zstd\%GIT_ZSTD_VERSION%\%OS%\%PLATFORM%\zstd.exe
