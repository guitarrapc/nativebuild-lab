#!/bin/bash
set -eu

pushd $SRC_DIR
  make clean
  make CFLAGS="-target arm64-apple-macos11 -Werror -O3"
popd
