#!/bin/bash
set -eu

SRC_DIR=mbedtls
LIBNAME_CRYPTO=libmbedcrypto
LIBNAME_TLS=libmbedtls
LIBNAME_X509=libmbedx509

VERSION=$(cd $SRC_DIR && echo "$(git tag --points-at HEAD | tr -d '[:space:]')" && cd ..)
GIT_VERSION=${VERSION}
# 'v1.5.2 ' -> '1.5.2'
FILE_VERSION=$(echo "${VERSION}" | cut -c 2-)
