#!/bin/bash
set -e

SCRIPT_DIR=$(cd $(dirname $0); pwd)
source $SCRIPT_DIR/settings.sh
OS=darwin
PLATFORM=arm64
TARGET=arm64-apple-macos11
ARCH=arm64
OUTPUT_DIR=${OUTPUT_DIR:=pkg/${SRC_DIR}/${GIT_VERSION}/${OS}/${PLATFORM}/}

# build
. $SCRIPT_DIR/core/builder-darwin.sh

# confirm
ls $MAKE_LIB/$LIBNAME.a
ls $MAKE_LIB/$LIBNAME.*dylib
ls $MAKE_PROGRAMS/$EXENAME

# copy
mkdir -p "./${OUTPUT_DIR}/"
cp ./$MAKE_LIB/$LIBNAME.a "./${OUTPUT_DIR}/."
cp "./$MAKE_LIB/$LIBNAME.${FILE_VERSION}.dylib" "./${OUTPUT_DIR}/$LIBNAME.dylib"
cp ./$MAKE_PROGRAMS/$EXENAME "./${OUTPUT_DIR}/."
