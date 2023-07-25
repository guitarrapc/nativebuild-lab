#!/bin/sh
set -eu

apk --no-cache add make gcc libc-dev file
cd /src
make clean
make

# generate file test
if ! file "$(readlink -f /src/lib/liblz4.so)" | grep "x86-64"; then
  file "$(readlink -f /src/lib/liblz4.so)"
  echo "file generation arch not desired."
  exit 1
fi
