#!/bin/bash
set -e

SCRIPT_DIR=$(cd $(dirname $0); pwd)
source $SCRIPT_DIR/settings.sh
OS=android
ABI=arm64-v8a
PLATFORM=arm64
OUTPUT_DIR=${OUTPUT_DIR:=pkg/${SRC_DIR}/${GIT_VERSION}/${OS}/${PLATFORM}/}

# build
docker run --rm -v "$SCRIPT_DIR/core:/builder" -v "$PWD/$SRC_DIR:/src" -e "ABI=$ABI" -e "PREFIX=$PREFIX" ubuntu:22.04 /bin/bash /builder/builder-android.sh

# confirm
ls $SRC_DIR/cmake/build.dir/library/$LIBNAME_CRYPTO.a
ls $SRC_DIR/cmake/build.dir/library/$LIBNAME_CRYPTO.so*
ls $SRC_DIR/cmake/build.dir/library/$LIBNAME_TLS.a
ls $SRC_DIR/cmake/build.dir/library/$LIBNAME_TLS.so*
ls $SRC_DIR/cmake/build.dir/library/$LIBNAME_X509.a
ls $SRC_DIR/cmake/build.dir/library/$LIBNAME_X509.so*

# copy
mkdir -p "./${OUTPUT_DIR}/"
cp ./$SRC_DIR/cmake/build.dir/library/$LIBNAME_CRYPTO.a "./${OUTPUT_DIR}/."
cp ./$SRC_DIR/cmake/build.dir/library/$LIBNAME_CRYPTO.so "./${OUTPUT_DIR}/."
cp ./$SRC_DIR/cmake/build.dir/library/$LIBNAME_TLS.a "./${OUTPUT_DIR}/."
cp ./$SRC_DIR/cmake/build.dir/library/$LIBNAME_TLS.so "./${OUTPUT_DIR}/."
cp ./$SRC_DIR/cmake/build.dir/library/$LIBNAME_X509.a "./${OUTPUT_DIR}/."
cp ./$SRC_DIR/cmake/build.dir/library/$LIBNAME_X509.so "./${OUTPUT_DIR}/."
