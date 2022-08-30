#!/bin/bash
set -e

SCRIPT_DIR=$(cd $(dirname $0); pwd)
source $SCRIPT_DIR/settings.sh
OS=linux
PLATFORM=arm64
OUTPUT_DIR=${OUTPUT_DIR:=pkg/${SRC_DIR}/${GIT_VERSION}/${OS}/${PLATFORM}/}

# build
docker run --rm -v "$SCRIPT_DIR/core:/builder" -v "$PWD/$SRC_DIR:/src" ubuntu:22.04 /bin/sh /builder/builder-linux-arm64.sh

# confirm
ls $SRC_DIR/lib/*.a
ls $SRC_DIR/lib/*.so*

# copy
mkdir -p "./${OUTPUT_DIR}/"
cp "./$SRC_DIR/lib/$LIBNAME_CRYPTO.a" "./${OUTPUT_DIR}/."
cp "./$SRC_DIR/lib/$LIBNAME_CRYPTO.so.12" "./${OUTPUT_DIR}/$LIBNAME_CRYPTO.so"
cp "./$SRC_DIR/lib/$LIBNAME_TLS.a" "./${OUTPUT_DIR}/."
cp "./$SRC_DIR/lib/$LIBNAME_TLS.so.18" "./${OUTPUT_DIR}/$LIBNAME_TLS.so"
cp "./$SRC_DIR/lib/$LIBNAME_X509.a" "./${OUTPUT_DIR}/."
cp "./$SRC_DIR/lib/$LIBNAME_X509.so.4" "./${OUTPUT_DIR}/$LIBNAME_X509.so"
