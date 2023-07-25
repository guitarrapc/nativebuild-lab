#!/bin/bash
set -e

SCRIPT_DIR=$(cd $(dirname $0); pwd)
source $SCRIPT_DIR/settings.sh
OS=windows
PLATFORM=x64
OUTPUT_DIR=${OUTPUT_DIR:=pkg/${SRC_DIR}/${GIT_VERSION}/${OS}/${PLATFORM}/}

# build
docker run --rm -v "$SCRIPT_DIR/core:/builder" -v "$PWD/$SRC_DIR:/src" ubuntu:22.04 /bin/sh /builder/builder-windows-x64-mingw.sh
# docker run --rm -v "$SCRIPT_DIR/core:/builder" -v "$PWD/$SRC_DIR:/src" guitarrapc/ubuntu-mingw-w64:22.04.1 /bin/bash /builder/builder-windows-x64-mingw.sh

# confirm
ls $SRC_DIR/lib/dll/$LIBNAME.dll
ls $SRC_DIR/programs/$EXENAME.exe

# copy
mkdir -p "./${OUTPUT_DIR}/mingw"
cp ./$SRC_DIR/lib/dll/$LIBNAME.dll "./${OUTPUT_DIR}/mingw/."
cp "./$SRC_DIR/programs/$EXENAME.exe" "./${OUTPUT_DIR}/mingw/."
