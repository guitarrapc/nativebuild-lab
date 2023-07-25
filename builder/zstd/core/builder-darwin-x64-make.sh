#!/bin/bash
set -eu

pushd $SRC_DIR
  make clean
  make CFLAGS="-target $TARGET -Werror -O3"
popd
