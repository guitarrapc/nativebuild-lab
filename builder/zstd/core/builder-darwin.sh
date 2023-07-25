#!/bin/bash
set -eu

BUILD_DIR=$SRC_DIR/build/cmake/build
rm -rf $BUILD_DIR
mkdir -p $BUILD_DIR

pushd $BUILD_DIR
  cmake -DCMAKE_BUILD_TYPE=Release -DCMAKE_OSX_ARCHITECTURES="$ARCH" ..
  cmake --build . --config Release
popd

# generate file test
if ! file "${BUILD_DIR}/lib/$(readlink ${BUILD_DIR}/lib/libzstd.dylib)" | grep -i "$ARCH"; then
  file "${BUILD_DIR}/lib/$(readlink ${BUILD_DIR}/lib/libzstd.dylib)"
  echo "file generation arch not desired."
  exit 1
fi
