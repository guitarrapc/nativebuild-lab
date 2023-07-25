#!/bin/bash
set -eu

apt update
apt install -yq --no-install-suggests --no-install-recommends make gcc libc-dev cmake file
apt install -yq --no-install-suggests --no-install-recommends build-essential # fix `No CMAKE_CXX_COMPILER could be found.`

SRC_DIR="/src"
BUILD_DIR=$SRC_DIR/build/cmake/build
rm -rf $BUILD_DIR
mkdir -p $BUILD_DIR

CMAKE_TOOLCHAIN=$BUILD_DIR/toolchain.cmake
cat <<EOF | tee "${CMAKE_TOOLCHAIN}"
  set(CMAKE_SYSTEM_NAME "Linux")
EOF

pushd $BUILD_DIR
  cmake -DCMAKE_BUILD_TYPE=Release -DCMAKE_TOOLCHAIN_FILE="${CMAKE_TOOLCHAIN}" ..
  cmake --build . --config Release
popd

# generate file test
if ! file "$(readlink -f $BUILD_DIR/lib/libzstd.so)" | grep "x86-64"; then
  file "$(readlink -f $BUILD_DIR/lib/libzstd.so)"
  echo "file generation arch not desired."
  exit 1
fi
