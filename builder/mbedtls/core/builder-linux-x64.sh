#!/bin/sh
set -eu

apk --no-cache add make gcc libc-dev cmake
apk --no-cache add python3 perl py3-pip
pip3 install jinja2

SRC_DIR="/src"
BUILD_DIR=$SRC_DIR/cmake/build.dir
rm -rf $BUILD_DIR
mkdir -p $BUILD_DIR

cd $BUILD_DIR
  cmake -DCMAKE_BUILD_TYPE=Release -DMBEDTLS_TARGET_PREFIX="$PREFIX" -DUSE_SHARED_MBEDTLS_LIBRARY=On ../../
  cmake --build . --config Release --target "${PREFIX}mbedcrypto_static"
  cmake --build . --config Release --target "${PREFIX}mbedx509_static"
  cmake --build . --config Release --target "${PREFIX}mbedtls_static"
  cmake --build . --config Release --target "${PREFIX}mbedcrypto"
  cmake --build . --config Release --target "${PREFIX}mbedx509"
  cmake --build . --config Release --target "${PREFIX}mbedtls"
  cmake --build . --config Release
