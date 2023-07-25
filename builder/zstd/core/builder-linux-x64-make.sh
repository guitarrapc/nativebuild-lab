#!/bin/sh
set -eu

apk --no-cache add make gcc libc-dev
cd /src
make clean
make

# generate file test
if ! file "$(readlink -f /src/lib/libzstd.so)" | grep "x86-64"; then
  file "$(readlink -f /src/lib/libzstd.so)"
  echo "file generation arch not desired."
  exit 1
fi
