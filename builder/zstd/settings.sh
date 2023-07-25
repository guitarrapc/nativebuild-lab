#!/bin/bash
set -eu

SRC_DIR=zstd
MAKE_LIB=${SRC_DIR}/lib
MAKE_PROGRAMS=${SRC_DIR}/programs
CMAKE_LIB=${SRC_DIR}/build/cmake/build/lib
CMAKE_PROGRAMS=${SRC_DIR}/build/cmake/build/programs

LIBNAME=libzstd
EXENAME=zstd

VERSION=$(cd $SRC_DIR && echo "$(git tag --points-at HEAD -l -n0 | grep "v" | tr -d '[:space:]')" && cd ..)
GIT_VERSION=${VERSION}
# 'v1.5.2 ' -> '1.5.2'
FILE_VERSION=$(echo "${VERSION}" | cut -c 2-)
