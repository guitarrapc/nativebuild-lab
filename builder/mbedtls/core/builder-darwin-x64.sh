#!/bin/bash
set -eu

BUILD_DIR=$SRC_DIR/cmake/build.dir
rm -rf $BUILD_DIR
mkdir -p $BUILD_DIR

cd $BUILD_DIR
  cmake -DCMAKE_BUILD_TYPE=Release -DUSE_SHARED_MBEDTLS_LIBRARY=On -DCMAKE_APPLE_SILICON_PROCESSOR=x86_64 ../../
  cmake --build . --config Release --target mbedtls_static
  cmake --build . --config Release --target mbedtls
  cmake --build . --config Release
cd ..
