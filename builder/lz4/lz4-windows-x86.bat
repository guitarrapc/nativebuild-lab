@echo OFF
SETLOCAL ENABLEDELAYEDEXPANSION

set SCRIPT_DIR=%~dp0
call %SCRIPT_DIR%\settings.bat
set OS=windows
set ARCH=win32
set PLATFORM=x86
if not defined OUTPUT_DIR (set OUTPUT_DIR=pkg\%SRC_DIR%\%GIT_VERSION%\%OS%\%PLATFORM%)

:: build
call %SCRIPT_DIR%\core\builder-windows.bat
if %ERRORLEVEL% == 1 exit /b 1

:: confirm
dir %SRC_DIR%\build\cmake\build\Release\%EXENAME%*
dir %SRC_DIR%\build\cmake\build\Release\%EXENAME%.exe

:: copy
mkdir %OUTPUT_DIR%
cp %SRC_DIR%\build\cmake\build\Release\%EXENAME%.dll .\%OUTPUT_DIR%\%EXENAME%.dll
cp %SRC_DIR%\build\cmake\build\Release\%EXENAME%.exe .\%OUTPUT_DIR%\%EXENAME%.exe
