#!/bin/sh

set -e

# generate arm64 on M1/M2, amd64 on Intel or Rosetta2.

ZSTD_VERSION=$(cd zstd && echo "$(git tag --points-at HEAD | tr -d '[:space:]')" && cd ..)
GIT_ZSTD_VERSION=${ZSTD_VERSION}
FILE_ZSTD_VERSION=$(echo "${ZSTD_VERSION}" | cut -c 2-)
OS=darwin
PLATFORM=$(uname -m)
if [[ "$PLATFORM" == "x86_64" ]]; then PLATFORM="amd64"; fi
OUTPUT_BASE=${OUTPUTBASE:=pkg}

# build
cd zstd
make clean
make
cd ..

# confirm
ls zstd/lib/libzstd.a
ls zstd/lib/libzstd.*dylib
ls zstd/programs/zstd

# copy
mkdir -p "./${OUTPUT_BASE}/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/"
cp ./zstd/lib/libzstd.a "./${OUTPUT_BASE}/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/."
cp "./zstd/lib/libzstd.${FILE_ZSTD_VERSION}.dylib" "./${OUTPUT_BASE}/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/libzstd.dylib"
cp ./zstd/programs/zstd "./${OUTPUT_BASE}/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/."
