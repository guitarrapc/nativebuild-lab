#!/bin/bash
set -e

SCRIPT_DIR=$(cd $(dirname $0); pwd)
source $SCRIPT_DIR/settings.sh
OS=darwin
PLATFORM=x64
ARCH=x86_64
TARGET=x86_64-apple-macos12.6
OUTPUT_DIR=${OUTPUT_DIR:=pkg/${SRC_DIR}/${GIT_VERSION}/${OS}/${PLATFORM}/}

# build
. $SCRIPT_DIR/core/builder-darwin.sh

# confirm
ls $CMAKE_LIB/$LIBNAME.a
ls $CMAKE_LIB/$LIBNAME.*dylib
ls $CMAKE_PROGRAMS/$EXENAME

# copy
mkdir -p "./${OUTPUT_DIR}/"
cp ./$CMAKE_LIB/$LIBNAME.a "./${OUTPUT_DIR}/."
cp "./$CMAKE_LIB/$LIBNAME.${FILE_VERSION}.dylib" "./${OUTPUT_DIR}/$LIBNAME.dylib"
cp ./$CMAKE_PROGRAMS/$EXENAME "./${OUTPUT_DIR}/."
