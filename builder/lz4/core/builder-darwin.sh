#!/bin/bash
set -eu

pushd $SRC_DIR
  make clean
  make CFLAGS="-target $TARGET -Werror -O3"
popd

# generate file test
if ! file "$(readlink -f $SRC_DIR/lib/liblz4.dylib)" | grep "$ARCH"; then
  file "$(readlink -f $SRC_DIR/lib/liblz4.dylib)"
  echo "file generation arch not desired."
  exit 1
fi
