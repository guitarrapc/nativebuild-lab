#!/bin/bash
set -eu

SRC_DIR=lz4
MAKE_LIB=${SRC_DIR}/lib
MAKE_PROGRAMS=${SRC_DIR}/programs

LIBNAME=liblz4
EXENAME=lz4

VERSION=$(cd $SRC_DIR && echo "$(git tag --points-at HEAD -l -n0 | grep "v" | tr -d '[:space:]')" && cd ..)
GIT_VERSION=${VERSION}
# 'v1.5.2 ' -> '1.5.2'
FILE_VERSION=$(echo "${VERSION}" | cut -c 2-)
