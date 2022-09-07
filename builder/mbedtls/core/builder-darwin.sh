#!/bin/bash
set -eu

BUILD_DIR=$SRC_DIR/cmake/build.dir
rm -rf $BUILD_DIR
mkdir -p $BUILD_DIR

pushd $BUILD_DIR
  cmake -DCMAKE_BUILD_TYPE=Release -DMBEDTLS_TARGET_PREFIX="$PREFIX" -DUSE_SHARED_MBEDTLS_LIBRARY=On -DCMAKE_APPLE_SILICON_PROCESSOR="$ARCH" ../../
  cmake --build . --config Release --target "${PREFIX}mbedcrypto_static"
  cmake --build . --config Release --target "${PREFIX}mbedx509_static"
  cmake --build . --config Release --target "${PREFIX}mbedtls_static"
  cmake --build . --config Release --target "${PREFIX}mbedcrypto"
  cmake --build . --config Release --target "${PREFIX}mbedx509"
  cmake --build . --config Release --target "${PREFIX}mbedtls"
  cmake --build . --config Release
popd

# generate file test
if ! file "$(readlink -f ${BUILD_DIR}/library/lib${PREFIX}mbedcrypto.dylib)" | grep -i "$ARCH"; then
  echo "file generation arch not desired."
  exit 1
fi
