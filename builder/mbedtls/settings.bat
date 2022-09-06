set SRC_DIR=mbedtls
set LIBNAME_CRYPTO=libmbedcrypto
set LIBNAME_TLS=libmbedtls
set LIBNAME_X509=libmbedx509
set WIN_LIBNAME_CRYPTO=mbedcrypto
set WIN_LIBNAME_TLS=mbedtls
set WIN_LIBNAME_X509=mbedx509

cd %SRC_DIR%
  FOR /F "tokens=* USEBACKQ" %%F IN (`git tag --points-at HEAD -l -n0 ^| findstr "^v"`) DO (SET VERSION=%%F)
  set GIT_VERSION=%VERSION%
  :: 'v1.5.2 ' -> '1.5.2'
  set FILE_VERSION=%VERSION:~1%
cd ..
