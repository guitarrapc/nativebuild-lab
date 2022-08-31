#!/bin/bash
set -e

SCRIPT_DIR=$(cd $(dirname $0); pwd)
source $SCRIPT_DIR/settings.sh
OS=linux
PLATFORM=x64
OUTPUT_DIR=${OUTPUT_DIR:=pkg/${SRC_DIR}/${GIT_VERSION}/${OS}/${PLATFORM}/}

# build
docker run --rm -v "$SCRIPT_DIR/core:/builder" -v "$PWD/$SRC_DIR:/src" alpine:latest /bin/sh /builder/builder-linux-x64.sh

# confirm
ls $SRC_DIR/library/*.a
ls $SRC_DIR/library/*.so*

# copy
mkdir -p "./${OUTPUT_DIR}/"
cp "./$SRC_DIR/library/$LIBNAME_CRYPTO.a" "./${OUTPUT_DIR}/."
cp "./$SRC_DIR/library/$LIBNAME_CRYPTO.so.12" "./${OUTPUT_DIR}/$LIBNAME_CRYPTO.so"
cp "./$SRC_DIR/library/$LIBNAME_TLS.a" "./${OUTPUT_DIR}/."
cp "./$SRC_DIR/library/$LIBNAME_TLS.so.18" "./${OUTPUT_DIR}/$LIBNAME_TLS.so"
cp "./$SRC_DIR/library/$LIBNAME_X509.a" "./${OUTPUT_DIR}/."
cp "./$SRC_DIR/library/$LIBNAME_X509.so.4" "./${OUTPUT_DIR}/$LIBNAME_X509.so"
