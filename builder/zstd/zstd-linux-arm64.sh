#!/bin/bash
set -e

SCRIPT_DIR=$(cd $(dirname $0); pwd)
source $SCRIPT_DIR/settings.sh
OS=linux
PLATFORM=arm64
OUTPUT_DIR=${OUTPUT_DIR:=pkg/${SRC_DIR}/${GIT_VERSION}/${OS}/${PLATFORM}/}

# build
docker run --rm -v "$SCRIPT_DIR/core:/builder" -v "$PWD/$SRC_DIR:/src" ubuntu:22.04 /bin/bash /builder/builder-linux-arm64.sh

# confirm
ls $CMAKE_LIB/$LIBNAME.a
ls $CMAKE_LIB/$LIBNAME.so*
ls $CMAKE_PROGRAMS/$EXENAME

# copy
mkdir -p "./${OUTPUT_DIR}/"
cp ./$CMAKE_LIB/$LIBNAME.a "./${OUTPUT_DIR}/."
cp "./$CMAKE_LIB/$LIBNAME.so.${FILE_VERSION}" "./${OUTPUT_DIR}/$LIBNAME.so"
cp ./$CMAKE_PROGRAMS/$EXENAME "./${OUTPUT_DIR}/."
