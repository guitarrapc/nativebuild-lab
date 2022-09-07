#!/bin/bash
set -eu

apt-get update
apt-get install -yq --no-install-suggests --no-install-recommends make gcc libc-dev cmake
apt-get install -yq --no-install-suggests --no-install-recommends python3 perl python3-pip
apt-get install -yq --no-install-suggests --no-install-recommends gcc-aarch64-linux-gnu g++-aarch64-linux-gnu
pip3 install jinja2

SRC_DIR="/src"
BUILD_DIR=$SRC_DIR/cmake/build.dir
rm -rf $BUILD_DIR
mkdir -p $BUILD_DIR

CMAKE_TOOLCHAIN=$BUILD_DIR/toolchain.cmake
cat <<EOF | tee "${CMAKE_TOOLCHAIN}"
  set(CMAKE_SYSTEM_NAME "Linux")
  set(CMAKE_C_COMPILER /usr/bin/aarch64-linux-gnu-gcc)
  set(CMAKE_SYSTEM_PROCESSOR "aarch64")
  set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY Release)
  set(CMAKE_LIBRARY_OUTPUT_DIRECTORY Release)
  set(CMAKE_RUNTIME_OUTPUT_DIRECTORY Release)
EOF

cd $BUILD_DIR
  cmake -DCMAKE_BUILD_TYPE=Release -DMBEDTLS_TARGET_PREFIX="$PREFIX" -DUSE_SHARED_MBEDTLS_LIBRARY=On -DCMAKE_TOOLCHAIN_FILE="$CMAKE_TOOLCHAIN" ../../
  cmake --build . --config Release --target "${PREFIX}mbedcrypto_static"
  cmake --build . --config Release --target "${PREFIX}mbedx509_static"
  cmake --build . --config Release --target "${PREFIX}mbedtls_static"
  cmake --build . --config Release --target "${PREFIX}mbedcrypto"
  cmake --build . --config Release --target "${PREFIX}mbedx509"
  cmake --build . --config Release --target "${PREFIX}mbedtls"
  cmake --build . --config Release
