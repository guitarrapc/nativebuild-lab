#!/bin/bash
set -e

source ./builder/zstd/settings.sh
OS=android
ABI=armeabi-v7a
PLATFORM=arm
OUTPUT_DIR=${OUTPUT_DIR:=pkg/${SRC_DIR}/${GIT_VERSION}/${OS}/${PLATFORM}/}

# build
docker run --rm -v "$PWD/builder/$SRC_DIR/core:/builder" -v "$PWD/$SRC_DIR:/src" -e "ABI=$ABI" ubuntu:22.04 /bin/bash /builder/zstd-builder-android.sh

# confirm
ls $SRC_DIR/build/cmake/build/lib/$LIBNAME.a
ls $SRC_DIR/build/cmake/build/lib/$LIBNAME.so*
ls $SRC_DIR/build/cmake/build/programs/$EXENAME

# copy
mkdir -p "./${OUTPUT_DIR}/"
cp ./$SRC_DIR/build/cmake/build/lib/$LIBNAME.a "./${OUTPUT_DIR}/."
cp "./$SRC_DIR/build/cmake/build/lib/$LIBNAME.so" "./${OUTPUT_DIR}/$LIBNAME.so"
cp ./$SRC_DIR/build/cmake/build/programs/$EXENAME "./${OUTPUT_DIR}/."
