#!/bin/bash
set -e

SCRIPT_DIR=$(cd $(dirname $0); pwd)
source $SCRIPT_DIR/settings.sh
OS=windows
PLATFORM=x86
OUTPUT_DIR=${OUTPUT_DIR:=pkg/${SRC_DIR}/${GIT_VERSION}/${OS}/${PLATFORM}/}

# build
docker run --rm -v "$SCRIPT_DIR/core:/builder" -v "$PWD/$SRC_DIR:/src" ubuntu:22.04 /bin/sh /builder/builder-windows-x86-make.sh
# docker run --rm -v "$SCRIPT_DIR/core:/builder" -v "$PWD/$SRC_DIR:/src" guitarrapc/ubuntu-mingw-w64:22.04.1 /bin/bash /builder/builder-windows-x86-make.sh

# confirm
ls $MAKE_LIB/dll/$LIBNAME.dll
ls $MAKE_PROGRAMS/$EXENAME.exe

# copy
mkdir -p "./${OUTPUT_DIR}/mingw"
cp ./$MAKE_LIB/dll/$LIBNAME.dll "./${OUTPUT_DIR}/mingw/."
cp "./$MAKE_PROGRAMS/$EXENAME.exe" "./${OUTPUT_DIR}/mingw/."
