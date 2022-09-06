#!/bin/bash
set -eu

SRC_DIR=mbedtls
PREFIX=${PREFIX:=""}
LIBNAME_CRYPTO=lib${PREFIX}mbedcrypto
LIBNAME_TLS=lib${PREFIX}mbedtls
LIBNAME_X509=lib${PREFIX}mbedx509

VERSION=$(cd $SRC_DIR && echo "$(git tag --points-at HEAD -l -n0 | grep "v" | tr -d '[:space:]')" && cd ..)
GIT_VERSION=${VERSION}
# 'v1.5.2 ' -> '1.5.2'
FILE_VERSION=$(echo "${VERSION}" | cut -c 2-)
