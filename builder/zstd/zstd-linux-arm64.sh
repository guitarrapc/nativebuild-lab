#!/bin/bash
set -e

SCRIPT_DIR=$(cd $(dirname $0); pwd)
source ./$SCRIPT_DIR/settings.sh
OS=linux
PLATFORM=arm64
OUTPUT_DIR=${OUTPUT_DIR:=pkg/${SRC_DIR}/${GIT_VERSION}/${OS}/${PLATFORM}/}

# build
docker run --rm -v "$SCRIPT_DIR/core:/builder" -v "$PWD/$SRC_DIR:/src" ubuntu:22.04 /bin/sh /builder/builder-linux-arm64.sh

# confirm
ls $SRC_DIR/lib/$LIBNAME.a
ls $SRC_DIR/lib/$LIBNAME.so*
ls $SRC_DIR/programs/$EXENAME

# copy
mkdir -p "./${OUTPUT_DIR}/"
cp ./$SRC_DIR/lib/$LIBNAME.a "./${OUTPUT_DIR}/."
cp "./$SRC_DIR/lib/$LIBNAME.so.${FILE_VERSION}" "./${OUTPUT_DIR}/$LIBNAME.so"
cp ./$SRC_DIR/programs/$EXENAME "./${OUTPUT_DIR}/."
