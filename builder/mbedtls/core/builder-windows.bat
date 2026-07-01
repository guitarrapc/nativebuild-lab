:: ARM64, x64, win32
if not defined ARCH (
  echo Please define 'ARCH' to build.
  exit /b 1
)

:: create directory
set BUILD_DIR=%SRC_DIR%\cmake\build.dir
rmdir /S /Q %BUILD_DIR%
mkdir %BUILD_DIR%

:: pre-generate source files because GEN_FILES defaults to OFF on Windows hosts
pushd %SRC_DIR%
  set CC=cl
  set HOSTCC=cl
  python -m pip install -r scripts\basic.requirements.txt
  if errorlevel 1 (
    popd
    exit /b %errorlevel%
  )
  call scripts\make_generated_files.bat
  if errorlevel 1 (
    popd
    exit /b %errorlevel%
  )
popd

:: build
pushd %BUILD_DIR%
  cmake -DCMAKE_BUILD_TYPE=Release -DMBEDTLS_TARGET_PREFIX="%PREFIX%" -DUSE_SHARED_MBEDTLS_LIBRARY=On -G "Visual Studio 17 2022" -A %ARCH% ..\..\
  cmake --build . --config Release
popd
