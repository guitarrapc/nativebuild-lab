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
ls $SRC_DIR/lib/dll/$LIBNAME.dll
ls $SRC_DIR/programs/$EXENAME.exe

# copy
mkdir -p "./${OUTPUT_DIR}/mingw"
cp ./$SRC_DIR/lib/dll/$LIBNAME.dll "./${OUTPUT_DIR}/mingw/."
cp "./$SRC_DIR/programs/$EXENAME.exe" "./${OUTPUT_DIR}/mingw/."
