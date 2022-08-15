#!/bin/sh
ZSTD_VERSION=$(cd zstd && "$(git tag --points-at HEAD | tr -d '[:space:]')" && cd ..)
GIT_ZSTD_VERSION=${ZSTD_VERSION}
FILE_ZSTD_VERSION=$(echo "${ZSTD_VERSION}" | cut -c 1)
OS=linux
PLATFORM=amd64

docker run --rm -v "$PWD/builder/zstd:/builder" -v "$PWD/zstd:/src" alpine:latest /bin/sh /builder/zstd-builder-amd64.sh
ls zstd/lib/libzstd.a
ls zstd/lib/libzstd.so*

mkdir -p "./pkg/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/"
cp ./zstd/lib/libzstd.a "./pkg/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/."
cp "./zstd/lib/libzstd.so.${FILE_ZSTD_VERSION}" "./pkg/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/libzstd.so"
