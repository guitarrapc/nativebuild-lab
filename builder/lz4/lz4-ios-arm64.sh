#!/bin/bash
set -e

SCRIPT_DIR=$(cd $(dirname $0); pwd)
source $SCRIPT_DIR/settings.sh
OS=ios
PLATFORM=arm64
OUTPUT_DIR=${OUTPUT_DIR:=pkg/${SRC_DIR}/${GIT_VERSION}/${OS}/${PLATFORM}/}

# build
. ./builder/$SRC_DIR/core/builder-ios.sh

# confirm
ls -l $MAKE_LIB/$LIBNAME.a
ls -l $MAKE_LIB/$LIBNAME.*dylib

# copy
mkdir -p "./${OUTPUT_DIR}/"
cp $MAKE_LIB/$LIBNAME.a "./${OUTPUT_DIR}/."
cp "$MAKE_LIB/$LIBNAME.${FILE_VERSION}.dylib" "./${OUTPUT_DIR}/$LIBNAME.dylib"
