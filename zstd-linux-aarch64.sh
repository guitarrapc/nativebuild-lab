#!/bin/sh

set -e

ZSTD_VERSION=$(cd zstd && echo "$(git tag --points-at HEAD | tr -d '[:space:]')" && cd ..)
GIT_ZSTD_VERSION=${ZSTD_VERSION}
FILE_ZSTD_VERSION=$(echo "${ZSTD_VERSION}" | cut -c 2-)
OS=linux
PLATFORM=aarch64
OUTPUT_BASE=${OUTPUTBASE:=pkg}

# build
docker run --rm -v "$PWD/builder/zstd:/builder" -v "$PWD/zstd:/src" ubuntu:22.04 /bin/sh /builder/zstd-builder-linux-aarch64.sh

# confirm
ls zstd/lib/libzstd.a
ls zstd/lib/libzstd.so*
ls zstd/zstd

# copy
mkdir -p "./${OUTPUT_BASE}/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/"
cp ./zstd/lib/libzstd.a "./${OUTPUT_BASE}/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/."
cp "./zstd/lib/libzstd.so.${FILE_ZSTD_VERSION}" "./${OUTPUT_BASE}/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/libzstd.so"
cp ./zstd/programs/zstd "./${OUTPUT_BASE}/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/."
