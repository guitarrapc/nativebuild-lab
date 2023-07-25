#!/bin/sh
set -eu

apt update
apt install -yq --no-install-suggests --no-install-recommends make gcc libc-dev cmake file
apt install -yq --no-install-suggests --no-install-recommends python3 perl python3-pip
pip3 install jinja2

SRC_DIR="/src"
BUILD_DIR=$SRC_DIR/cmake/build.dir
rm -rf $BUILD_DIR
mkdir -p $BUILD_DIR

CMAKE_TOOLCHAIN=$BUILD_DIR/toolchain.cmake
cat <<EOF | tee "${CMAKE_TOOLCHAIN}"
  set(CMAKE_SYSTEM_NAME "Linux")
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

# generate file test
if ! file "$(readlink -f $BUILD_DIR/library/lib${PREFIX}mbedcrypto.so)" | grep "x86-64"; then
  file "$(readlink -f $BUILD_DIR/library/lib${PREFIX}mbedcrypto.so)"
  echo "file generation arch not desired."
  exit 1
fi
