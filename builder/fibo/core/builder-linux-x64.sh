#!/bin/sh
set -eux

apk --no-cache add make gcc libc-dev cmake file

SRC_DIR="/src"
BUILD_DIR=$SRC_DIR/build/cmake/build
rm -rf $BUILD_DIR
mkdir -p $BUILD_DIR

cd $BUILD_DIR
  cmake ..
  cmake --build . --config Release

# generate file test
targetFile="$BUILD_DIR/${PREFIX}libfibo.so"
if [ -L "${targetFile}" ]; then
  if ! file "$(readlink -f "${targetFile}")" | grep "x86-64"; then
    file "$(readlink -f "${targetFile}")"
    echo "file generation arch not desired."
    exit 1
  fi
else
  if ! file "${targetFile}" | grep "x86-64"; then
    file "${targetFile}"
    echo "file generation arch not desired."
    exit 1
  fi
fi
