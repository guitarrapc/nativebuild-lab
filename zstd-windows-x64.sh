#!/bin/sh

set -e

ZSTD_VERSION=$(cd zstd && echo "$(git tag --points-at HEAD | tr -d '[:space:]')" && cd ..)
GIT_ZSTD_VERSION=${ZSTD_VERSION}
FILE_ZSTD_VERSION=$(echo "${ZSTD_VERSION}" | cut -c 2-)
OS=windows
PLATFORM=x64

# build
docker run --rm -v "$PWD/builder/zstd:/builder" -v "$PWD/zstd:/src" ubuntu:22.04 /bin/sh /builder/zstd-builder-windows.sh

# confirm
ls zstd/lib/dll/libzstd.dll
ls zstd/programs/zstd.exe

# copy
mkdir -p "./pkg/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/"
cp ./zstd/lib/dll/libzstd.dll "./pkg/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/."
cp "./zstd/programs/zstd.exe" "./pkg/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/."
