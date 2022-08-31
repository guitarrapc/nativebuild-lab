#!/bin/bash
set -eu

cd $SRC_DIR
  make clean
  # target arm64-apple-macos11 cause following warning "was built for newer macOS version (12.0) than being linked (11.0)"
  make SHARED=1 CFLAGS="-target arm64-apple-macos12 -Werror -O2"
cd ..
