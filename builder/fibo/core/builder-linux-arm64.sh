#!/bin/bash
set -eux

apt-get update
apt-get install -yq --no-install-suggests --no-install-recommends make gcc libc-dev cmake file
apt-get install -yq --no-install-suggests --no-install-recommends gcc-aarch64-linux-gnu g++-aarch64-linux-gnu

SRC_DIR="/src"
BUILD_DIR=$SRC_DIR/build/cmake/build
rm -rf $BUILD_DIR
mkdir -p $BUILD_DIR

CMAKE_TOOLCHAIN=$BUILD_DIR/toolchain.cmake
cat <<EOF | tee "${CMAKE_TOOLCHAIN}"
  set(CMAKE_SYSTEM_NAME "Linux")
  set(CMAKE_C_COMPILER /usr/bin/aarch64-linux-gnu-gcc)
  set(CMAKE_SYSTEM_PROCESSOR "aarch64")
EOF

cd $BUILD_DIR
  cmake -DCMAKE_TOOLCHAIN_FILE="$CMAKE_TOOLCHAIN" ..
  cmake --build . --config Release

# generate file test
targetFile="$BUILD_DIR/${PREFIX}libfibo.so"
if [ -L "${targetFile}" ]; then
  if ! file "$(readlink -f "${targetFile}")" | grep "ARM aarch64"; then
    file "$(readlink -f "${targetFile}")"
    echo "file generation arch not desired."
    exit 1
  fi
else
  if ! file "${targetFile}" | grep "ARM aarch64"; then
    file "${targetFile}"
    echo "file generation arch not desired."
    exit 1
  fi
fi
