#!/bin/sh

set -e

# generate arm64 on M1/M2, amd64 on Intel or Rosetta2.

ZSTD_VERSION=$(cd zstd && echo "$(git tag --points-at HEAD | tr -d '[:space:]')" && cd ..)
GIT_ZSTD_VERSION=${ZSTD_VERSION}
FILE_ZSTD_VERSION=$(echo "${ZSTD_VERSION}" | cut -c 2-)
OS=darwin
PLATFORM=$(uname -m)
if [[ "$PLATFORM" == "x86_64" ]]; then PLATFORM="amd64"; fi

# build
cd zstd
make clean
make
cd ..

# confirm
ls zstd/lib/libzstd.a
ls zstd/lib/libzstd.*dylib

# copy
mkdir -p "./pkg/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/"
cp ./zstd/lib/libzstd.a "./pkg/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/."
cp "./zstd/lib/libzstd.${FILE_ZSTD_VERSION}.dylib" "./pkg/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/libzstd.dylib"
