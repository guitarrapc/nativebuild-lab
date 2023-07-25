#!/bin/bash
set -eu

SRC_DIR=mbedtls_wrapper
MAKE_LIB=${SRC_DIR}/library
CMAKE_LIB=${SRC_DIR}/cmake/build

PREFIX=${PREFIX:=""}
LIBNAME=lib${PREFIX}mbedtls_wrapper

VERSION=v3.2.1
GIT_VERSION=${VERSION}
# 'v1.5.2 ' -> '1.5.2'
FILE_VERSION=$(echo "${VERSION}" | cut -c 2-)
