#!/bin/sh
ZSTD_VERSION=$(cd zstd && echo "$(git tag --points-at HEAD | tr -d '[:space:]')" && cd ..)
GIT_ZSTD_VERSION=${ZSTD_VERSION}
FILE_ZSTD_VERSION=$(echo "${ZSTD_VERSION}" | cut -c 2-)
OS=darwin
PLATFORM=arm

# build
cd zstd
make
cd ..

# confirm
ls zstd/lib/libzstd.a
ls zstd/lib/libzstd.*dylib

# copy
mkdir -p "./pkg/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/"
cp ./zstd/lib/libzstd.a "./pkg/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/."
cp "./zstd/lib/libzstd.${FILE_ZSTD_VERSION}.dylib" "./pkg/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/libzstd.dylib"
