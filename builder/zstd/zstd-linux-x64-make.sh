#!/bin/bash
set -e

SCRIPT_DIR=$(cd $(dirname $0); pwd)
source $SCRIPT_DIR/settings.sh
OS=linux
PLATFORM=x64
OUTPUT_DIR=${OUTPUT_DIR:=pkg/${SRC_DIR}/${GIT_VERSION}/${OS}/${PLATFORM}/}

# build
docker run --rm -v "$SCRIPT_DIR/core:/builder" -v "$PWD/$SRC_DIR:/src" alpine:latest /bin/sh /builder/builder-linux-x64-make.sh

# confirm
ls $MAKE_LIB/$LIBNAME.a
ls $MAKE_LIB/$LIBNAME.so*
ls $MAKE_PROGRAMS/$EXENAME

# copy
mkdir -p "./${OUTPUT_DIR}/"
cp ./$MAKE_LIB/$LIBNAME.a "./${OUTPUT_DIR}/."
cp "./$MAKE_LIB/$LIBNAME.so.${FILE_VERSION}" "./${OUTPUT_DIR}/$LIBNAME.so"
cp ./$MAKE_PROGRAMS/$EXENAME "./${OUTPUT_DIR}/."
