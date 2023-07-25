@echo OFF
SETLOCAL ENABLEDELAYEDEXPANSION

set SCRIPT_DIR=%~dp0
call %SCRIPT_DIR%\settings.bat
set OS=windows
set ARCH=x64
set PLATFORM=x64
if not defined OUTPUT_DIR (set OUTPUT_DIR=pkg\%SRC_DIR%\%GIT_VERSION%\%OS%\%PLATFORM%)

:: build
call %SCRIPT_DIR%\core\builder-windows.bat
if errorlevel 1 (
  echo build failed.
  exit /b %errorlevel%
)

:: confirm
dir %CMAKE_LIB%\%EXENAME%*
dir %CMAKE_LIB%\%EXENAME%.exe

:: copy
mkdir %OUTPUT_DIR%
cp %CMAKE_LIB%\%EXENAME%.dll .\%OUTPUT_DIR%\%EXENAME%.dll
cp %CMAKE_LIB%\%EXENAME%.exe .\%OUTPUT_DIR%\%EXENAME%.exe
