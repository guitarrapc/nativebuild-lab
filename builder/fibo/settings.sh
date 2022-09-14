#!/bin/bash
set -eu

SRC_DIR=fibo
MAKE_LIB=${SRC_DIR}/lib
CMAKE_LIB=${SRC_DIR}/build/cmake/build

PREFIX=${PREFIX:=""}
LIBNAME=lib${PREFIX}fibo

VERSION=v1.0.0
GIT_VERSION=${VERSION}
# 'v1.5.2 ' -> '1.5.2'
FILE_VERSION=$(echo "${VERSION}" | cut -c 2-)
