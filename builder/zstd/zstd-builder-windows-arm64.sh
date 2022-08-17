#!/bin/sh
# see: https://github.com/facebook/zstd/issues/2072
apt-get update
apt-get install -yq --no-install-suggests --no-install-recommends make gcc libc-dev
cd /src
make clean
make EXT=.exe TARGET_SYSTEM=Windows CC=aarch64-w64-mingw32-gcc WINDRES=aarch64-w64-mingw32-windres AR=aarch64-w64-mingw32-ar

# apt-get install -y xz-utils curl
# curl -LO https://github.com/mstorsjo/llvm-mingw/releases/download/20220323/llvm-mingw-20220323-ucrt-ubuntu-18.04-aarch64.tar.xz
# tar -Jxvf llvm-mingw-20220323-ucrt-ubuntu-18.04-aarch64.tar.xz
