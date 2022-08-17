#!/bin/sh

set -e

ZSTD_VERSION=$(cd zstd && echo "$(git tag --points-at HEAD | tr -d '[:space:]')" && cd ..)
GIT_ZSTD_VERSION=${ZSTD_VERSION}
FILE_ZSTD_VERSION=$(echo "${ZSTD_VERSION}" | cut -c 2-)
OS=linux
PLATFORM=arm64
OUTPUT_DIR=${OUTPUT_DIR:=pkg/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/}

# build
docker run --rm -v "$PWD/builder/zstd/docker:/builder" -v "$PWD/zstd:/src" ubuntu:22.04 /bin/sh /builder/zstd-builder-linux-arm64.sh

# confirm
ls zstd/lib/libzstd.a
ls zstd/lib/libzstd.so*
ls zstd/programs/zstd

# copy
mkdir -p "./${OUTPUT_DIR}/"
cp ./zstd/lib/libzstd.a "./${OUTPUT_DIR}/."
cp "./zstd/lib/libzstd.so.${FILE_ZSTD_VERSION}" "./${OUTPUT_DIR}/libzstd.so"
cp ./zstd/programs/zstd "./${OUTPUT_DIR}/."
