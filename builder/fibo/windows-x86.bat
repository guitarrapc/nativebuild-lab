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
if errorlevel 1 (
  echo build failed.
  exit /b %errorlevel%
)

:: confirm
dir %CMAKE_LIB%\Release\*.lib
dir %CMAKE_LIB%\Release\*.dll
dir %CMAKE_LIB%\Release\*.exe

:: copy
mkdir %OUTPUT_DIR%
cp %CMAKE_LIB%\Release\%WIN_LIBNAME%.dll .\%OUTPUT_DIR%\%WIN_LIBNAME%.dll
cp %CMAKE_LIB%\Release\%WIN_LIBNAME%.lib .\%OUTPUT_DIR%\%WIN_LIBNAME%.lib
cp %CMAKE_LIB%\Release\%WIN_LIBNAME%cli.exe .\%OUTPUT_DIR%\%WIN_LIBNAME%cli.exe
