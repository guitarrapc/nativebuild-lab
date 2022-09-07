#!/bin/bash
set -e

SCRIPT_DIR=$(cd $(dirname $0); pwd)
source $SCRIPT_DIR/settings.sh
OS=android
ABI=armeabi-v7a
PLATFORM=arm
OUTPUT_DIR=${OUTPUT_DIR:=pkg/${SRC_DIR}/${GIT_VERSION}/${OS}/${PLATFORM}/}

# build
docker run --rm -v "$SCRIPT_DIR/core:/builder" -v "$PWD/$SRC_DIR:/src" -e "ABI=$ABI" -e "PREFIX=$PREFIX" ubuntu:22.04 /bin/bash /builder/builder-android.sh

# confirm
ls $CMAKE_LIB/*.a
ls $CMAKE_LIB/*.so*

# copy
mkdir -p "./${OUTPUT_DIR}/"
cp "$CMAKE_LIB/$LIBNAME_CRYPTO.a" "./${OUTPUT_DIR}/."
cp "$CMAKE_LIB/$LIBNAME_CRYPTO.so" "./${OUTPUT_DIR}/."
cp "$CMAKE_LIB/$LIBNAME_TLS.a" "./${OUTPUT_DIR}/."
cp "$CMAKE_LIB/$LIBNAME_TLS.so" "./${OUTPUT_DIR}/."
cp "$CMAKE_LIB/$LIBNAME_X509.a" "./${OUTPUT_DIR}/."
cp "$CMAKE_LIB/$LIBNAME_X509.so" "./${OUTPUT_DIR}/."
