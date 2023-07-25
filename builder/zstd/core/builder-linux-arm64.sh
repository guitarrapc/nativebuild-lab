#!/bin/bash
set -eu

apt update
apt install -yq --no-install-suggests --no-install-recommends make gcc libc-dev cmake file
apt install -yq --no-install-suggests --no-install-recommends g++-aarch64-linux-gnu gcc-aarch64-linux-gnu

SRC_DIR="/src"
BUILD_DIR=$SRC_DIR/build/cmake/build
rm -rf $BUILD_DIR
mkdir -p $BUILD_DIR

CMAKE_TOOLCHAIN=$BUILD_DIR/toolchain.cmake
cat <<EOF | tee "${CMAKE_TOOLCHAIN}"
  set(CMAKE_SYSTEM_NAME "Linux")
  set(CMAKE_C_COMPILER "/usr/bin/aarch64-linux-gnu-gcc")
  set(CMAKE_CXX_COMPILER "/usr/bin/aarch64-linux-gnu-g++")
  set(CMAKE_SYSTEM_PROCESSOR "aarch64")
EOF

pushd $BUILD_DIR
  cmake -DCMAKE_BUILD_TYPE=Release -DCMAKE_TOOLCHAIN_FILE="${CMAKE_TOOLCHAIN}" ..
  cmake --build . --config Release
popd

# generate file test
if ! file "$(readlink -f $BUILD_DIR/lib/libzstd.so)" | grep "ARM aarch64"; then
  file "$(readlink -f $BUILD_DIR/lib/libzstd.so)"
  echo "file generation arch not desired."
  exit 1
fi
