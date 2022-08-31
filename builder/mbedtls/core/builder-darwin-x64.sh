#!/bin/bash
set -eu

cd $SRC_DIR
  make clean
  make SHARED=1 CFLAGS="-target x86_64-apple-macos10.12 -Werror -O2" lib
cd ..
