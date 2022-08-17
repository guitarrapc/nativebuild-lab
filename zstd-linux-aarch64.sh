#!/bin/sh

set -e

ZSTD_VERSION=$(cd zstd && echo "$(git tag --points-at HEAD | tr -d '[:space:]')" && cd ..)
GIT_ZSTD_VERSION=${ZSTD_VERSION}
FILE_ZSTD_VERSION=$(echo "${ZSTD_VERSION}" | cut -c 2-)
OS=linux
PLATFORM=aarch64

# build
docker run --rm -v "$PWD/builder/zstd:/builder" -v "$PWD/zstd:/src" ubuntu:22.04 /bin/sh /builder/zstd-builder-linux-aarch64.sh

# confirm
ls zstd/lib/libzstd.a
ls zstd/lib/libzstd.so*

# copy
mkdir -p "./pkg/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/"
cp ./zstd/lib/libzstd.a "./pkg/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/."
cp "./zstd/lib/libzstd.so.${FILE_ZSTD_VERSION}" "./pkg/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/libzstd.so"
