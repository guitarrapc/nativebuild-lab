#!/bin/bash
set -e

SCRIPT_DIR=$(cd $(dirname $0); pwd)
source $SCRIPT_DIR/settings.sh
OS=darwin
PLATFORM=arm64
ARCH=ARM64
OUTPUT_DIR=${OUTPUT_DIR:=pkg/${SRC_DIR}/${GIT_VERSION}/${OS}/${PLATFORM}/}

# build
. $SCRIPT_DIR/core/builder-darwin.sh

# confirm
ls $SRC_DIR/cmake/build.dir/library/*.a
ls $SRC_DIR/cmake/build.dir/library/*.*dylib

# copy
mkdir -p "./${OUTPUT_DIR}/"
cp "./$SRC_DIR/cmake/build.dir/library/$LIBNAME_CRYPTO.a" "./${OUTPUT_DIR}/."
cp "./$SRC_DIR/cmake/build.dir/library/$LIBNAME_CRYPTO.${FILE_VERSION}.dylib" "./${OUTPUT_DIR}/$LIBNAME_CRYPTO.dylib"
cp "./$SRC_DIR/cmake/build.dir/library/$LIBNAME_TLS.a" "./${OUTPUT_DIR}/."
cp "./$SRC_DIR/cmake/build.dir/library/$LIBNAME_TLS.${FILE_VERSION}.dylib" "./${OUTPUT_DIR}/$LIBNAME_TLS.dylib"
cp "./$SRC_DIR/cmake/build.dir/library/$LIBNAME_X509.a" "./${OUTPUT_DIR}/."
cp "./$SRC_DIR/cmake/build.dir/library/$LIBNAME_X509.${FILE_VERSION}.dylib" "./${OUTPUT_DIR}/$LIBNAME_X509.dylib"
