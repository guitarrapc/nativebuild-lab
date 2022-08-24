#!/bin/bash

set -e

source ./builder/zstd/settings.sh
OS=ios
PLATFORM=arm64
OUTPUT_DIR=${OUTPUT_DIR:=pkg/${SRC_DIR}/${GIT_VERSION}/${OS}/${PLATFORM}/}

# build
bash ./builder/$SRC_DIR/core/zstd-builder-ios.sh

# confirm
ls -l $SRC_DIR/build/cmake/build/lib/$LIBNAME.a
ls -l $SRC_DIR/build/cmake/build/lib/$LIBNAME.*dylib

# copy
mkdir -p "./${OUTPUT_DIR}/"
cp $SRC_DIR/build/cmake/build/lib/$LIBNAME.a "./${OUTPUT_DIR}/."
cp "$SRC_DIR/build/cmake/build/lib/$LIBNAME.${FILE_VERSION}.dylib" "./${OUTPUT_DIR}/$LIBNAME.dylib"
