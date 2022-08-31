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
if %ERRORLEVEL% == 1 exit /b 1

:: confirm
dir %SRC_DIR%\cmake\build.dir\library\Release\*.lib
dir %SRC_DIR%\cmake\build.dir\library\Release\*.dll

:: copy
mkdir %OUTPUT_DIR%
cp %SRC_DIR%\cmake\build.dir\library\Release\%WIN_LIBNAME_CRYPTO%.dll .\%OUTPUT_DIR%\%WIN_LIBNAME_CRYPTO%.dll
cp %SRC_DIR%\cmake\build.dir\library\Release\%WIN_LIBNAME_CRYPTO%.lib .\%OUTPUT_DIR%\%WIN_LIBNAME_CRYPTO%.lib
cp %SRC_DIR%\cmake\build.dir\library\Release\%WIN_LIBNAME_TLS%.dll .\%OUTPUT_DIR%\%WIN_LIBNAME_TLS%.dll
cp %SRC_DIR%\cmake\build.dir\library\Release\%WIN_LIBNAME_TLS%.lib .\%OUTPUT_DIR%\%WIN_LIBNAME_TLS%.lib
cp %SRC_DIR%\cmake\build.dir\library\Release\%WIN_LIBNAME_X509%.dll .\%OUTPUT_DIR%\%WIN_LIBNAME_X509%.dll
cp %SRC_DIR%\cmake\build.dir\library\Release\%WIN_LIBNAME_X509%.lib .\%OUTPUT_DIR%\%WIN_LIBNAME_X509%.lib
