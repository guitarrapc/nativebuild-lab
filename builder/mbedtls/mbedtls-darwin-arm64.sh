#!/bin/bash
set -e

SCRIPT_DIR=$(cd $(dirname $0); pwd)
source $SCRIPT_DIR/settings.sh
OS=darwin
PLATFORM=arm64
OUTPUT_DIR=${OUTPUT_DIR:=pkg/${SRC_DIR}/${GIT_VERSION}/${OS}/${PLATFORM}/}

# build
. $SCRIPT_DIR/core/builder-darwin-arm64.sh

# confirm
ls $SRC_DIR/library/*.a
ls $SRC_DIR/library/*.*dylib

# copy
mkdir -p "./${OUTPUT_DIR}/"
cp "./$SRC_DIR/library/$LIBNAME_CRYPTO.a" "./${OUTPUT_DIR}/."
cp "./$SRC_DIR/library/$LIBNAME_CRYPTO.dylib" "./${OUTPUT_DIR}/$LIBNAME_CRYPTO.dylib"
cp "./$SRC_DIR/library/$LIBNAME_TLS.a" "./${OUTPUT_DIR}/."
cp "./$SRC_DIR/library/$LIBNAME_TLS.dylib" "./${OUTPUT_DIR}/$LIBNAME_TLS.dylib"
cp "./$SRC_DIR/library/$LIBNAME_X509.a" "./${OUTPUT_DIR}/."
cp "./$SRC_DIR/library/$LIBNAME_X509.dylib" "./${OUTPUT_DIR}/$LIBNAME_X509.dylib"
