#!/bin/bash
set -e

SCRIPT_DIR=$(cd $(dirname $0); pwd)
source $SCRIPT_DIR/settings.sh
OS=linux
PLATFORM=arm64
OUTPUT_DIR=${OUTPUT_DIR:=pkg/${SRC_DIR}/${GIT_VERSION}/${OS}/${PLATFORM}/}

# build
docker run --rm -v "$SCRIPT_DIR/core:/builder" -v "$PWD/$SRC_DIR:/src" -e "PREFIX=$PREFIX" ubuntu:22.04 /bin/sh /builder/builder-linux-arm64.sh

# confirm
ls $CMAKE_LIB/*.a
ls $CMAKE_LIB/*.so*

# copy
mkdir -p "./${OUTPUT_DIR}/"
cp "./$CMAKE_LIB/$LIBNAME.a" "./${OUTPUT_DIR}/."
cp "./$CMAKE_LIB/$LIBNAME.so" "./${OUTPUT_DIR}/$LIBNAME.so"
