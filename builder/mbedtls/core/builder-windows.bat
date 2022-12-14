:: ARM64, x64, win32
if not defined ARCH (
  echo Please define 'ARCH' to build.
  exit /b 1
)

:: create directory
set BUILD_DIR=%SRC_DIR%\cmake\build.dir
rmdir /S /Q %BUILD_DIR%
mkdir %BUILD_DIR%

:: build
pushd %BUILD_DIR%
  cmake -DCMAKE_BUILD_TYPE=Release -DMBEDTLS_TARGET_PREFIX="%PREFIX%" -DUSE_SHARED_MBEDTLS_LIBRARY=On -G "Visual Studio 17 2022" -A %ARCH% ..\..\
  cmake --build . --config Release --target "%PREFIX%mbedcrypto_static"
  cmake --build . --config Release --target "%PREFIX%mbedx509_static"
  cmake --build . --config Release --target "%PREFIX%mbedtls_static"
  cmake --build . --config Release --target "%PREFIX%mbedcrypto"
  cmake --build . --config Release --target "%PREFIX%mbedx509"
  cmake --build . --config Release --target "%PREFIX%mbedtls"
  cmake --build . --config Release
popd
