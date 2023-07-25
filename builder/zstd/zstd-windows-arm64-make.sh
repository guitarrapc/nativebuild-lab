#!/bin/bash
set -e

SCRIPT_DIR=$(cd $(dirname $0); pwd)
source $SCRIPT_DIR/settings.sh
OS=windows
PLATFORM=arm64
OUTPUT_DIR=${OUTPUT_DIR:=pkg/${SRC_DIR}/${GIT_VERSION}/${OS}/${PLATFORM}/}

# build
docker run --rm -v "$SCRIPT_DIR/core:/builder" -v "$PWD/$SRC_DIR:/src" mstorsjo/llvm-mingw:20220802 /bin/bash /builder/builder-windows-arm64-mingw.sh

# confirm
ls $MAKE_LIB/dll/$LIBNAME.dll
ls $MAKE_PROGRAMS/$EXENAME.exe

# copy
mkdir -p "./${OUTPUT_DIR}/mingw"
cp ./$MAKE_LIB/dll/$LIBNAME.dll "./${OUTPUT_DIR}/mingw/."
cp "./$MAKE_PROGRAMS/$EXENAME.exe" "./${OUTPUT_DIR}/mingw/."
