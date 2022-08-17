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

:: build
set BASEDIR=%cd%
rmdir /S /Q %BASEDIR%\zstd\build\cmake\build
mkdir %BASEDIR%\zstd\build\cmake\build
cd %BASEDIR%/zstd/build/cmake/build
cmake -DCMAKE_BUILD_TYPE=Release -G "Visual Studio 17 2022" -A win32 ..
cmake --build . --config Release
cd %BASEDIR%

:: confirm
dir zstd\build\cmake\build\lib\Release\zstd*
dir zstd\build\cmake\build\programs\Release\zstd.exe

:: copy
mkdir pkg\zstd\%GIT_ZSTD_VERSION%\%OS%\%PLATFORM%
cp zstd\build\cmake\build\lib\Release\zstd.dll .\pkg\zstd\%GIT_ZSTD_VERSION%\%OS%\%PLATFORM%\zstd.dll
cp zstd\build\cmake\build\programs\Release\zstd.exe .\pkg\zstd\%GIT_ZSTD_VERSION%\%OS%\%PLATFORM%\zstd.exe
