#!/bin/sh

set -e

ZSTD_VERSION=$(cd zstd && echo "$(git tag --points-at HEAD | tr -d '[:space:]')" && cd ..)
GIT_ZSTD_VERSION=${ZSTD_VERSION}
FILE_ZSTD_VERSION=$(echo "${ZSTD_VERSION}" | cut -c 2-)
OS=windows
PLATFORM=x64
OUTPUT_DIR=${OUTPUT_DIR:=pkg/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/}

# build
# docker run --rm -v "$PWD/builder/zstd:/builder" -v "$PWD/zstd:/src" ubuntu:22.04 /bin/sh /builder/zstd-builder-windows-x64.sh
docker run --rm -v "%cd%/builder/zstd:/builder" -v "%cd%/zstd:/src" guitarrapc/ubuntu-mingw-w64:22.04.1 /bin/bash /builder/zstd-builder-windows-x64.sh

# confirm
ls zstd/lib/dll/libzstd.dll
ls zstd/programs/zstd.exe

# copy
mkdir -p "./${OUTPUT_DIR}/mingw"
cp ./zstd/lib/dll/libzstd.dll "./${OUTPUT_DIR}/mingw/."
cp "./zstd/programs/zstd.exe" "./${OUTPUT_DIR}/mingw/."
