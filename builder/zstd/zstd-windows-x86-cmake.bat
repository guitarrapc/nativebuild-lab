:: run on cmd
SETLOCAL ENABLEDELAYEDEXPANSION

call builder/zstd/settings.bat
set OS=windows
set ARCH=win32
set PLATFORM=x86
if not defined OUTPUT_DIR (set OUTPUT_DIR=pkg\%SRC_DIR%\%GIT_VERSION%\%OS%\%PLATFORM%)

:: build
call builder\%SRC_DIR%\core\zstd-builder-windows-cmake.bat
if %ERRORLEVEL% == 1 exit /b 1

:: confirm
dir %SRC_DIR%\build\cmake\build\lib\Release\%EXENAME%*
dir %SRC_DIR%\build\cmake\build\programs\Release\%EXENAME%.exe

:: copy
mkdir %OUTPUT_DIR%
cp %SRC_DIR%\build\cmake\build\lib\Release\%EXENAME%.dll .\%OUTPUT_DIR%\%EXENAME%.dll
cp %SRC_DIR%\build\cmake\build\programs\Release\%EXENAME%.exe .\%OUTPUT_DIR%\%EXENAME%.exe
